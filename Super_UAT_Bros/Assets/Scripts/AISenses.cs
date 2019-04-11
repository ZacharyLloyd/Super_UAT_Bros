using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISenses : MonoBehaviour
{
    //Values
    public float sightDistance = 10; //How far enemies can see
    public float fieldOfView = 60; //View angle
    public float hearingScale = 1.0f; //How well enemy can hear. 1.0 = normal hearing, otherwise there would be deafness/superhearing
    const float DegreesToRadians = Mathf.PI / 180.0f;
    private Transform tf;

    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
    }


    public bool CanHear(GameObject target)
    {
        ////If the target does not have a noisemaker we cannot hear them
        //Noisemaker targetNoiseMaker = target.GetComponent<Noisemaker>();
        //if (targetNoiseMaker == null)
        //{
        //    return false;
        //}
        ////If they do have a noisemaker, check distance -- if it is <= (PlayerVolume * hearingScale), then we can hear them
        //Transform targetTf = target.GetComponent<Transform>();
        //if (Vector3.Distance(tf.position, targetTf.position) <= targetNoiseMaker.PlayerVolume * hearingScale)
        //{
        //    return true;
        //}
        ////Otherwise enemies cannot hear player
        return false;
    }
    public bool CanSee(GameObject target)
    {
        if (Vector3.Distance(target.transform.position, tf.position) < 96)
        {
            return true;
        }

        return false;
    //    //If they do not have a collider, they are invisible
    //    Collider2D targetCollider = target.GetComponent<Collider2D>();
    //    if (targetCollider == null)
    //    {
    //        return false;
    //    }

    //    //If they are outside the view angle, cannot be seen
    //    //To check, need the vector to the target, and compare that angle to the foward vector
    //    Transform targetTransform = target.GetComponent<Transform>();
    //    Vector3 vectorToTarget = targetTransform.position - tf.position;
    //    vectorToTarget.Normalize();

    //    // If the player is in field-of-view
    //    //Use raycast to make sure nothing is blocking the view

    //    RaycastHit2D hitInfo = Physics2D.Raycast(tf.position, tf.position + vectorToTarget, sightDistance);

    //    //If the raycase hits nothing, enemy cannot see player
    //    if (hitInfo.collider == null)
    //    {
    //        return false;
    //    }

    //    //If the raycast hits the player first, enemy can see them
    //    if (hitInfo.collider.name == "enemy")
    //    {

    //        return true;
    //    }
    //    //Otherwise, if enemy raycast hitss somehting else we failed to see the player
    //    return false;
    }
}
