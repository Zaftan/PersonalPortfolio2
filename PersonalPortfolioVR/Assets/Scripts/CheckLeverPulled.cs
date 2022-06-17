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
        Debug.Log("entered");
        if(other.tag == "GrabIgnoreRay")
        {
            Debug.Log("entered+");
            DoorLights.instance.TurnOnLight(lightID);
            StartCoroutine(Timer());
        }
    }


    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited");
        if (other.tag == "GrabIgnoreRay")
        {
            Debug.Log("Exited+");
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
            lever.transform.Rotate(0, -140, 0);
            currentTime = time;
        }

    }
}
