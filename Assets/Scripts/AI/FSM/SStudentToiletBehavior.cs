using UnityEngine;
using SES.Core;


namespace SES.AIControl.FSM
{
    public class SStudentToiletBehavior : StudentBaseState
    {
        
        Spot toiletToVisit;

        float timer = 0;
        float toiletWaitingTime = 2.0f;
        float sessionTimer;

        public override void EnterState(StudentBehaviorControl behaviorControl)
        {
            toiletToVisit = behaviorControl.bathroomToVisit.RequestToilet(behaviorControl);
            toiletWaitingTime = Random.Range(SimulationDefaults.lockerWaitingTime, SimulationDefaults.lockerWaitingTime + 3);
            if (toiletToVisit != null)
            {
                behaviorControl.NavigateTo(toiletToVisit.transform.position);
            }
            else
            {
                behaviorControl.GoToClassroom();
            }
        }
        public override void Update(StudentBehaviorControl behaviorControl)
        {
            PassTime();

            if (sessionTimer > toiletWaitingTime)
            {
                toiletToVisit.ClearSpot();
                toiletToVisit = null;
                behaviorControl.bathroomToVisit = null;

                behaviorControl.GoToClassroom();

            }
        }

        public override string ToString()
        {
            return "Going To Toilet";
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
    }
}