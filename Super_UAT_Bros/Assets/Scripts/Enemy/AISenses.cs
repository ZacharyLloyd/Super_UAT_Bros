using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISenses : MonoBehaviour
{
    public float hearingScale = 1.0f; //How well enemy can hear. 1.0 = normal hearing, otherwise there would be deafness/superhearing
    private Transform tf; //Creating a variable to hold to transform of the enemy
    public float distance; //Vairable to change the distance the enemy can see

    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
    }
    //Function allowing the enemy to hear
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
    //Function to alloow enemy to see
    public bool CanSee(GameObject target)
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(tf.position, -tf.right, distance);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.tag == "Player")
            {
                return true;
            } 
        }
        return false;
    }
}
