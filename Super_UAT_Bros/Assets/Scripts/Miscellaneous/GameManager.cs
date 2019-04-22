using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //Singleton
    public Transform tf; //Transform for player or enemy automatically grabs this accordingly
    public PlayerPawn player; //Reference to PlayerPawn
    public Image healthUI; //Reference to the healthUI
    public float currentHealth; //Setting current health
    public float maxHealth; //Setting max health
    public float damage; //Setting the damage the enemy does
    public float ammoValue; //Setting the new value for the ammo after ammo is picked up
    public static float ammo; //Setting the value for the current ammo
    public float maxAmmo; //Setting the value for the max ammo
    public TextMeshProUGUI ammoUI; //Reference for the ammoUI

    public GameObject enemy; //Getting the enemy

    public GameObject playerPrefab; //Getting the player

    private Player_Spawn playerSpawn; //Setting the player spawn

    //Setting the destination of where to go next in the game
    [Header("Destination")]
    public string Scene_Name;

    //Setting the postion for where to spawn
    [Header("Set Position")]
    public float posx, posy;

    //UI is the child of gamemanager and is set to be disabled in main menu and to be enabled in the gameplay
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
