using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    public uint pinNumber;
    private bool _fallen = false;
    public bool Fallen { get => _fallen; 
        set
        {
            _fallen = value;
            gameLogic.pinDiagramCanvasses[pinNumber - 1].SetActive(!value);
        }
    }
    Vector3 initialPosition;
    AudioSource audioSource;
    Rigidbody rigid;
    public GameLogic gameLogic;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetPin()
    {
        rigid.angularVelocity = Vector3.zero;
        rigid.velocity = Vector3.zero;
        transform.SetPositionAndRotation(initialPosition, Quaternion.Euler(0, 0, 360));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("BowlingBall") || collision.gameObject.CompareTag("Pin"))
        {
            bool isBowlingBall = collision.gameObject.CompareTag("BowlingBall");
            float low = isBowlingBall ? 1.25f : 0.5f;
            float high = isBowlingBall ? 5f : 3f;
            float clamped = Mathf.Clamp(collision.rigidbody.velocity.magnitude, low, high);
            audioSource.pitch = 0.9f + (clamped - 1.25f) * 0.25f / (high - low);
            audioSource.volume = clamped - 0.2f;
            audioSource.Play();
        }
    }
}
