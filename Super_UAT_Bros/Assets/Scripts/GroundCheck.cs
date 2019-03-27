using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Tilemap")
        {
            Player.grounded = true;
            Player.__animator.SetBool("grounded", Player.grounded);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Tilemap")
        {
            Player.grounded = false;
            Player.__animator.SetBool("grounded", Player.grounded);
        }
    }

}
