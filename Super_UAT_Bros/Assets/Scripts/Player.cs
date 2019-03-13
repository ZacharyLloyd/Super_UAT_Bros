using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed; //Speed for moving
    public float maxSpeed; //Max Speed
    public float jumpForce; // Amount of jump
    [HideInInspector] public bool isWalking; // Is player walking true/false
    Animator animator; // Player's animator
    Rigidbody2D rb; // Player's rigidbody

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
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Set up bool for Animator
        animator.SetBool("isWalking", isWalking);
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(right))
            MoveRight();
        if (Input.GetKey(left))
            MoveLeft();
        if (Input.GetKey(jump))
            Jump();
        else isWalking = false;
    }
    //Moving to the right
    public void MoveRight()
    {
        isWalking = true;

        Flip(DIRECTION.Right);
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.velocity += new Vector2(speed, 0);
        }
    }
    //Moving to the left
    public void MoveLeft()
    {
        isWalking = true;
        Flip(DIRECTION.Left);
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.velocity += new Vector2(-speed, 0);
        }
    }
    //Jumping ***THIS IS EMPTY FOR NOW***
    public void Jump()
    {

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
