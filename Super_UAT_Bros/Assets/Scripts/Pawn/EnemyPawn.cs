using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPawn : Pawn
{
    //FSM
    //Different states
    public enum AIStates
    {
        Idle,
        LookAround,
        Chase,
        Attack
    }
    public AIStates currentState; //Setting the current state of the enemy
    [HideInInspector]public Winning winning; //Winninf factor reference

    // Start is called before the first frame update
    public override void Start()
    {
        canAttack = true;
        winning = FindObjectOfType<Winning>();
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
    //Idle function
    public override void Idle()
    {
        //Does nothing but play animation
        animator.SetBool("EnemyIdle", true);
        animator.SetBool("EnemyWalk", false);
        animator.SetBool("EnemyAttack", false);
        rb.velocity = Vector2.zero; //This was used to enesure the enemy stops moving in idle
    }
    //Chase function
    public override void Chase()
    {
        MoveTowards(GameManager.instance.player.tf.position);
    }
    //MoveTowards function for enemy to move towards the player by taking the player's position
    public override void MoveTowards(Vector2 target)
    {
        if (Vector2.Distance(transform.position, target) <= closeEnough)
        {
            Move(GameManager.instance.player.transform);
        }
    }
    //Move function for the enemy so they can move
    public override void Move(Transform target)
    {
        if (target.position.x < tf.position.x)
        {
            Flip(-1);
            transform.Translate(-moveSpeed * Time.deltaTime, 0,0);
        }
        else if (target.position.x > tf.position.x)
        {
            Flip(1);
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
        }
        animator.SetBool("EnemyIdle", false);
        animator.SetBool("EnemyWalk", true);
    }
    //Attack function so the enemy can kill the player
    public override void Attack()
    {
        //Attack player
        animator.SetBool("EnemyWalk", false);
        animator.SetBool("EnemyAttack", true);
    }
    //LookAround function for hearing to transfer to seeing
    public override void LookAround()
    {
        Flip(1 * (int)Mathf.Sign(tf.localScale.x));
    }
    //Destroying enemy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (winning != null)
            {
                winning.enemiesToBeKilled--;
            }
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
