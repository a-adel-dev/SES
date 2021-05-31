using SES.Core;
using UnityEngine;

namespace SES.AIControl.FSM
{
    public class SStudentBackToClassBehavior : StudentBaseState
    {
        bool stopForPOI;
        int POIChance = 10;

        public override void EnterState(StudentBehaviorControl behaviorControl)
        {
            stopForPOI = Random.Range(1, 100) <= POIChance;
            if (behaviorControl.CurrentClassroom != null)
            {
                behaviorControl.NavigateTo(behaviorControl.CurrentClassroom.Entrance);
            }
            else if (behaviorControl.CurrentLab != null)
            {
                behaviorControl.NavigateTo(behaviorControl.CurrentLab.SubSpaces.Entrance.position);
            }
            behaviorControl.AgentHealth.SetActivityType(ActivityType.Breathing);
        }

        public override void Update(StudentBehaviorControl behaviorControl)
        {
            if (behaviorControl.nearPOI && behaviorControl.inCorridor && behaviorControl.visitedPOI == false && stopForPOI)
            {
                behaviorControl.StopForPOI();
            }
            if (behaviorControl.CurrentClassroom != null)
            {
                if (behaviorControl.nav.remainingDistance <= behaviorControl.nav.stoppingDistance &&
                    behaviorControl.nav.pathPending == false)
                {
                    behaviorControl.CurrentClassroom.ReceiveStudent(behaviorControl);
                    behaviorControl.NavigateTo(behaviorControl.currentDesk.transform.position);
                    behaviorControl.CurrentClassroom.RequestStatus(behaviorControl);
                }
            }
            else if (behaviorControl.CurrentLab != null)
            {
                if (behaviorControl.nav.remainingDistance <= behaviorControl.nav.stoppingDistance &&
                    behaviorControl.nav.pathPending == false)
                {
                    behaviorControl.CurrentLab.ReceiveStudent(behaviorControl);
                    behaviorControl.StartClass();
                }
            }
        }

        public override string ToString()
        {
            return "Going Back To Class";
        }
    }
}