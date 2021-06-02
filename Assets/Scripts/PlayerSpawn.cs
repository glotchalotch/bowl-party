using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject playerPrefab;
    //public Player Player { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        Player _player = Player.instance;
        if (_player == null)
        {
            Instantiate(playerPrefab, transform);
        } else _player.transform.SetPositionAndRotation(transform.position, transform.rotation);
        /*else
        {
            Player = _player;
            Player.transform.SetPositionAndRotation(transform.position, transform.rotation);
        }*/
        SaveDataManager.LoadSave();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
