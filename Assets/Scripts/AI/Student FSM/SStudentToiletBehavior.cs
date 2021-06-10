using UnityEngine;
using SES.Core;


namespace SES.AIControl.FSM
{
    public class SStudentToiletBehavior : StudentBaseState
    {
        
        

        float timer = 0;
        float toiletWaitingTime = 2.0f;
        float sessionTimer;

        public override void EnterState(StudentBehaviorControl behaviorControl)
        {
            behaviorControl.ToiletToVisit = behaviorControl.bathroomToVisit.RequestToilet(behaviorControl);
            if (behaviorControl.ToiletToVisit != null)
            {
                behaviorControl.NavigateTo(behaviorControl.ToiletToVisit.transform.position);
                toiletWaitingTime = Random.Range(SimulationDefaults.lockerWaitingTime,
                                                 SimulationDefaults.lockerWaitingTime + 3);
            }
            else
            {
                behaviorControl.GoToClassroom();
            }
            behaviorControl.AgentHealth.SetActivityType(ActivityType.Breathing);
        }
        public override void Update(StudentBehaviorControl behaviorControl)
        {
            PassTime();

            if (sessionTimer > toiletWaitingTime)
            {
                behaviorControl.bathroomToVisit.ReleaseToilet(behaviorControl.ToiletToVisit);
                behaviorControl.ToiletToVisit = null;
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
            if (timer >= SimulationParameters.TimeStep)
            {
                timer -= SimulationParameters.TimeStep;
                sessionTimer++;
            }
        }
    }
}