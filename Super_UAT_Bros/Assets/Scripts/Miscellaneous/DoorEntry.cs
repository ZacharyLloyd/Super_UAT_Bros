
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEntry : MonoBehaviour
{
    public static DoorEntry instance; //Use for easy access to this class

    public float value_x; //Setting x cordinate
    public float value_y; //Setting y cordinate
    public string scene_name; //Scene name to switch levels
    public bool allowSpawn = true; //Bool for spawning

    public PlayerPawn player; //Setting the palyer

    private void Awake()
    {
        //Finding the player
        player = FindObjectOfType<PlayerPawn>();
    }
    //Switching scences/levels
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
