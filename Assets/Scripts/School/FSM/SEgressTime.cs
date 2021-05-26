using UnityEngine;
using System;
using SES.Core;
using System.Collections.Generic;
using SES.Spaces;


namespace SES.School
{
    public class SEgressTime : SSchoolBaseState
    {
        int cooldownClassExit;
        int sessionTimer = 0;
        float timeStep;
        float timer = 0f;

        
        
        public override void EnterState(SchoolScheduler progressionController)
        {
            cooldownClassExit = SimulationParameters.cooldownClassExit;
            progressionController.SchoolState = "Home time";
            if (resumed == false)
            {
                timeStep = progressionController.timeStep;
                progressionController.EgressClassGroup();
            }
        }

        public override void Update(SchoolScheduler progressionController)
        {
            if (sessionTimer >= cooldownClassExit)
            {
                //Debug.Log($"Egressing a class group. session timer is {sessionTimer}, cooldown exit is {cooldownClassExit}");
                sessionTimer = 0;
                progressionController.EgressClassGroup();
            }
            if (progressionController.remainingEgressStudents <= 0)
            {
                progressionController.ResetEgress();
                progressionController.TransitionToState(progressionController.offTime);
            }
            PassTime();
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