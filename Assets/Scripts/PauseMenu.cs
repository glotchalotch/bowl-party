using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Transform player;
    public Canvas canvas;
    public GameLogic gameLogic;
    // Start is called before the first frame update
    void Start()
    {
        canvas.enabled = false;
    }

    // Update is called once per frame
    public void TogglePause(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            canvas.enabled = !canvas.enabled;
            transform.position = player.position + (player.forward * 0.75f);
            transform.rotation = Quaternion.Euler(player.rotation.eulerAngles.x, player.rotation.eulerAngles.y, 0);
            gameLogic.TogglePause();
        }
        
    }

    public void BackToTitle()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene("TitleScreen");
    }
}
