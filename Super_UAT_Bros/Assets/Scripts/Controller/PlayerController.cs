using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    public static PlayerController controller;
    public PlayerPawn player;
    //Mapping Movement to selected keys
    public KeyCode right = KeyCode.D;
    public KeyCode left = KeyCode.A;
    public KeyCode jump = KeyCode.J;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start(); //Calling the parent start function
        if (controller == null)
            controller = this;
        player = FindObjectOfType<PlayerPawn>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // The player controller gets input from the keyboard and then moves the pawn.
        if (Input.GetKey(right))
            pawn.MoveRight();
        else if (Input.GetKey(left))
            pawn.MoveLeft();
        else
        {
            pawn.isWalking = false;
            //Set up bool for Animator
            pawn.animator.SetBool("IsWalking", pawn.isWalking);
        }

        if ((Input.GetKeyDown(jump) && PlayerPawn.__totalJumps != 0) && (Input.GetKey(right) == false && Input.GetKey(left) == false))
            pawn.Jump();

        if (PlayerPawn.__grounded == true)
        {
            PlayerPawn.__totalJumps = PlayerPawn.__setValue;
        }
    }
}
