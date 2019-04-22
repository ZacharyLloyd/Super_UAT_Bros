using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Shoot : MonoBehaviour
{
    GameManager instance;

    public Transform pointOfFire; //Assigning the gameObject that is the child to the Player gameObject
    public GameObject bulletPrefab; //Assing a Prefab to the slot in order to spawn it when shooting

    public bool automaticMode; //Enable or disable automatic firing
    [Range(1, 20)] public int recoilSpeed; //Setting a recoil speed for automatic mode

    private bool isKeyReleased; //To shoot again
    private IEnumerator coroutine; //Corountine for automatic mode

    Animator animator; //Referencing the animator
    float seconds = 1f; //Time for recoil
    float settedSeconds; //Refernce to seconds
    bool IsShooting = false; //IsShooting bool to detect if shooting or not

    private void Awake()
    {
        animator = GetComponent<Animator>(); //Getting animator
        settedSeconds = seconds; //Setting seconds
    }

    //called at start
    private void Start()
    {
        //Getting bulletPrefab
        bulletPrefab.tag = "Bullet";
    }
    //called once per frame
    void Update()
    {
        //To play animation
        animator.SetBool("IsShooting", IsShooting);
        //When player shoots
        //Not automatic mode
        if (Input.GetKeyDown(KeyCode.Space) || isKeyReleased == true)
        {
            IsShooting = true;
            coroutine = Recoil();
            switch (automaticMode)
            {
                case false:
                    if (GameManager.ammo != 0)
                    {
                        seconds = settedSeconds;
                        isKeyReleased = false;
                        //Bullet will spawn with a set direction based on player's direction
                        Instantiate(bulletPrefab, pointOfFire.position, pointOfFire.rotation);
                        FindObjectOfType<AudioManager>().Play("Shooting");
                    }
                    break;
                case true:
                    isKeyReleased = false;
                    //Bullet will spawn with a set direction based on player's direction
                    Instantiate(bulletPrefab, pointOfFire.position, pointOfFire.rotation);
                    FindObjectOfType<AudioManager>().Play("Shooting");
                    StartCoroutine(coroutine);
                    break;
            }
        }
        //For automatic mode
        if (Input.GetKeyUp(KeyCode.Space) && automaticMode == true) StopCoroutine(coroutine);
        if (IsShooting == true && seconds != 0)
        {
            seconds -= Time.deltaTime;
            if (seconds <= 0)
            {
                seconds = settedSeconds;
                IsShooting = false;
            }
        }
    }
    //Recoil for automatic mode
    private IEnumerator Recoil()
    {
        float value = (float)recoilSpeed;
        yield return new WaitForSeconds(1 / value);
        isKeyReleased = true;
    }
}