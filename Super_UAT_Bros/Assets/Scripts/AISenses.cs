using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISenses : MonoBehaviour
{
    //Values
    public float sightDistance = 10; //How far enemies can see
    public float fieldOfView = 60; //View angle
    public float hearingScale = 1.0f; //How well enemy can hear. 1.0 = normal hearing, otherwise there would be deafness/superhearing

    const float DebugAngleDistance = 2.0f;
    const float DegreesToRadians = Mathf.PI / 180.0f;
    private Transform tf;

    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
    }


    public bool CanHear(GameObject target)
    {
        //If the target does not have a noisemaker we cannot hear them
        Noisemaker targetNoiseMaker = target.GetComponent<Noisemaker>();
        if (targetNoiseMaker == null)
        {
            return false;
        }
        //If they do have a noisemaker, check distance -- if it is <= (PlayerVolume * hearingScale), then we can hear them
        Transform targetTf = target.GetComponent<Transform>();
        if (Vector3.Distance(tf.position, targetTf.position) <= targetNoiseMaker.PlayerVolume * hearingScale)
        {
            return true;
        }
        //Otherwise enemies cannot hear player
        return false;
    }
    public void DrawDebugAngle()
    {
        Vector3 perpendicularDirection = new Vector3(-tf.right.y, tf.right.x);
        float oppositeSideLength = Mathf.Tan(fieldOfView * 0.5f * DegreesToRadians) * DebugAngleDistance;
    }
    public bool CanSee(GameObject target)
    {
        //If they do not have a collider, they are invisible
        Collider2D targetCollider = target.GetComponent<Collider2D>();
        if (targetCollider == null)
        {
            return false;
        }

        //If they are outside the view angle, cannot be seen
        //To check, need the vector to the target, and compare that angle to the foward vector
        Transform targetTransform = target.GetComponent<Transform>();
        Vector3 vectorToTarget = targetTransform.position - tf.position;
        vectorToTarget.Normalize();

        DrawDebugAngle();
        if (Vector3.Angle(vectorToTarget, tf.right) >= fieldOfView)
        {
            return false;
        }
        // If the player is in field-of-view
        //Use raycast to make sure nothing is blocking the view

        RaycastHit2D hitInfo = Physics2D.Raycast(tf.position, tf.position + vectorToTarget, sightDistance);

        //If the raycase hits nothing, enemy cannot see player
        if (hitInfo.collider == null)
        {
            return false;
        }

        //If the raycast hits the player first, enemy can see them
        if (hitInfo.collider.name == "enemyShooter")
        {

            return true;
        }
        //Otherwise, if enemy raycast hitss somehting else we failed to see the player
        return false;
    }
}
