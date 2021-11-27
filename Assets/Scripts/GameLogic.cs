using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GameLogic : MonoBehaviour
{
    int frame;
    int subframe;
    public GameObject pinParentObject;
    List<Score> scores = new List<Score>(new Score[10]);
    Pin[] pins;
    public ScoreboardUIController scoreboardUIController;
    public TextMeshProUGUI totalScoreText;
    public GameObject[] pinDiagramCanvasses;
    public static int TotalScore { get; private set; } = 0;
    public bool gameStarted = false;
    public bool paused { get; private set; } = false;
    public UnityEvent<bool> onPauseToggle;

    public class Score
    {
        public List<IndividualScore> values = new List<IndividualScore>(new IndividualScore[3]);
        public struct IndividualScore
        {
            private int? _value;
            public int? Value
            {
                get { return _value; }
                set
                {
                    TotalScore += (int)value;
                    _value = value;
                }
            }
            public ScoreType type;
        }
        public int frame;
        public int totalOnFrame;

        //If this int is -1 no strike was made
        public int shotsSinceStrike = -1;
    }

    public enum ScoreType
    {
        Normal,
        Strike,
        Spare
    }

    // Start is called before the first frame update
    void Start()
    {
        frame = 0;
        subframe = 0;
        pins = pinParentObject.GetComponentsInChildren<Pin>(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowScore()
    {
        int score = 0;
        int maxSubframe = frame == 9 ? 2 : 1;
        foreach(Pin p in pins)
        {
            if (p.Fallen)
            {
                score++;
                if (subframe != maxSubframe) {
                    p.gameObject.SetActive(false);
                    //this weird is here again so that SetPositionAndRotation is only called once per pin regardless of maxSubframe
                    p.ResetPin();
                }
            }
            if (subframe == maxSubframe)
            {
                p.ResetPin();
            }
        }

        ScoreType scoreType;
        if (score == 10 && (subframe == 0 || subframe == 2))
        {
            scoreType = ScoreType.Strike;
            if (frame != 9) maxSubframe = 0;
        }
        else if (subframe > 0 && scores[frame].values[subframe - 1].Value + score == 10)
        {
            scoreType = ScoreType.Spare;
        }
        else scoreType = ScoreType.Normal;
        Score.IndividualScore generatedScore = new Score.IndividualScore { Value = score, type = scoreType };
        scoreboardUIController.SetSubframeScore(frame, subframe, generatedScore);
        if(scores[frame] == null)
        {
            scores[frame] = new Score { values = new List<Score.IndividualScore> { generatedScore, new Score.IndividualScore(), new Score.IndividualScore() },
                totalOnFrame = frame > 0 ? scores[frame - 1].totalOnFrame : 0,
                frame = frame
            };
        } else
        {
            scores[frame].values[subframe] = generatedScore;
        }
        scores[frame].totalOnFrame += score;
        if(frame > 0)
        {
            List<Score> found = scores.FindAll(s => s != null && s.shotsSinceStrike < 2 && s.shotsSinceStrike > -1);
            if(found.Count > 0)
            {
                foreach (Score s in found)
                {
                    scores[s.frame].shotsSinceStrike++;
                    if (scores[s.frame].shotsSinceStrike == 2)
                    {
                        int i = 0;
                        foreach (Score.IndividualScore b in scores[s.frame + 1].values)
                        {
                            if (b.Value != null)
                            {
                                scores[s.frame].totalOnFrame += (int)b.Value;
                                scores[s.frame + 1].totalOnFrame += (int)b.Value;
                                i++;
                            }
                        }
                        if (i < 2)
                        {
                            Score.IndividualScore b = scores[s.frame + 2].values[0];
                            scores[s.frame].totalOnFrame += (int)b.Value;
                            scores[s.frame + 1].totalOnFrame += (int)b.Value;
                            scores[s.frame + 2].totalOnFrame += (int)b.Value + (int)scores[s.frame + 1].values[0].Value;
                            scoreboardUIController.SetFrameTotalScore(s.frame + 2, scores[s.frame + 2].totalOnFrame, false);
                        }
                        scoreboardUIController.SetFrameTotalScore(s.frame, scores[s.frame].totalOnFrame, false);
                        scoreboardUIController.SetFrameTotalScore(s.frame + 1, scores[s.frame + 1].totalOnFrame, false);
                        scores[s.frame].shotsSinceStrike = -1;
                    }
                }
            }
            if(subframe == 0 && scores[frame - 1].values[1].type == ScoreType.Spare)
            {
                Score aScore = scores[frame - 1];
                aScore.totalOnFrame += score;
                scores[frame].totalOnFrame += score;
                scoreboardUIController.SetFrameTotalScore(frame - 1, score, true);
                scores[frame - 1] = aScore;
            }
        }
        if (generatedScore.type == ScoreType.Strike && frame != 9) scores[frame].shotsSinceStrike = 0;

        scoreboardUIController.SetFrameTotalScore(frame, scores[frame].totalOnFrame, false);

        if (frame == 9)
        {
            if (subframe == 1 && !(scores[9].values[0].type == ScoreType.Strike || generatedScore.type == ScoreType.Spare || generatedScore.type == ScoreType.Strike)) maxSubframe = 1;
            else if(generatedScore.type == ScoreType.Strike || generatedScore.type == ScoreType.Spare) ActivatePins();
        }
        else if (generatedScore.type == ScoreType.Strike)
        {
            NextFrame();
            return;
        }
        

        if (subframe == maxSubframe)
        {
            if (frame != 9) NextFrame();
            else EndGame();
        }
        else subframe++;
    }

    void ActivatePins()
    {
        foreach (Pin p in pins)
        {
            if (!p.gameObject.activeSelf) p.gameObject.SetActive(true);
        }
    }

    void NextFrame()
    {
        frame++;
        subframe = 0;
        ActivatePins();
        foreach (GameObject g in pinDiagramCanvasses)
        {
            g.SetActive(true);
        }
    }

    void EndGame()
    {
        gameStarted = false;
        scoreboardUIController.SetTotalScore(scores[9].totalOnFrame);
    }

    public void StartGame()
    {
        if(!gameStarted)
        {
            gameStarted = true;
            foreach (Pin p in pins)
            {
                p.ResetPin();
                p.gameObject.SetActive(true);
            }
            scores = new List<Score>(new Score[10]);
            frame = 0;
            subframe = 0;
            TotalScore = 0;
            foreach(FrameUIController u in scoreboardUIController.GetComponentsInChildren<FrameUIController>())
            {
                u.ResetScore();
            }
            totalScoreText.SetText("");
            foreach(GameObject g in pinDiagramCanvasses)
            {
                g.SetActive(true);
            }
        }
    }

    public void TogglePause()
    {
        paused = !paused;
        onPauseToggle.Invoke(paused);
    }

}
