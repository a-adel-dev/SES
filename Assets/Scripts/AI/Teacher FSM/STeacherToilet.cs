using UnityEngine;
using SES.Core;


namespace SES.AIControl.FSM
{
    public class STeacherToilet : STeacherBaseState
    {

        Spot toiletToVisit;

        float timer = 0;
        float toiletWaitingTime = 2.0f;
        float sessionTimer;

        public override void EnterState(TeacherBehaviorControl behaviorControl)
        {
            toiletToVisit = behaviorControl.bathroomToVisit.RequestToilet(behaviorControl);
            toiletWaitingTime = Random.Range(SimulationDefaults.lockerWaitingTime, SimulationDefaults.lockerWaitingTime + 3);
            if (toiletToVisit != null)
            {
                behaviorControl.NavigateTo(toiletToVisit.transform.position);
            }
            else
            {
                behaviorControl.GoToTeacherroom();
            }
        }
        public override void Update(TeacherBehaviorControl behaviorControl)
        {
            PassTime();

            if (sessionTimer > toiletWaitingTime)
            {
                toiletToVisit.ClearSpot();
                toiletToVisit = null;
                behaviorControl.bathroomToVisit = null;

                behaviorControl.GoToTeacherroom();

            }
        }

        public override string ToString()
        {
            return "Going To Toilet";
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
    }
}