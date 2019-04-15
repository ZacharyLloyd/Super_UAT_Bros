﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Spawn : MonoBehaviour
{
    public static Player_Spawn instance;

    public Vector3 coordinates;

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

    private void Start()
    {
        Debug.Log("Coordinates: " + coordinates);
        GameManager.instance.playerPrefab.transform.position = new Vector3(GameManager.instance.posx, GameManager.instance.posy, 0);
        Debug.Log("Player was created");
    }
}