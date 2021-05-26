﻿using UnityEngine;
using SES.Core;


namespace SES.AIControl.FSM
{
    public class SStudentLockerBehavior : StudentBaseState
    {
        float timer = 0;
        Spot lockerToVisit;

        float waitingTime;
        float sessionTimer;

        public override void EnterState(StudentBehaviorControl behaviorControl)
        {
            if (PickLocker(behaviorControl))
            {
                behaviorControl.NavigateTo(lockerToVisit.transform.position);
            }
            else
            {
                RequestStatus(behaviorControl);
            }
            waitingTime = Random.Range(SimulationDefaults.lockerWaitingTime - 1f, SimulationDefaults.lockerWaitingTime + 1);
        }

        public override void Update(StudentBehaviorControl behaviorControl)
        {
            PassTime();

            if (sessionTimer > waitingTime)
            {
                ReleaseLocker();
                behaviorControl.NavigateTo(behaviorControl.currentDesk.transform.position);
                RequestStatus(behaviorControl);
            }
        }

        public override string ToString()
        {
            return "Going To Locker";
        }

        private void PassTime()
        {
            timer += Time.deltaTime;
            if (timer >= SimulationParameters.timeStep)
            {
                timer -= SimulationParameters.timeStep;
                sessionTimer++;
            }
        }

        bool PickLocker(StudentBehaviorControl control)
        {
            if (control.currentClassroom != null)
            {
                lockerToVisit = control.currentClassroom.RequestLocker(control);
            }
            else if (control.currentLab != null)
            {
                lockerToVisit = control.currentLab.RequestLocker(control);
            }
            return (lockerToVisit != null);
        }

        void ReleaseLocker()
        {
            lockerToVisit.ClearSpot();
            lockerToVisit = null;
        }

        void RequestStatus(StudentBehaviorControl control)
        {
            if (control.currentClassroom != null)
            {
                control.currentClassroom.classScheduler.RequestStatus(control);
            }
            else if (control.currentLab != null)
            {
                control.StartClass();
            }
        }

    }
}