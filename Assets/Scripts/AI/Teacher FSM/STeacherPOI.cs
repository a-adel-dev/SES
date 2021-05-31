using SES.Core;
using UnityEngine;

namespace SES.AIControl.FSM
{
    public class STeacherPOI : STeacherBaseState
    {
        float timer = 0;
        float POIWaitingTime = 2.0f;
        float sessionTimer;

        public override void EnterState(TeacherBehaviorControl behaviorControl)
        {
            behaviorControl.visitedPOI = true;
            behaviorControl.NavigateTo(behaviorControl.poi.transform.position);
            POIWaitingTime = Random.Range(SimulationDefaults.lockerWaitingTime - 0.5f, SimulationDefaults.lockerWaitingTime + 1);
            Vector3 POIDirection = new Vector3(behaviorControl.poi.transform.position.x,
                                                            0,
                                                            behaviorControl.poi.transform.position.z);
            behaviorControl.transform.LookAt(POIDirection);
            behaviorControl.AgentHealth.SetActivityType(ActivityType.Breathing);

        }

        public override void Update(TeacherBehaviorControl behaviorControl)
        {
            PassTime();
            if (sessionTimer >= POIWaitingTime)
            {
                behaviorControl.GoToTeacherroom();
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