using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Controller
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        //AI states are based on enum values
        switch (pawn.currentState)
        {
            case Pawn.AIStates.Idle:
                pawn.Idle();

                //Check for transitions
                if (pawn.senses.CanHear(GameManager.instance.player.gameObject))
                {
                    pawn.currentState = Pawn.AIStates.LookAround;
                }
                if (pawn.senses.CanSee(GameManager.instance.player.gameObject))
                {
                    pawn.currentState = Pawn.AIStates.Shoot;
                }
                break;
            case Pawn.AIStates.Chase:
                pawn.Chase();
                //Check for transitions
                if (!pawn.senses.CanSee(GameManager.instance.player.gameObject))
                {
                    pawn.currentState = Pawn.AIStates.LookAround;
                }
                if (Vector3.Distance(pawn.tf.position, GameManager.instance.player.tf.position) > pawn.stopChaseDistance)
                {
                    pawn.currentState = Pawn.AIStates.GoHome;
                }
                if (Vector3.Distance(pawn.tf.position, GameManager.instance.player.tf.position) < pawn.stopChaseDistance / 2)
                {
                    pawn.currentState = Pawn.AIStates.Shoot;
                }
                break;
            case Pawn.AIStates.LookAround:
                pawn.LookAround();
                //Check for transitions
                if (pawn.senses.CanSee(GameManager.instance.player.gameObject))
                {
                    pawn.currentState = Pawn.AIStates.Chase;
                }
                else if (Vector3.Distance(pawn.tf.position, GameManager.instance.player.tf.position) < pawn.stopChaseDistance / 2)
                {
                    pawn.currentState = Pawn.AIStates.Shoot;
                }
                else if (!pawn.senses.CanHear(GameManager.instance.player.gameObject))
                {
                    pawn.currentState = Pawn.AIStates.GoHome;
                }
                break;
            case Pawn.AIStates.GoHome:
                pawn.GoHome();
                //Check for transitions
                if (pawn.senses.CanHear(GameManager.instance.player.gameObject))
                {
                    pawn.currentState = Pawn.AIStates.LookAround;
                }
                if (pawn.senses.CanSee(GameManager.instance.player.gameObject))
                {
                    pawn.currentState = Pawn.AIStates.Chase;
                }
                if (Vector3.Distance(pawn.tf.position, pawn.homePoint) <= pawn.closeEnough)
                {
                    pawn.currentState = Pawn.AIStates.Idle;
                }
                break;
            case Pawn.AIStates.Shoot:
                pawn.Shoot();
                //Check for transitions
                if (Vector3.Distance(pawn.tf.position, GameManager.instance.player.tf.position) > pawn.stopChaseDistance / 2)
                {
                    //StopCoroutine(pawn.coroutine);
                    pawn.currentState = Pawn.AIStates.Chase;
                }
                if (!pawn.senses.CanSee(GameManager.instance.player.gameObject))
                {
                    pawn.currentState = Pawn.AIStates.LookAround;
                }
                /*if (Vector3.Distance(pawn.tf.position, GameManager.instance.player.tf.position) > pawn.stopChaseDistance)
                {
                    pawn.currentState = Pawn.AIStates.GoHome;
                }*/
                break;
        }
    }
}
