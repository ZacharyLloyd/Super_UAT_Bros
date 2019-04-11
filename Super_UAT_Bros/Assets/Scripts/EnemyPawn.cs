using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPawn : Pawn
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        canAttack = true;

    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void Idle()
    {
        //Does nothing but play animation
        animator.SetBool("EnemyIdle", true);
    }

    public override void Chase()
    {
        MoveTowards(GameManager.instance.player.tf.position);
    }

    public override void MoveTowards(Vector2 target)
    {
        
        if (Vector2.Distance(tf.position, target) > closeEnough)
        {
            Move(GameManager.instance.player.tf);
        }
    }

    public override void Move(Transform target)
    {
        rb.velocity = new Vector2(moveSpeed * Mathf.Sign(tf.localScale.x), 0);
        if (target.position.x < tf.position.x)
        {
            Flip(DIRECTION.Left);
        } else if (target.position.x > tf.position.x)
        {
            Flip(DIRECTION.Right);
        }
        animator.SetBool("EnemyWalk", true);
    }

    public override void Attack()
    {
        //Look at target
        Vector3 vectorToTarget = GameManager.instance.player.tf.position - tf.position;
        tf.right = vectorToTarget;
        animator.SetBool("EnemyAttack", true);
        if (canAttack == true)
        {
            canAttack = false;
            coroutine = Recoil();
            StartCoroutine(coroutine);
        }

    }
    IEnumerator Recoil()
    {
        yield return new WaitForSeconds(1f);
        canAttack = true;
    }
    //Destroying enemy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            ++GameManager.instance.enemiesKilled;
            Destroy(gameObject);
        }
    }
}
