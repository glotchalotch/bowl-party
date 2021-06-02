using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinFallTrigger : MonoBehaviour
{
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
        if(other.gameObject.CompareTag("BowlingBall"))
        {
            other.gameObject.GetComponent<BowlingBall>().ResetBall();
        } else if(other.gameObject.CompareTag("Pin"))
        {
            Pin p = other.GetComponent<Pin>();
            if (p != null)
            {
                p.Fallen = true;
                other.gameObject.SetActive(false);
                return;
            }
        }
    }
}
