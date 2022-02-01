using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLeverPulled : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Grab Ignore Ray"))
        {
            DoorLights.instance.TurnOnLight();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Grab Ignore Ray"))
        {
            DoorLights.instance.TurnOffLight();
        }
    }
}
