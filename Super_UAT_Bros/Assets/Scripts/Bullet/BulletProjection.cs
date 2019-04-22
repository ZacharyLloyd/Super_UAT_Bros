using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjection : MonoBehaviour
{
    public PlayerPawn playerInstance; //Grabbing data from Player script to reference direction

    public float bulletVelocity; //How fast the bullet will go
    public Rigidbody2D rigidBody; //This in able to apply physics to bullet
    //before first frame
    void Awake()
    {
        playerInstance = FindObjectOfType<PlayerPawn>(); //Getting the player
        rigidBody = GetComponent<Rigidbody2D>(); //Getting the player's rigid body
        rigidBody.velocity = transform.right * bulletVelocity * Mathf.Sign(playerInstance.transform.localScale.x);
    }
    //Destroy bullet if it hits the tilemap
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Tilemap")
            Destroy(this.gameObject);
    }
}