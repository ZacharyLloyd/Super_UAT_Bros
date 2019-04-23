using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Animations;

public class EnemyController : Controller
{
    bool isIdle = false; //Setting a bool for enemy idle
    bool isWalking = false; //Setting a bool for enemy walking
    bool isAttacking = false; //Setting a bool for enemy attacking
    public PlayerPawn player; //Referencing the player
    public Animator animator; //Setting a variable to refernece the animator
    public EnemyPawn epawn; //Setting a variable to reference the EnemyPawn

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        //player = FindObjectOfType<PlayerPawn>();
        player = GameManager.instance.player;
        epawn = GetComponent<EnemyPawn>();
    }

    // Update is called once per frame
    void Update()
    {
        epawn.animator.SetBool("EnemyIdle", isIdle); //Setting the bool for the animation
        epawn.animator.SetBool("EnemyWalk", isWalking); //Setting the bool for the animation
        epawn.animator.SetBool("EnemyAttack", isAttacking); //Setting the bool for the animation

        //AI states are based on enum values
        switch (epawn.currentState)
        {
            //Idle state
            case EnemyPawn.AIStates.Idle:
                epawn.Idle();

                //Check for transitions
                if (epawn.senses.CanSee(GameManager.instance.player.gameObject))
                {
                    isWalking = true;
                    epawn.currentState = EnemyPawn.AIStates.Chase;
                }
                else if (epawn.senses.CanHear(GameManager.instance.player.gameObject))
                {
                    epawn.currentState = EnemyPawn.AIStates.LookAround;
                }
                break;
            //Look around state
            case EnemyPawn.AIStates.LookAround:
                epawn.LookAround();
                if (epawn.senses.CanSee(GameManager.instance.player.gameObject))
                {
                    epawn.currentState = EnemyPawn.AIStates.Chase;
                }
                else if (!epawn.senses.CanHear(GameManager.instance.player.gameObject))
                {
                    epawn.currentState = EnemyPawn.AIStates.Idle;
                }
                break;
            //Chase state
            case EnemyPawn.AIStates.Chase:
                epawn.Chase();
                //Check for transitions
                if (Vector2.Distance(epawn.transform.position, GameManager.instance.player.transform.position) <= 2)
                {
                    isAttacking = true;
                    epawn.currentState = EnemyPawn.AIStates.Attack;
                }
                else if (!epawn.senses.CanSee(GameManager.instance.player.gameObject))
                {
                    isIdle = true;
                    epawn.currentState = EnemyPawn.AIStates.Idle;
                }
                break;
            //Attack state
            case EnemyPawn.AIStates.Attack:
                epawn.Attack();
                //Check for transitions
                if (Vector3.Distance(epawn.transform.position, GameManager.instance.player.transform.position) > 1)
                {
                    isWalking = true;
                    epawn.currentState = EnemyPawn.AIStates.Chase;
                }
                break;
        }
    }
}
