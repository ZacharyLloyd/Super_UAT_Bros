using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Spawn : MonoBehaviour
{
    public static Player_Spawn instance; //Setting the instance for the spawning
    public Vector3 coordinates; //Setting the coordinates for spawning

    //Setting the spawn to the player or to be destoryed
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //The actual spawning of the player
    private void Start()
    {
        GameManager.instance.playerPrefab.transform.position = new Vector3(GameManager.instance.posx, GameManager.instance.posy, 0);   
    }
}