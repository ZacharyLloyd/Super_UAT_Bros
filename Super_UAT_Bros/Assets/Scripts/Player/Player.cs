using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*******NOT USED ANYMORE******
public class Player : MonoBehaviour
{
    public float speed; //Speed for moving
    public float maxSpeed; //Max Speed
    public float jumpForce; // Amount of jump
    public int totalJumps;
    public static int __totalJumps; // Number of jumps
    public static int __setValue; // Set value for number of jumps
    [HideInInspector] public int setValue;
    [HideInInspector] public bool isWalking; // Is player walking true/false
    [HideInInspector] public static bool grounded = false; //Is the player grounded
    [HideInInspector] public static Animator __animator; //The player's animator
    Animator animator; // Player's animator
    Rigidbody2D rb; // Player's rigidbody
    public Collider2D groundCheck; //Checking if player is grounded
    bool step = false; // Step is for detecting walking to play the sound
    public Transform tf;

    IEnumerator coroutine;

    //Mapping Movement to selected keys
    public KeyCode right = KeyCode.D;
    public KeyCode left = KeyCode.A;
    public KeyCode jump = KeyCode.J;

    //Flipping the image with this enumerator
    public enum DIRECTION
    {
        Left = -1,
        Right = 1
    }
    private void Awake()
    {
        tf = GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        __animator = GetComponent<Animator>();
        animator = __animator;
        animator = GetComponent<Animator>();
        setValue = totalJumps;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(right))
            MoveRight();
        else if (Input.GetKey(left))
            MoveLeft();
        else
        {
            isWalking = false;
            //Set up bool for Animator
            animator.SetBool("IsWalking", isWalking);
        }

        if ((Input.GetKeyDown(jump) && totalJumps != 0) && (Input.GetKey(right) == false && Input.GetKey(left) == false))
            Jump();

        if (grounded == true)
        {
            totalJumps = setValue;
        }
    }
    private void FixedUpdate()
    {
        
    }
    //Moving to the right
    public void MoveRight()
    {

        coroutine = Walk();

        isWalking = true;
        //Set up bool for Animator
        animator.SetBool("IsWalking", isWalking);

        Flip(1);
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.velocity += new Vector2(speed, 0);
        }

        if (grounded == true) StartCoroutine(coroutine);

        if (Input.GetKeyDown(jump) && totalJumps != 0)
            Jump();
    }
    //Moving to the left
    public void MoveLeft()
    {
        coroutine = Walk();

        isWalking = true;
        //Set up bool for Animator
        animator.SetBool("IsWalking", isWalking);

        Flip(-1);
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.velocity += new Vector2(-speed, 0);
        }

        if (grounded == true) StartCoroutine(coroutine);

        if (Input.GetKeyDown(jump) && totalJumps != 0)
            Jump();
    }
    //Jumping
    public void Jump()
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
    public void Flip(int direction)
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
}
