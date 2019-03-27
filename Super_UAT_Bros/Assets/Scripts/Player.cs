using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed; //Speed for moving
    public float maxSpeed; //Max Speed
    public float jumpForce; // Amount of jump
    [HideInInspector] public bool isWalking; // Is player walking true/false
    [HideInInspector] public static bool grounded = false; //Is the player grounded
    [HideInInspector] public static Animator __animator; //The player's animator
    Animator animator; // Player's animator
    Rigidbody2D rb; // Player's rigidbody
    public Collider2D groundCheck; //Checking if player is grounded

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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        __animator = GetComponent<Animator>();
        animator = __animator;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Is grounded" + grounded);
    
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(right))
            MoveRight();
        else if (Input.GetKey(left))
            MoveLeft();
        else if (Input.GetKey(jump) && grounded == true)
            Jump();
        else
        {
            isWalking = false;
            //Set up bool for Animator
            animator.SetBool("IsWalking", isWalking);
        }
    }
    //Moving to the right
    public void MoveRight()
    {
        isWalking = true;
        //Set up bool for Animator
        animator.SetBool("IsWalking", isWalking);

        Flip(DIRECTION.Right);
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.velocity += new Vector2(speed, 0);
        }
        if (Input.GetKey(jump) && grounded == true)
            Jump();
    }
    //Moving to the left
    public void MoveLeft()
    {
        isWalking = true;
        //Set up bool for Animator
        animator.SetBool("IsWalking", isWalking);

        Flip(DIRECTION.Left);
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.velocity += new Vector2(-speed, 0);
        }
        if (Input.GetKey(jump) && grounded == true)
            Jump();
    }
    //Jumping
    public void Jump()
    {
        maxSpeed = maxSpeed * 2;
        grounded = false;
        animator.SetBool("grounded", grounded);
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    //Flipping the image across the Y axis
    public void Flip(DIRECTION direction)
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
}
