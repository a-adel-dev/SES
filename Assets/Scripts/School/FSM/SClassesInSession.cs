using UnityEngine;
using System;
using SES.Core;

namespace SES.School
{
    public class SClassesInSession : SSchoolBaseState 
    {

        short sessionTimer = 0;
        public short sessionLength;
        public float timeStep;
        float timer = 0f;

        public override void EnterState(SchoolDayProgressionController progressionController)
        {
            sessionLength = progressionController.periodLength;
            timeStep = progressionController.timeStep;
            Debug.Log($"----------Classes In session----------");
            progressionController.StartPeriod();
        }

        public override void Update(SchoolDayProgressionController progressionController)
        {
            
            if (sessionTimer >= sessionLength)
            {
                sessionTimer = 0;
                progressionController.TransitionToState(progressionController.breakTime);
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
