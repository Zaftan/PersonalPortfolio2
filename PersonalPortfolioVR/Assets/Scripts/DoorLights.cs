using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLights : MonoBehaviour
{
    public static DoorLights instance;

    [SerializeField] List<GameObject> lights;
    int lightsOn;
    [SerializeField] bool isDoorOpen;

    [SerializeField] GameObject door;
    bool once = false;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if(lightsOn == 3)
        {
            isDoorOpen = true;

            print("Door is open");
        }

        if (isDoorOpen && !once)
        {
            door.transform.Rotate(0, 140, 0);
            once = true;
        }


    }

    public void TurnOnLight(int id)
    {
        if (id < lights.Count)
        {
            Debug.Log("turning on light");
            lights[id].GetComponent<lightScript>().isOn = true;
            lightsOn++;
        }
    }

    public void TurnOffLight(int id)
    {
        if (id < lights.Count)
        {
            Debug.Log("turning off light");
            lights[id].GetComponent<lightScript>().isOn = false;
            lightsOn--;
        }


    }

}
