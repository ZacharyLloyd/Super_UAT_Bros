using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPawn : Pawn
{

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        bulletPrefab.tag = "Bullet";
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void MoveRight()
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
}