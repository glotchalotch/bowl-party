using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressableButton : MonoBehaviour
{
    public AudioSource audioSource;
    public new Animation animation;
    public GameLogic gameLogic;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.gameObject.gameObject.gameObject.GetComponent<Valve.VR.InteractionSystem.HandCollider>() != null)
        //{
        if(!gameLogic.paused) ButtonAction(other);
        //}
    }

    protected virtual void ButtonAction(Collider other)
    {
        if (!animation.isPlaying)
        {
            animation.Play();
            audioSource.Play();
        }
    }
}
