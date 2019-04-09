using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noisemaker : MonoBehaviour
{
    public float PlayerVolume = 0; //Setting the player volume to be adjusted by designers
    public float SlowDownVolume = 0.016f; //How the volume fades away to nothing

    // Update is called once per frame
    void Update()
    {
        //Each frame noise gets lower until it hits zero/nothing
        if (PlayerVolume > 0)
        {
            PlayerVolume -= SlowDownVolume;
        }
    }
}
