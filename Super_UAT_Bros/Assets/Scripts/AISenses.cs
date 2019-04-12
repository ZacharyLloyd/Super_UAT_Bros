using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISenses : MonoBehaviour
{
    public float hearingScale = 1.0f; //How well enemy can hear. 1.0 = normal hearing, otherwise there would be deafness/superhearing
    private Transform tf;
    public float distance;

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
    public bool CanSee(GameObject target)
    {
        Vector3 length = new Vector3(distance * Mathf.Sign(tf.localScale.x), 0, 0);
        RaycastHit2D hitInfo = Physics2D.Raycast(tf.position, tf.position + length);
        if (hitInfo.collider.tag == "Player")
        {
            return true;
        }
        return false;
    }
}
