using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightScript : MonoBehaviour
{
    public bool isOn = false;

    private void Update()
    {
        if (isOn)
        {
            gameObject.GetComponent<Light>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<Light>().enabled = false;
        }
    }
}
