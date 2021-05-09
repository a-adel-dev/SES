using UnityEngine;
using System;
using SES.Core;


namespace SES.School
{
    public class SEgressTime : SSchoolBaseState
    {
        int sessionLength = 20;
        int sessionTimer = 0;
        float timeStep;
        float timer = 0f;
        public override void EnterState(SchoolDayProgressionController progressionController)
        {
            //consider pausing
            timeStep = progressionController.timeStep;
            Debug.Log("-------------Egress Time-------------");
            progressionController.EgressClasses();
            progressionController.SchoolState = "Home time";
        }

        public override void Update(SchoolDayProgressionController progressionController)
        {

            
            if (sessionTimer >= sessionLength)
            {
                sessionTimer = 0;
                progressionController.TransitionToState(progressionController.offTime);
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