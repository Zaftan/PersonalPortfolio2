using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLights : MonoBehaviour
{
    public static DoorLights instance;

    [SerializeField] List<GameObject> lights;
    int lightsOn;
    bool isDoorOpen;

    private void Start()
    {
            
    }

    private void Update()
    {
        if(lightsOn == 3)
        {
            isDoorOpen = true;
        }
    }

    public void TurnOnLight()
    {
        foreach (GameObject light in lights)
        {
            if(light.GetComponent<lightScript>().isOn == false)
            {
                light.GetComponent<lightScript>().isOn = true;
                lightsOn++;
                break;
            }
            else
            {
                return;
            }
        }
    }

    public void TurnOffLight()
    {
        if (lights[3].GetComponent<lightScript>().isOn)
        {
            lights[3].GetComponent<lightScript>().isOn = false;
            lightsOn--;
        } 
        else if (lights[2].GetComponent<lightScript>().isOn)
        {
            lights[2].GetComponent<lightScript>().isOn = false;
            lightsOn--;
        }
        else if (lights[1].GetComponent<lightScript>().isOn)
        {
            lights[1].GetComponent<lightScript>().isOn = false;
            lightsOn--;
        }
    }

}
