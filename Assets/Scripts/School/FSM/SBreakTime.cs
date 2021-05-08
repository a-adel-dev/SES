﻿using UnityEngine;
using System;
using SES.Core;


namespace SES.School
{
    public class SBreakTime : SSchoolBaseState
    {
        short sessionTimer = 0;
        short periodIndex = 0;
        short sessionLength , numPeriods;
        float timeStep = .5f;
        float timer = 0f;
        

        public override void EnterState(SchoolDayProgressionController progressionController)
        {
            numPeriods = progressionController.numPeriods;
            timeStep = progressionController.timeStep;
            sessionLength = progressionController.breakLength;
            Debug.Log($"---------------BreakTime--------------");
            progressionController.EndPeriod();
        }

        public override void Update(SchoolDayProgressionController progressionController)
        {
            if (sessionTimer >= sessionLength)
            {
                sessionTimer = 0;
                periodIndex++;
                if (periodIndex == numPeriods)
                {
                    sessionTimer = 0;
                    periodIndex = 0;
                    progressionController.TransitionToState(progressionController.egressTime);
                }
                else
                {
                    progressionController.TransitionToState(progressionController.classesInSession);
                }
            }
            else
            {
                PassTime();
            }
        }
        private void PassTime()
        {
            timer += Time.deltaTime;
            if (timer >= timeStep)
            {
                timer -= timeStep;

                sessionTimer++;
                DateTimeRecorder.UpdateSchoolTime(new TimeSpan(0, 1, 0));
            }
        }
    }
}