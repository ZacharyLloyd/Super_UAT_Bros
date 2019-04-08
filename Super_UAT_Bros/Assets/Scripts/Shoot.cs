using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Shoot : MonoBehaviour
{
    GameManager instance;

    public Transform pointOfFire; //Assigning the gameObject that is the child to the Player gameObject
    public GameObject bulletPrefab; //Assing a Prefab to the slot in order to spawn it when shooting

    public bool automaticMode;
    [Range(1, 20)] public int recoilSpeed;

    private bool isKeyReleased;
    private IEnumerator coroutine;

    Animator animator;
    float seconds = 1f;
    float settedSeconds;
    bool IsShooting = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        settedSeconds = seconds;
    }

    //called at start
    private void Start()
    {
        bulletPrefab.tag = "Bullet";
    }
    //called once per frame
    void Update()
    {
        animator.SetBool("IsShooting", IsShooting);
        //When player shoots
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

    private IEnumerator Recoil()
    {
        float value = (float)recoilSpeed;
        yield return new WaitForSeconds(1 / value);
        isKeyReleased = true;
    }
}