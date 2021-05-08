using UnityEngine;
using System;

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
