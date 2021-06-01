using UnityEngine;
using SES.Core;


namespace SES.AIControl.FSM
{
    public class STeacherLocker : STeacherBaseState
    {
        float timer = 0;
        Spot lockerToVisit;

        float waitingTime;
        float sessionTimer;

        public override void EnterState(TeacherBehaviorControl behaviorControl)
        {

            if (PickLocker(behaviorControl))
            {
                behaviorControl.NavigateTo(lockerToVisit.transform.position);
            }
            else
            {
                behaviorControl.Rest();
            }
            waitingTime = Random.Range(SimulationDefaults.lockerWaitingTime - 1f, SimulationDefaults.lockerWaitingTime + 1);
            behaviorControl.AgentHealth.SetActivityType(ActivityType.Breathing);
        }

        public override void Update(TeacherBehaviorControl behaviorControl)
        {
            PassTime();

            if (sessionTimer > waitingTime)
            {
                ReleaseLocker();
                behaviorControl.NavigateTo(behaviorControl.currentDesk.transform.position);
                behaviorControl.GoToTeacherroom();
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

        bool PickLocker(TeacherBehaviorControl behaviorControl)
        {
            lockerToVisit = behaviorControl.teacherroom.RequestLocker(behaviorControl);
            return (lockerToVisit != null);
        }

        void ReleaseLocker()
        {
            lockerToVisit.ClearSpot();
            lockerToVisit = null;
        }
    }
}