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
            behaviorControl.NavigateTo(behaviorControl.poi.transform.position);
            Vector3 POIDirection = new Vector3(behaviorControl.poi.transform.position.x,
                                                            0,
                                                            behaviorControl.poi.transform.position.z);
            behaviorControl.transform.LookAt(POIDirection);

        }

        public override void OnTriggerEnter(StudentBehaviorControl behaviorControl)
        {

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
            if (timer >= SimulationParameters.timeStep)
            {
                timer -= SimulationParameters.timeStep;
                sessionTimer++;
            }
        }

    }
}