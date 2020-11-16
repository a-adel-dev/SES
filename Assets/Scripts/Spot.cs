using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spot : MonoBehaviour
{
    public bool available = true;
    public AI agent { get; set; }


    public void SetAvailable(bool state)
    {
        available = state;
    }

    public string IsAvailableText()
    {
        if (available)
        {
            return "free";
        }
        else
        {
            return "not Available";
        }
    }
}
