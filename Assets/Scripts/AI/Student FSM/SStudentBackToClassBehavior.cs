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
            if (behaviorControl.currentClassroom != null)
            {
                behaviorControl.NavigateTo(behaviorControl.currentClassroom.entrance);
            }
            else if (behaviorControl.currentLab != null)
            {
                behaviorControl.NavigateTo(behaviorControl.currentLab.Entrance);
            }
        }

        public override void Update(StudentBehaviorControl behaviorControl)
        {
            if (behaviorControl.nearPOI && behaviorControl.inCorridor && behaviorControl.visitedPOI == false && stopForPOI)
            {
                behaviorControl.StopForPOI();
            }
            if (behaviorControl.currentClassroom != null)
            {
                if (behaviorControl.nav.remainingDistance <= behaviorControl.nav.stoppingDistance &&
                    behaviorControl.nav.pathPending == false)
                {
                    behaviorControl.currentClassroom.ReceiveStudent(behaviorControl);
                    behaviorControl.NavigateTo(behaviorControl.currentDesk.transform.position);
                    behaviorControl.currentClassroom.RequestStatus(behaviorControl);
                }
            }
            else if (behaviorControl.currentLab != null)
            {
                if (behaviorControl.nav.remainingDistance <= behaviorControl.nav.stoppingDistance &&
                    behaviorControl.nav.pathPending == false)
                {
                    behaviorControl.currentLab.ReceiveStudent(behaviorControl);
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