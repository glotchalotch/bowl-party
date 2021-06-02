using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PauseMenu : MonoBehaviour
{
    Player player;
    public Canvas canvas;
    public GameLogic gameLogic;
    // Start is called before the first frame update
    void Start()
    {
        canvas.enabled = false;
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        bool pressed = SteamVR_Input.GetStateDown("Pause", SteamVR_Input_Sources.Any, true);
        if (pressed)
        {
            canvas.enabled = !canvas.enabled;
            Transform t = player.hmdTransform;
            transform.position = t.position + (t.forward * 0.75f);
            transform.rotation = Quaternion.Euler(t.rotation.eulerAngles.x, t.rotation.eulerAngles.y, 0);
            gameLogic.TogglePause();
        }
    }
}
