using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    //Component
    private AISenses senses;

    //FSM
    public enum AIStates
    {
        Idle,
        Chase,
        Attack
    }
    public Vector3 homePoint;
    public Vector3 goalPoint;
    public AIStates currentState;
    public float stopChaseDistance;
    public float closeEnough;

    public float moveSpeed = 1;
    Transform tf;
    public Animator animator;
    public bool canAttack = true;
    IEnumerator coroutine;

    // Use this for initialization
    void Start()
    {
        // Store my senses component
        senses = GetComponent<AISenses>();

        tf = GetComponent<Transform>();

        // Save home point
        homePoint = tf.position;
    }

    //Update is called once per frame
    void Update()
    {

        tf = gameObject.transform;
        //AI states are based on enum values
        switch (currentState)
        {
            case AIStates.Idle:
                Idle();
                //Check for transitions
                if (senses.CanSee(GameManager.instance.player.gameObject))
                {
                    currentState = AIStates.Chase;
                }
                break;
            case AIStates.Chase:
                Chase();
                //Check for transitions
                if (Vector3.Distance(tf.position, GameManager.instance.player.tf.position) <= stopChaseDistance)
                {
                    currentState = AIStates.Attack;
                }
                break;
            case AIStates.Attack:
                Attack();
                //Check for transitions
                if (Vector3.Distance(tf.position, GameManager.instance.player.tf.position) > stopChaseDistance / 2)
                {
                    //StopCoroutine(pawn.coroutine);
                    currentState = AIStates.Chase;
                }
                /*if (Vector3.Distance(pawn.tf.position, GameManager.instance.player.tf.position) > pawn.stopChaseDistance)
                {
                    pawn.currentState = Pawn.AIStates.GoHome;
                }*/
                break;


        }
    }
    IEnumerator Recoil()
    {
        yield return new WaitForSeconds(1f);
        canAttack = true;
    }
    public void Idle()
    {
        //Does nothing
    }

    public void Chase()
    {
        goalPoint = GameManager.instance.player.tf.position;
        MoveTowards(goalPoint);
    }

    public void MoveTowards(Vector3 target)
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

    public void Move(Vector3 direction)
    {
        //Move in the direction passed through, at speed "moveSpeed"
        tf.position += (direction.normalized * moveSpeed * Time.deltaTime);
    }

    public void Attack()
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

}
