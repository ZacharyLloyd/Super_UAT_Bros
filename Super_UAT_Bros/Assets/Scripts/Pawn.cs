using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    public float speed; //Speed for moving
    public float maxSpeed; //Max Speed
    public float jumpForce; // Amount of jump
    public int totalJumps;
    public static int __totalJumps; // Number of jumps
    public static int __setValue; // Set value for number of jumps
    [HideInInspector] public int setValue;
    [HideInInspector] public bool isWalking; // Is player walking true/false
    [HideInInspector] public static bool __grounded = false; //Is the player grounded
    public bool grounded;
    [HideInInspector] public static Animator __animator; //The player's animator
    public Animator animator; // Player's animator
    public Rigidbody2D rb; // Player's rigidbody
    public Collider2D groundCheck; //Checking if player is grounded
    bool step = false; // Step is for detecting walking to play the sound
    public Transform tf;
    public Noisemaker noisemaker;

    public IEnumerator coroutine;
    
    //Flipping the image with this enumerator
    public enum DIRECTION
    {
        Left = -1,
        Right = 1
    }


    //AI Component
    public AISenses senses;

    //FSM
    public enum AIStates
    {
        Idle,
        Chase,
        LookAround,
        GoHome,
        Attack
    }
    public Vector3 homePoint;
    public Vector3 goalPoint;
    public AIStates currentState;
    public float stopChaseDistance;
    public float closeEnough;

    public float moveSpeed = 1;

    public GameObject bulletPrefab;
    public bool canAttack = true;
    public Transform pointOfFire;

    // Start is called before the first frame update
    public virtual void Start()
    {

        grounded = __grounded;
        // Store my senses component
        senses = GetComponent<AISenses>();

        tf = GetComponent<Transform>();

        // Load noisemaker
        noisemaker = GetComponent<Noisemaker>();

        // Save home point
        homePoint = tf.position;
    }

    public virtual void Update()
    {

    }

    //This is the player's functions
    //Moving to the right
    public virtual void MoveRight()
    {
        coroutine = Walk();

        isWalking = true;
        //Set up bool for Animator
        animator.SetBool("IsWalking", isWalking);

        Flip(DIRECTION.Right);
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.velocity += new Vector2(speed, 0);
        }

        if (grounded == true) StartCoroutine(coroutine);

        if (Input.GetKeyDown(PlayerController.jump) && totalJumps != 0)
            Jump();
    }
    //Moving to the left
    public virtual void MoveLeft()
    {
        coroutine = Walk();

        isWalking = true;
        //Set up bool for Animator
        animator.SetBool("IsWalking", isWalking);

        Flip(DIRECTION.Left);
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.velocity += new Vector2(-speed, 0);
        }

        if (grounded == true) StartCoroutine(coroutine);

        if (Input.GetKeyDown(PlayerController.jump) && totalJumps != 0)
            Jump();
    }
    //Jumping
    public virtual void Jump()
    {
        coroutine = Walk();
        StopCoroutine(coroutine);

        maxSpeed = maxSpeed * 2;
        grounded = false;
        animator.SetBool("grounded", grounded);
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        FindObjectOfType<AudioManager>().Play("Jumping");
        --totalJumps;
    }
    //Flipping the image across the Y axis
    public virtual void Flip(DIRECTION direction)
    {
        Vector3 xscale;
        switch (direction)
        {
            case DIRECTION.Right:
                xscale = gameObject.transform.localScale;
                xscale.x = (float)DIRECTION.Right;
                gameObject.transform.localScale = xscale;
                break;
            case DIRECTION.Left:
                xscale = gameObject.transform.localScale;
                xscale.x = (float)DIRECTION.Left;
                gameObject.transform.localScale = xscale;
                break;
        }
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
    public virtual void GoHome()
    {

    }
    public virtual void LookAround()
    {

    }
    public virtual void MoveTowards(Vector3 target)
    {

    }
    public virtual void Move(Vector3 direction)
    {

    }
    public virtual void Turn(bool isTurnClockwise)
    {

    }
    public virtual void Attack()
    {

    }
    IEnumerator Recoil()
    {
        yield return new WaitForSeconds(1f);
        canAttack = true;
    }

}
