using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallResetButton : PressableButton
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void ButtonAction(Collider other)
    {
        base.ButtonAction(other);
        FindObjectOfType<BowlingBall>().ResetBall();
    }
}
