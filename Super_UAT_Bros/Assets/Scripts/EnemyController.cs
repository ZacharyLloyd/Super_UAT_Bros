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
                if (pawn.senses.CanSee(GameManager.instance.player.gameObject))
                {
                    pawn.currentState = Pawn.AIStates.Chase;
                }
                break;
            case Pawn.AIStates.Chase:
                pawn.Chase();
                //Check for transitions
                if (Vector3.Distance(pawn.tf.position, GameManager.instance.player.tf.position) > 16)
                {
                    pawn.currentState = Pawn.AIStates.Attack;
                }
                break;
            case Pawn.AIStates.Attack:
                pawn.Attack();
                //Check for transitions
                if (Vector3.Distance(pawn.tf.position, GameManager.instance.player.tf.position) > 24)
                {
                    //StopCoroutine(pawn.coroutine);
                    pawn.currentState = Pawn.AIStates.Chase;
                }
                break;
        }
    }
}
