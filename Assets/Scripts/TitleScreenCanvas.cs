using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class TitleScreenCanvas : MonoBehaviour
{
    public Canvas creditsCanvas;
    public BoxCollider[] creditsCanvasColliders;
    public Canvas optionsCanvas;
    public BoxCollider[] optionsCanvasColliders;
    public Canvas mainCanvas;
    public BoxCollider[] mainCanvasColliders;
    Player player;
    bool heightSet = false;
    // Start is called before the first frame update
    void Start()
    {
        BackToMain();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!heightSet && player.eyeHeight > 0)
        {
            transform.position += new Vector3(0, player.eyeHeight, 0);
            heightSet = true;
        }
    }

    public void QuitGame()
    {
        Application.Quit(0);
    }

    public void PlayGame()
    {
        SteamVR_LoadLevel.Begin("Alley");
    }

    private void SetOptions(bool enable)
    {
        optionsCanvas.enabled = enable;
        foreach(BoxCollider b in optionsCanvasColliders)
        {
            b.enabled = enable;
        }
    }

    private void SetCredits(bool enable)
    {
        creditsCanvas.enabled = enable;
        foreach (BoxCollider b in creditsCanvasColliders)
        {
            b.enabled = enable;
        }
    }

    private void SetMain(bool enable)
    {
        mainCanvas.enabled = enable;
        foreach (BoxCollider b in mainCanvasColliders)
        {
            b.enabled = enable;
        }
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
