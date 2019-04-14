using System.Collections;
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


    public IEnumerator coroutine;

    //Enemy parts
    //AI Component
    public AISenses senses;

    //FSM
    public enum AIStates
    {
        Idle,
        LookAround,
        Chase,
        Attack
    }

    public AIStates currentState;
    public float closeEnough;
    public float moveSpeed = 1;
    public bool canAttack = false;

    // Start is called before the first frame update
    public virtual void Start()
    {
        bulletPrefab.tag = "Bullet";
        
        // Store my senses component
        senses = GetComponent<AISenses>();

        tf = GetComponent<Transform>();

        rb = GetComponent<Rigidbody2D>();

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
    public virtual void Idle()
    {

    }
    public virtual void Chase()
    {

    }
    public virtual void MoveTowards(Vector2 target)
    {

    }
    public virtual void Move(Transform target)
    {

    }
    public virtual void Attack()
    {

    }
    public virtual void LookAround()
    {

    }
    IEnumerator Recoil()
    {
        yield return new WaitForSeconds(1f);
        canAttack = true;
    }

}
