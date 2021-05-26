using UnityEngine;
using System;
using SES.Core;

namespace SES.School
{
    public class SClassesInSession : SSchoolBaseState 
    {

        int sessionTimer = 0;
        public int sessionLength;
        float timer = 0f;

        public override void EnterState(SchoolScheduler progressionController)
        {

            if (resumed == false)
            {
                sessionLength = progressionController.periodLength;
                progressionController.RelocateClasses();
                progressionController.StartPeriod();
            }
            else
            {
                progressionController.ResumeClasses();
            }
            progressionController.SchoolState = "Classes in session";
        }

        public override void Update(SchoolScheduler progressionController)
        {
            if (sessionTimer >= sessionLength)
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
