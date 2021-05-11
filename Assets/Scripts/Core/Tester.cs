//using SES.AIControl;
using SES.Core;
//using SES.Spaces;
//using System;
//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class TesterC : MonoBehaviour
{
    private void Start()
    {
        TimeStepController.TimeStep += RegisterTime;
    }


    private void OnDisable()
    {
        TimeStepController.TimeStep -= RegisterTime;
    }

    public void RegisterTime()
    {
        Debug.Log("Time Registered");
    }
}
