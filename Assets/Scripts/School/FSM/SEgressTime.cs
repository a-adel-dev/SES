using UnityEngine;
using System;
using SES.Core;


namespace SES.School
{
    public class SEgressTime : SSchoolBaseState
    {
        /// <summary>
        /// the time to allow the last egressed class to reach egress point 
        /// before transitioning to the next state
        /// </summary>
        int waitTimeForEgress = 20;
        int cooldownClassExit = SimulationParameters.cooldownClassExit;
        bool egressDone = false;
        int sessionTimer = 0;
        float timeStep;
        float timer = 0f;
        public override void EnterState(SchoolDayProgressionController progressionController)
        {
            //consider pausing
            timeStep = progressionController.timeStep;
            Debug.Log("-------------Egress Time-------------");
            //Debug.Log($"cooldown class Exit is {cooldownClassExit}");
            progressionController.EgressClassGroup();
            progressionController.SchoolState = "Home time";

        }

        public override void Update(SchoolDayProgressionController progressionController)
        {
            if (egressDone == false && progressionController.remainingEgressClassrooms.Count <= 0)
            {
                egressDone = true;
            }
            if (egressDone == false && sessionTimer >= cooldownClassExit)
            {
                sessionTimer = 0;
                progressionController.EgressClassGroup();
                Debug.Log($"Egressing a classgroup, egressDone is {egressDone}"); 
            }
            if (egressDone && sessionTimer >= waitTimeForEgress)
            {
                progressionController.ResetRemainingEgressClasses();
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
                Debug.Log(sessionTimer);
                DateTimeRecorder.UpdateSchoolTime(new TimeSpan(0, 1, 0));
            }
        }
    }
}