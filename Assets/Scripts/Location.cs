using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    public SpotManager spotManager = new SpotManager();
    public string type { get; private set; }

    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Spot>())
            {
                spotManager.AddSpot(child.GetComponent<Spot>());
            }
        }
        AssignType();
    }

    private void AssignType()
    {
        if (CompareTag("Board"))
        {
            type = "Board";
        }
        else if (CompareTag("Locker"))
        {
            type = "Locker";
        }
        else if (CompareTag("WC"))
        {
            type = "WC";
        }
        else
        {
            type = "unidentified";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
