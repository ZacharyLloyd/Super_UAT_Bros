using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Singleton
    public static GameManager instance;
    public Transform tf;
    public Player player;
    public Image healthUI;
    public float currentHealth;
    public float maxHealth;
    public float damage;
    public float ammoValue;
    public static float ammo;
    public float maxAmmo;
    public TextMeshProUGUI ammoUI;


    public GameObject enemy;
    public float enemiesKilled = 0;

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

        if (ammo != 0)
            if (Input.GetKeyDown(KeyCode.Space)) UseAmmo();


        if (enemiesKilled >= 4)
        {
            SceneManager.LoadScene(2);
        }
    }
    //Using ammo UI
    public void UseAmmo()
    {
        --ammo;
        ammoUI.text = ammo.ToString() + "/" + maxAmmo.ToString();
    }
    public void DecreaseHealth(float damage)
    {
        if (healthUI.fillAmount != 0)
        {
            healthUI.fillAmount -= damage / maxHealth;
            currentHealth = healthUI.fillAmount;
        }
        else SceneManager.LoadScene(3);
    }
}
