using UnityEngine;
using System;
using SES.Core;

namespace SES.School
{
    public class SSchoolClassesInSession : SSchoolBaseState 
    {

        int sessionTimer = 0;
        float timer = 0f;

        public override void EnterState(SchoolDayProgressionController progressionController)
        {
            if (resumed == false)
            {
                progressionController.StartPeriod();
            }
            else
            {
                progressionController.ResumeClasses();
            }
            progressionController.SchoolState = "Classes in session";
        }

        public override void Update(SchoolDayProgressionController progressionController)
        {
            if (sessionTimer >= SimulationParameters.periodLength)
            {
                sessionTimer = 0;
                resumed = false;
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
            if (timer >= SimulationParameters.timeStep)
            {
                timer -= SimulationParameters.timeStep;
                sessionTimer++;
                DateTimeRecorder.UpdateSchoolTime(new TimeSpan(0, 1, 0));
            }
        }
    }
}
