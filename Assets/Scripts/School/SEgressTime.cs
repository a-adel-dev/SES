using UnityEngine;
using System;


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
                PassTime(progressionController);
            }
        }

        private void PassTime(SchoolDayProgressionController progressionController)
        {
            timer += Time.deltaTime;
            if (timer >= timeStep)
            {
                timer -= timeStep;
                sessionTimer++;
                progressionController.timeRecorder.UpdateSchoolTime(new TimeSpan(0, 1, 0));
            }
        }
    }
}