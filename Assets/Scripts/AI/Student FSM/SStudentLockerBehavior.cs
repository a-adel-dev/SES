using UnityEngine;
using SES.Core;


namespace SES.AIControl.FSM
{
    public class SStudentLockerBehavior : StudentBaseState
    {
        float timer = 0;
        
        float waitingTime;
        float sessionTimer;

        public override void EnterState(StudentBehaviorControl behaviorControl)
        {
            if (PickLocker(behaviorControl))
            {
                behaviorControl.NavigateTo(behaviorControl.lockerToVisit.transform.position);
            }
            else if (behaviorControl.CurrentClassroom != null)
            {
                RequestStatus(behaviorControl);
            }
            else if (behaviorControl.CurrentLab != null)
            {
                behaviorControl.StartClass();
            }
            waitingTime = Random.Range(SimulationDefaults.lockerWaitingTime - 1f, SimulationDefaults.lockerWaitingTime + 1);
            behaviorControl.AgentHealth.SetActivityType(ActivityType.Breathing);
        }

        public override void Update(StudentBehaviorControl behaviorControl)
        {
            PassTime();

            if (sessionTimer > waitingTime)
            {
                behaviorControl.ClearLocker();
                behaviorControl.NavigateTo(behaviorControl.currentDesk.transform.position);
                if (behaviorControl.CurrentClassroom != null)
                {
                    RequestStatus(behaviorControl);
                }
                else
                {
                    behaviorControl.StartClass();
                }
            }
        }

        public override string ToString()
        {
            return "Going To Locker";
        }

        private void PassTime()
        {
            timer += Time.deltaTime;
            if (timer >= SimulationParameters.TimeStep)
            {
                timer -= SimulationParameters.TimeStep;
                sessionTimer++;
            }
        }

        bool PickLocker(StudentBehaviorControl control)
        {
            if (control.CurrentClassroom != null)
            {
                control.lockerToVisit = control.CurrentClassroom.RequestLocker(control);

            }
            else if (control.CurrentLab != null)
            {
                control.lockerToVisit = control.CurrentLab.RequestLocker(control);
            }
            return (control.lockerToVisit != null);
        }

        void RequestStatus(StudentBehaviorControl control)
        {
            control.CurrentClassroom.RequestStatus(control);
        }

    }
}