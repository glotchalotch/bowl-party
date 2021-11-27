using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreboardUIController : MonoBehaviour
{
    public FrameUIController[] frames;
    public TextMeshProUGUI totalOverall;
    public TextMeshProUGUI highScoreText;
    public Transform player;
    bool heightSet = false;
    int framesElapsed = 0;
    // Start is called before the first frame update
    void Start()
    {
        SaveDataManager.LoadSave();
        highScoreText.SetText("High Score\n" + SaveDataManager.save.highScore.ToString());
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (framesElapsed >= 1) //height will report lower than actual if <1 frame has elapsed since load
        {
            if (!heightSet)
            {
                float? y = PlayerUtilities.GetRelativeHeight(player.position, 0);
                if (y != null)
                {
                    transform.position = new Vector3(transform.position.x, (float)y, transform.position.z);
                    heightSet = true;
                }
            }
        }
        else framesElapsed++;
    }

    public void SetSubframeScore(int frame, int subframe, GameLogic.Score.IndividualScore score)
    {
        frames[frame].SetScoreText(subframe, score);
    }

    public void SetFrameTotalScore(int frame, int score, bool add)
    {
        if (add) frames[frame].TotalScoreOnFrame += score;
        else frames[frame].TotalScoreOnFrame = score;
    }

    public void SetTotalScore(int score)
    {
        totalOverall.SetText(score.ToString());
        if(SaveDataManager.save.highScore < score)
        {
            SaveDataManager.save.highScore = score;
            SaveDataManager.SaveGame();
            highScoreText.SetText("High Score\n" + score.ToString());
        }
    }
}
