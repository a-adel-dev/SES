using UnityEngine;
using SES.Core;


namespace SES.AIControl.FSM
{
    public class SStudentLockerBehavior : StudentBaseState
    {
        float timer = 0;
        Spot lockerToVisit;

        float lockerWaitingTime = 2.0f;
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
        }

        public override void OnTriggerEnter(StudentBehaviorControl behaviorControl)
        {

        }

        public override void Update(StudentBehaviorControl behaviorControl)
        {
            PassTime();

            if (sessionTimer > lockerWaitingTime)
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
            lockerToVisit = control.currentClassroom.RequestLocker(control);
            return (lockerToVisit != null);
        }

        void ReleaseLocker()
        {
            lockerToVisit.ClearSpot();
            lockerToVisit = null;
        }

        void RequestStatus(StudentBehaviorControl control)
        {
            control.currentClassroom.classScheduler.RequestStatus(control);
        }

    }
}