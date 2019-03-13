using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed; //Speed for moving
    public float maxSpeed; //Max Speed
    public float jump; // Amount of jump
    [HideInInspector] public bool isWalking; // Is player walking true/false
    Animator animator; // Player's animator
    Rigidbody2D rb; // Player's rigidbody

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
