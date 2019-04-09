using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPawn : Pawn
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        canShoot = true;

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

    public override void Turn(bool isTurnClockwise)
    {
        //Rotate based on turnSpeed and direction enemies are turning
        if (isTurnClockwise)
        {
            tf.Rotate(0, 0, turnSpeed * Time.deltaTime);
        }
        else
        {
            tf.Rotate(0, 0, -turnSpeed * Time.deltaTime);
        }
    }

    public override void Shoot()
    {
        //Look at target
        Vector3 vectorToTarget = GameManager.instance.player.tf.position - tf.position;
        tf.right = vectorToTarget;

        if (canShoot == true)
        {
            canShoot = false;
            coroutine = Recoil();
            Instantiate(enemyBulletPrefab, pointOfFire);
            enemyBulletPrefab.tag = "enemyBullet";
            StartCoroutine(coroutine);
        }

    }
    IEnumerator Recoil()
    {
        yield return new WaitForSeconds(1f);
        canShoot = true;
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
