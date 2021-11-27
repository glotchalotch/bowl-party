using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenSphere : MonoBehaviour
{
    public GameObject bowlingBall;
    public Material bowlingBallMaterial;
    Queue<GameObject> ballsFunny = new Queue<GameObject>(40);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(transform.up, 3f * Time.deltaTime);

        if(ballsFunny.Count < 40)
        {
            if(Random.Range(0, 39) == 0)
            {
                Vector3 pos = transform.position + (Random.onUnitSphere * 4.5f);
                pos.y = transform.position.y + 4.5f;
                GameObject created = Instantiate(bowlingBall, pos, Random.rotation);
                created.GetComponent<MeshRenderer>().material = bowlingBallMaterial;
                ballsFunny.Enqueue(created);
            }
        }
        bool ballDestroyed = false;
        foreach(GameObject ball in ballsFunny)
        {
            if (ball != null)
            {
                ball.transform.position -= Vector3.up * 1f * Time.deltaTime;
                if (ball.transform.position.y < transform.position.y - 4.5f)
                {
                    Destroy(ball);
                    ballDestroyed = true;
                }
            }
            else ballDestroyed = true;
        }
        if (ballDestroyed) ballsFunny.Dequeue();
    }
}
