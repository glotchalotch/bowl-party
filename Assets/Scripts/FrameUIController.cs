using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FrameUIController : MonoBehaviour
{
    public TextMeshProUGUI[] scoreTexts;
    public TextMeshProUGUI totalScoreText;
    private int _totalScore;
    public int TotalScoreOnFrame { 
        get
        {
            return _totalScore;
        }
        set
        {
            _totalScore = value;
            totalScoreText.SetText(_totalScore.ToString());
        } 
    }

    public void SetScoreText(int scoreTextNumber, GameLogic.Score.IndividualScore score)
    {
        switch(score.type)
        {
            case GameLogic.ScoreType.Strike:
                {
                    scoreTexts[scoreTextNumber].SetText("X");
                    break;
                }
            case GameLogic.ScoreType.Spare:
                {
                    scoreTexts[scoreTextNumber].SetText("/");
                    break;
                }
            default:
                {
                    scoreTexts[scoreTextNumber].SetText(score.Value == 0 ? "-" : score.Value.ToString());
                    break;
                }
        }
    }

    public void ResetScore()
    {
        foreach(TextMeshProUGUI text in scoreTexts)
        {
            text.SetText("");
        }
        totalScoreText.SetText("");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
