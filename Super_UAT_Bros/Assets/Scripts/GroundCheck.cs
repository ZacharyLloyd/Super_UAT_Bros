using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Tilemap")
        {
            PlayerPawn.__grounded = true;
            Pawn.__animator.SetBool("grounded", PlayerPawn.__grounded);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Tilemap")
        {
            PlayerPawn.__grounded = false;
            Pawn.__animator.SetBool("grounded", PlayerPawn.__grounded);
        }
    }

}
