using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartButton : PressableButton
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
        //if(other.gameObject.gameObject.gameObject.GetComponent<Valve.VR.InteractionSystem.HandCollider>() != null)
        //{
        base.ButtonAction(other);
        gameLogic.StartGame();
        //}
    }
}
