using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class TitleScreenCanvas : MonoBehaviour
{
    public Canvas creditsCanvas;
    public BoxCollider[] creditsCanvasColliders;
    public Canvas optionsCanvas;
    public BoxCollider[] optionsCanvasColliders;
    public Canvas mainCanvas;
    public Transform player;
    //Player player;
    bool heightSet = false;
    int framesElapsed = 0;
    // Start is called before the first frame update
    void Start()
    {
        BackToMain();
    }

    private void Update()
    {
        if (framesElapsed >= 1) //height will report lower than actual if <1 frame has elapsed since load
        {
            if (!heightSet)
            {
                float? y = PlayerUtilities.GetRelativeHeight(player.position, -0.2f);
                if (y != null)
                {
                    transform.position = new Vector3(transform.position.x, (float)y, transform.position.z);
                    heightSet = true;
                }
            }
        }
        else framesElapsed++;
    }

    public void QuitGame()
    {
        Application.Quit(0);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Alley");
    }

    private void SetOptions(bool enable)
    {
        optionsCanvas.enabled = enable;
    }

    private void SetCredits(bool enable)
    {
        creditsCanvas.enabled = enable;
    }

    private void SetMain(bool enable)
    {
        mainCanvas.enabled = enable;
    }

    public void ShowOptions()
    {
        SetOptions(true);
        SetMain(false);
    }

    public void ShowCredits()
    {
        SetCredits(true);
        SetMain(false);
    }

    public void BackToMain()
    {
        SetOptions(false);
        SetCredits(false);
        SetMain(true);
        PlayerPrefs.Save();
    }

    public void OpenCC3License()
    {
        Application.OpenURL("https://creativecommons.org/licenses/by-nc/3.0/");
    }
}
