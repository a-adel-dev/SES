using UnityEngine;
using System;
using SES.Core;
using System.Collections.Generic;
using SES.Spaces;


namespace SES.School
{
    public class SSchoolEgressTime : SSchoolBaseState
    {
        int sessionTimer = 0;
        float timer = 0f;

        public override void EnterState(SchoolDayProgressionController progressionController)
        {
            progressionController.SchoolState = "Home time";
            if (resumed == false)
            {
                progressionController.EgressClassGroup();
            }
        }

        public override void Update(SchoolDayProgressionController progressionController)
        {
            if (sessionTimer >= SimulationParameters.cooldownClassExit)
            {
                //Debug.Log($"Egressing a class group. session timer is {sessionTimer}, cooldown exit is {SimulationParameters.cooldownClassExit}");
                sessionTimer = 0;
                progressionController.EgressClassGroup();
            }
            if (progressionController.remainingEgressStudents <= 0)
            {
                progressionController.TransitionToState(progressionController.offTime);
            }
            PassTime();
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