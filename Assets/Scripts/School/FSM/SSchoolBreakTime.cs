using UnityEngine;
using System;
using SES.Core;


namespace SES.School
{
    public class SSchoolBreakTime : SSchoolBaseState
    {
        int sessionTimer = 0;
        int periodIndex = 0;
        float timer = 0f;

        public override void EnterState(SchoolDayProgressionController progressionController)
        {
            if (resumed == false)
            {
                //Debug.Log($"---------------Break Time--------------");
                progressionController.EndPeriod();
                //progressionController.ReplaceClassTeahers();
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
            if (sessionTimer >= SimulationParameters.breakLength)
            {
                sessionTimer = 0;
                periodIndex++;
                if (periodIndex == SimulationParameters.numPeriods)
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
            if (timer >= SimulationParameters.TimeStep)
            {
                timer -= SimulationParameters.TimeStep;
                sessionTimer++;
                DateTimeRecorder.UpdateSchoolTime(new TimeSpan(0, 1, 0));
            }
        }
    }
}
