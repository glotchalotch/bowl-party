using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingBall : MonoBehaviour
{
    AudioSource audioSource;
    public Rigidbody rigid;
    public GameLogic gameLogic;
    public AudioClip[] clips = new AudioClip[2];
    public Vector3 ballRespawnPosition;
    bool rigidPaused = false;
    Vector3 velocity;
    Vector3 angularVelocity;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameLogic.paused && !rigidPaused)
        {
            velocity = rigid.velocity;
            angularVelocity = rigid.angularVelocity;
            rigid.isKinematic = true;
            rigid.detectCollisions = false;
            rigidPaused = true;
        } else if(!gameLogic.paused && rigidPaused)
        {
            rigid.isKinematic = false;
            rigid.detectCollisions = true;
            rigidPaused = false;
            rigid.velocity = velocity;
            rigid.angularVelocity = angularVelocity;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("BowlingFloor"))
        {
            if(collision.relativeVelocity.y > 2)
            {
                audioSource.PlayOneShot(clips[0]);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (rigid.velocity.x > 0.5 || rigid.velocity.z > 0.5)
        {
            if (collision.gameObject.CompareTag("BowlingFloor") && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else if (audioSource.clip.Equals(clips[1])) audioSource.Stop();
    }
    
    public void ResetBall()
    {
        transform.position = ballRespawnPosition;
        rigid.angularVelocity = Vector3.zero;
        rigid.velocity = Vector3.back;
    }
}
