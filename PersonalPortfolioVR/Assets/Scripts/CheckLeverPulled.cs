using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckLeverPulled : MonoBehaviour
{
    [SerializeField] int lightID;
    [SerializeField] TextMeshProUGUI UItext;
    [SerializeField] GameObject lever;

    [SerializeField] int time;
    [SerializeField] int currentTime = 0;
    private void Start()
    {
        currentTime = time;
    }

    private void Update()
    {
        UItext.text = currentTime.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "GrabIgnoreRay")
        {
            DoorLights.instance.TurnOnLight(lightID);
            StartCoroutine(Timer());
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "GrabIgnoreRay")
        {
            DoorLights.instance.TurnOffLight(lightID);
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1);
        currentTime--;
        
        if(currentTime != 0) { StartCoroutine(Timer()); }
        else
        {
            lever.transform.Rotate(new Vector3(0,0,120));
            currentTime = time;
        }

    }
}
