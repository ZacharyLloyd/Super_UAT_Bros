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
        LookAround,
        GoHome,
        Shoot
    }
    public Vector3 homePoint;
    public Vector3 goalPoint;
    public AIStates currentState;
    public float stopChaseDistance;
    public float closeEnough;

    public float moveSpeed = 1;
    public float turnSpeed = 1;
    Transform tf;

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
                if (senses.CanHear(GameManager.instance.player.gameObject))
                {
                    currentState = AIStates.LookAround;
                }
                if (senses.CanSee(GameManager.instance.player.gameObject))
                {
                    currentState = AIStates.Chase;
                }
                break;
            case AIStates.Chase:
                Chase();
                //Check for transitions
                if (!senses.CanSee(GameManager.instance.player.gameObject))
                {
                    currentState = AIStates.LookAround;
                }
                if (Vector3.Distance(tf.position, GameManager.instance.player.tf.position) > stopChaseDistance)
                {
                    currentState = AIStates.GoHome;
                }
                if (Vector3.Distance(tf.position, GameManager.instance.player.tf.position) <= stopChaseDistance)
                {
                    currentState = AIStates.Shoot;
                }
                break;
            case AIStates.LookAround:
                LookAround();
                //Check for transitions
                if (senses.CanSee(GameManager.instance.player.gameObject))
                {
                    currentState = AIStates.Shoot;

                }
                else if (Vector3.Distance(tf.position, GameManager.instance.player.tf.position) > stopChaseDistance)
                {
                    currentState = AIStates.GoHome;
                }
                else if (!senses.CanHear(GameManager.instance.player.gameObject))
                {
                    currentState = AIStates.GoHome;
                }
                break;
            case AIStates.GoHome:
                GoHome();
                //Check for transitions
                if (senses.CanHear(GameManager.instance.player.gameObject))
                {
                    currentState = AIStates.LookAround;
                }
                if (senses.CanSee(GameManager.instance.player.gameObject))
                {
                    currentState = AIStates.Chase;
                }
                if (Vector3.Distance(tf.position, homePoint) <= closeEnough)
                {
                    currentState = AIStates.Idle;
                }
                break;


        }
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

    public void GoHome()
    {
        goalPoint = homePoint;
        MoveTowards(goalPoint);
    }

    public void LookAround()
    {
        Turn(true);
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

    public void Turn(bool isTurnClockwise)
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

}
