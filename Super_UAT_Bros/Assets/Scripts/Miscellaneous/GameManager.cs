﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //Singleton
    public Transform tf;
    public PlayerPawn player;
    public Image healthUI;
    public float currentHealth;
    public float maxHealth;
    public float damage;
    public float ammoValue;
    public static float ammo;
    public float maxAmmo;
    public TextMeshProUGUI ammoUI;

    public GameObject enemy;

    public GameObject playerPrefab;

    private Player_Spawn playerSpawn;

    [Header("Destination")]
    public string Scene_Name;

    [Header("Set Position")]
    public float posx, posy;

    [Header("Game UI")]
    public RawImage healthUIParent; // This is just the background interface
    public bool GUI_ACTIVE;

    //Awake runs before all Starts
    private void Awake()
    {
        //Setup the Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        tf = GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthUI.fillAmount = currentHealth / maxHealth;
        ammo = ammoValue;
        ammo = maxAmmo;
        ammoUI.text =  ammo.ToString() + "/" + maxAmmo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //Set our GUI value to our GUI_ACTIVE boolean
        healthUIParent.gameObject.SetActive(GUI_ACTIVE);

        if (ammo != 0)
            if (Input.GetKeyDown(KeyCode.Space)) UseAmmo();
        
        if (ammo >= maxAmmo)
        {
            ammo = maxAmmo;
            ammoUI.text = ammo.ToString() + "/" + maxAmmo.ToString();
        }
    }

    //This is used for switching scenes/levels
    public void Goto_Scene(string scene_name)
    {
        scene_name = Scene_Name;
        if (scene_name != null) SceneManager.LoadScene(scene_name);

    }

    //Using ammo UI
    public void UseAmmo()
    {
        --ammo;
        ammoUI.text = ammo.ToString() + "/" + maxAmmo.ToString();
    }

    //Decreasing the player's health
    public void DecreaseHealth(float damage)
    {
        if (healthUI.fillAmount != 0)
        {
            healthUI.fillAmount -= damage / maxHealth;
            currentHealth = healthUI.fillAmount;
        }
        else SceneManager.LoadScene(3);
    }

    //Increasing the player's health
    public void IncreaseHealth(float heal)
    {
        if (healthUI.fillAmount != 1)
        {
            healthUI.fillAmount += heal / maxHealth;
            currentHealth = healthUI.fillAmount;
        }

    }

    //Increasing the player's ammo
    public void IncreaseAmmo(float addAmmo)
    {
        if (ammo != maxAmmo)
        {
            ammo += addAmmo;
            ammoUI.text = ammo.ToString() + "/" + maxAmmo.ToString();
        }
    }
}