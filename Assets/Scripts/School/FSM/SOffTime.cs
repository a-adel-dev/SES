﻿using UnityEngine;
using System;
using SES.Core;



namespace SES.School
{
    public class SOffTime : SSchoolBaseState
    {
        short simLength;
        short progressionIndex = 0;

        public override void EnterState(SchoolDayProgressionController progressionController)
        {
            simLength = progressionController.simLength;
            Debug.Log($"---------------offtime----------------");
            Debug.Log($"Day {progressionIndex + 1} is over.");
            TimeSpan skippingTime = DateTimeRecorder.SkipToNextDay();
            Debug.Log(string.Format("{0:c} have been skipped", skippingTime));
            Debug.Log("----------------------------------------");
            //calculate Air dissipation in classes and add it to SpaceHealthControl
        }

        public override void Update(SchoolDayProgressionController progressionController)
        {
            progressionIndex++;
            if (progressionIndex < simLength)
            {
                progressionController.TransitionToState(progressionController.classesInSession);
            }
            else
            {
                progressionController.TransitionToState(progressionController.simOver);
            }
        }


    }
}