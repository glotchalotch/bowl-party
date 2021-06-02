using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Valve.VR.InteractionSystem;

public class ScoreboardUIController : MonoBehaviour
{
    public FrameUIController[] frames;
    public TextMeshProUGUI totalOverall;
    public TextMeshProUGUI highScoreText;
    Player player;
    bool heightSet = false;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        highScoreText.SetText("High Score\n" + SaveDataManager.save.highScore.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if(!heightSet && player.eyeHeight > 0)
        {
            transform.position += new Vector3(0, player.eyeHeight, 0);
            heightSet = true;
        }
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
