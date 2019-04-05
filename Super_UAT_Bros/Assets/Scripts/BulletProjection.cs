using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjection : MonoBehaviour
{
    public Player playerInstance; //Grabbing data from Player script to reference direction

    public float bulletVelocity; //How fast the bullet will go
    public Rigidbody2D rigidBody; //This in able to apply physics to bullet
    //before first frame
    void Awake()
    {
        playerInstance = FindObjectOfType<Player>();
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = transform.right * bulletVelocity * Mathf.Sign(playerInstance.transform.localScale.x);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Tilemap")
            Destroy(this.gameObject);
    }
}