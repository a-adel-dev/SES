using SES.Core;
using UnityEngine;

namespace SES.AIControl.FSM
{
    public class SStudentNearPOIBehavior : StudentBaseState
    {
        float timer = 0;
        float POIWaitingTime = 2.0f;
        float sessionTimer;

        public override void EnterState(StudentBehaviorControl behaviorControl)
        {
            behaviorControl.visitedPOI = true;
            behaviorControl.NavigateTo(behaviorControl.poi.GetGameObject().transform.position);
            POIWaitingTime = Random.Range(SimulationDefaults.lockerWaitingTime - 0.5f, SimulationDefaults.lockerWaitingTime + 1);
            Vector3 POIDirection = new Vector3(behaviorControl.poi.GetGameObject().transform.position.x,
                                                            0,
                                                            behaviorControl.poi.GetGameObject().transform.position.z);
            behaviorControl.transform.LookAt(POIDirection);
            behaviorControl.AgentHealth.SetActivityType(ActivityType.Breathing);

        }

        public override void Update(StudentBehaviorControl behaviorControl)
        {
            PassTime();
            if (sessionTimer >= POIWaitingTime)
            {
                behaviorControl.GoToClassroom();
            }

        }

        public override string ToString()
        {
            return "Inspecting a point of interest";
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