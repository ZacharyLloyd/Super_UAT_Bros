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
        //Does nothing
    }

    public override void Chase()
    {
        goalPoint = GameManager.instance.player.tf.position;
        MoveTowards(goalPoint);
    }

    public override void GoHome()
    {
        goalPoint = homePoint;
        MoveTowards(goalPoint);
    }

    public override void LookAround()
    {
        Turn(true);
    }

    public override void MoveTowards(Vector3 target)
    {
        if (Vector3.Distance(tf.position, target) > closeEnough)
        {
            //Look at target
            Vector3 vectorToTarget = target - tf.position;
            tf.right = vectorToTarget;

            //Move Forward
            Move(tf.right);
        }
    }

    public override void Move(Vector3 direction)
    {
        //Move in the direction passed through, at speed "moveSpeed"
        tf.position += (direction.normalized * moveSpeed * Time.deltaTime);
    }

    //Flipping the image across the Y axis
    public override void Flip(DIRECTION direction)
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

    public override void Attack()
    {
        //Look at target
        Vector3 vectorToTarget = GameManager.instance.player.tf.position - tf.position;
        tf.right = vectorToTarget;

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
