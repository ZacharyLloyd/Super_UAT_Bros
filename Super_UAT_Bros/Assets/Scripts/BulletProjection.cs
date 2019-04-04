using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjection : MonoBehaviour
{
    public float bulletVelocity; //How fast the bullet will go
    public Rigidbody2D rigidBody; //This in able to apply physics to bullet
    //before first frame
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = transform.right * bulletVelocity;
    }
}