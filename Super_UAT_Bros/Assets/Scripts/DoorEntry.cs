﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEntry : MonoBehaviour
{
    public static DoorEntry instance;

    public float value_x;
    public float value_y;
    public string scene_name;
    public bool allowSpawn = true;

    public Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (collision.gameObject.tag == "Player")
            {
                GameManager.instance.posx = this.value_x;
                GameManager.instance.posy = this.value_y;
                Player_Spawn.instance.coordinates = new Vector3(GameManager.instance.posx, GameManager.instance.posy, 0);
                collision.gameObject.transform.position = Player_Spawn.instance.coordinates;

                if (scene_name != null)
                {
                    GameManager.instance.Scene_Name = scene_name;
                    GameManager.instance.Goto_Scene(scene_name);
                }   
            }
        }
    }
}
