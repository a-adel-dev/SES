using UnityEngine;
using System;
using SES.Core;


namespace SES.School
{
    public class SBreakTime : SSchoolBaseState
    {
        int sessionTimer = 0;
        int periodIndex = 0;
        int sessionLength , numPeriods;
        float timeStep = .5f;
        float timer = 0f;

        public override void EnterState(SchoolDayProgressionController progressionController)
        {
            if (resumed == false)
            {
                numPeriods = progressionController.numPeriods;
                timeStep = progressionController.timeStep;
                sessionLength = progressionController.breakLength;
                //Debug.Log($"---------------Break Time--------------");
                progressionController.EndPeriod();
                progressionController.ReplaceClassTeahers();
            }
            else
            {
                //Debug.Log($"----------Resuming Break Time--------------");
                progressionController.ResumeClasses();
            }
            progressionController.SchoolState = "Break Time";
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
                    resumed = false;
                    progressionController.TransitionToState(progressionController.egressTime);
                }
                else
                {
                    resumed = false;
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
