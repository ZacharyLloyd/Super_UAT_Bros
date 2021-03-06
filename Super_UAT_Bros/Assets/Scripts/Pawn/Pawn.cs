﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    //Player parts
    [HideInInspector] public bool isWalking; // Is player walking true/false
    [HideInInspector] public static Animator __animator; //The player's animator
    public Animator animator; // Player's animator
    public Rigidbody2D rb; // Player's rigidbody
    bool step = false; // Step is for detecting walking to play the sound
    public Transform tf; //Player's transform
    public Noisemaker noisemaker; //Player's noisemaker
    public GameObject bulletPrefab; //Player's bullet prefab
    public IEnumerator coroutine; //Coroutine for animations
    public PlayerController controller; //Reference to PlayerController

    //Enemy parts
    //AI Component
    public AISenses senses; //Reference to AISenses
    public float closeEnough; //Close enough is used for MoveTowards function
    public float moveSpeed = 1; //Enemy speed at which they move
    public bool canAttack = false; //A bool to allow the enemy to attack or not attack

    // Start is called before the first frame update
    public virtual void Start()
    {
        bulletPrefab.tag = "Bullet";
        
        // Store my senses component
        senses = GetComponent<AISenses>();

        tf = GetComponent<Transform>();

        rb = GetComponent<Rigidbody2D>();

        controller = GetComponent<PlayerController>();

        // Load noisemaker
        noisemaker = GetComponent<Noisemaker>();
    }
    public virtual void Update()
    {

    }
    //This is the player's functions
    //Moving to the right
    public virtual void MoveRight()
    {

    }
    //Moving to the left
    public virtual void MoveLeft()
    {

    }
    //Jumping
    public virtual void Jump()
    {

    }
    //Flipping the image across the Y axis
    public virtual void Flip(int direction)
    {
        Vector3 xscale;

        xscale = gameObject.transform.localScale;
        xscale.x = (float)direction;
        gameObject.transform.localScale = xscale;
    }
    //Function for walking animation
    public IEnumerator Walk()
    {
        // This will produce sound as the player walks
        if (step == false)
        {
            FindObjectOfType<AudioManager>().Play("Walking");
            step = true;
            yield return new WaitForSeconds((float)0.15);
            step = false;
        }
    }


    //This is the enemy's functions
    //Enemy's idle state function
    public virtual void Idle()
    {

    }
    //Enemy's chase state function
    public virtual void Chase()
    {

    }
    //The enemy's move towards the player function that grabs the players location
    public virtual void MoveTowards(Vector2 target)
    {

    }
    //The enemy's actual move function
    public virtual void Move(Transform target)
    {

    }
    //The enemy's attack function for the attack state
    public virtual void Attack()
    {

    }
    //The enemy's look around function for the look around state
    public virtual void LookAround()
    {

    }
    //This is used to delay the enemy's attack slightly
    IEnumerator Recoil()
    {
        yield return new WaitForSeconds(1f);
        canAttack = true;
    }

}
