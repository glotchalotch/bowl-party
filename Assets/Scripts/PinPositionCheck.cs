using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PinPositionCheck : MonoBehaviour
{
    private Pin[] pins;
    private bool checking = false;
    private IEnumerator currentCoroutine;
    private IEnumerator currentFallbackCoroutine;
    public GameLogic gameLogic;
    // Start is called before the first frame update
    void Start()
    {
        pins = transform.parent.GetComponentsInChildren<Pin>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(checking && !gameLogic.paused)
        {
            int noVelocity = 0;
            foreach(Pin p in pins)
            {
                bool fallenBefore = p.Fallen;
                Vector3 rot = p.transform.rotation.eulerAngles;
                if ((rot.x >= 35 && rot.x <= 325) || (rot.y >= 35 && rot.y <= 325) || (rot.z >= 35 && rot.z <= 325))
                {
                    p.Fallen = true;
                }
                else
                {
                    p.Fallen = false;
                    if (!p.isActiveAndEnabled) gameLogic.pinDiagramCanvasses[p.pinNumber - 1].SetActive(false);
                }

                Rigidbody rigidbody = p.GetComponent<Rigidbody>();
                if (rigidbody.velocity.Equals(Vector3.zero)) noVelocity++;
            }
            if(noVelocity == pins.Length)
            {
                if(currentCoroutine == null)
                {
                    currentCoroutine = EndCheckCoroutine();
                    StartCoroutine(currentCoroutine);
                }
            } else if(currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
                currentCoroutine = null;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameLogic.gameStarted && other.GetComponent<Pin>() == null && !checking && !gameLogic.paused)
        {
            checking = true;
            currentCoroutine = EndCheckCoroutine();
            StartCoroutine(currentCoroutine);
            currentFallbackCoroutine = FallbackCoroutine();
            StartCoroutine(currentFallbackCoroutine);
        }
    }

    IEnumerator EndCheckCoroutine()
    {
        if (!gameLogic.paused)
        {
            yield return new WaitForSeconds(2f);
            StopCoroutine(currentFallbackCoroutine);
            checking = false;
            gameLogic.ShowScore();
        }
        else yield return null;
    }

    IEnumerator FallbackCoroutine()
    {
        if (!gameLogic.paused)
        {
            yield return new WaitForSeconds(7f);
            checking = false;
            if (currentCoroutine != null) StopCoroutine(currentCoroutine);
            gameLogic.ShowScore();
        }
        else yield return null;
    }
}
