using UnityEngine;
using System;
using SES.Core;


namespace SES.School
{
    public class SEgressTime : SSchoolBaseState
    {
        short sessionLength = 20;
        short sessionTimer = 0;
        float timeStep;
        float timer = 0f;
        public override void EnterState(SchoolDayProgressionController progressionController)
        {
            timeStep = progressionController.timeStep;
            Debug.Log("-------------Egress Time-------------");
            progressionController.EgressClasses();
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