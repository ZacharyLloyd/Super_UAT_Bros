using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPawn : Pawn
{

    public int totalJumps;
    public static int __totalJumps; // Number of jumps
    public static int __setValue; // Set value for number of jumps
    [HideInInspector] public int setValue;
    [HideInInspector] public static bool __grounded = true; //Is the player grounded
    public bool grounded;
    public float speed; //Speed for moving
    public float maxSpeed; //Max Speed
    public float jumpForce; // Amount of jump
    public float MoveVolume; //MoveVolume for noisemaker
    public Collider2D groundCheck; //Checking if player is grounded

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        bulletPrefab.tag = "Bullet";

        __animator = GetComponent<Animator>();

        animator = __animator;

        animator = GetComponent<Animator>();

        DontDestroyOnLoad(this);

        setValue = totalJumps;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        grounded = __grounded;
        if (Input.GetKey(PlayerController.right))
            MoveRight();
        else if (Input.GetKey(PlayerController.left))
            MoveLeft();
        else
        {
            isWalking = false;
            //Set up bool for Animator
            animator.SetBool("IsWalking", isWalking);
        }

        if ((Input.GetKeyDown(PlayerController.jump) && totalJumps != 0) && (Input.GetKey(PlayerController.right) == false && Input.GetKey(PlayerController.left) == false))
        {
            Jump();
        }

        if (grounded == true)
        {
            totalJumps = setValue;
        }

        if (GameManager.instance.currentHealth == 0)
        {
            GameManager.instance.GUI_ACTIVE = false;
            GameManager.instance.healthUIParent.gameObject.SetActive(GameManager.instance.GUI_ACTIVE);
            GameManager.instance.Scene_Name = "Lose Screen";
            SceneManager.LoadScene(GameManager.instance.Scene_Name);
            Destroy(gameObject);

        }
    }

    public override void MoveRight()
    {
        if (noisemaker != null)
        {
            noisemaker.PlayerVolume = Mathf.Max(noisemaker.PlayerVolume, MoveVolume);
        }
        isWalking = true;
        coroutine = Walk();
        //Set up bool for Animator
        animator.SetBool("IsWalking", isWalking);

        Flip(1);
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.velocity += new Vector2(speed, 0);
        }

        if (grounded == true) StartCoroutine(coroutine);

        if (Input.GetKeyDown(PlayerController.jump) && totalJumps != 0)
            Jump();
    }
    //Moving to the left
    public override void MoveLeft()
    {
        if (noisemaker != null)
        {
            noisemaker.PlayerVolume = Mathf.Max(noisemaker.PlayerVolume, MoveVolume);
        }

        isWalking = true;
        coroutine = Walk();
        //Set up bool for Animator
        animator.SetBool("IsWalking", isWalking);

        Flip(-1);
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.velocity += new Vector2(-speed, 0);
        }

        if (grounded == true) StartCoroutine(coroutine);

        if (Input.GetKeyDown(PlayerController.jump) && totalJumps != 0)
            Jump();
    }
    public override void Jump()
    {
        if (noisemaker != null)
        {
            noisemaker.PlayerVolume = Mathf.Max(noisemaker.PlayerVolume, MoveVolume);
        }

        coroutine = Walk();
        StopCoroutine(coroutine);
        maxSpeed = maxSpeed * 2;
        grounded = false;
        animator.SetBool("grounded", grounded);
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        FindObjectOfType<AudioManager>().Play("Jumping");
        --totalJumps;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameManager.instance.DecreaseHealth(1);
        }
    }
}