using SES.Core;
using UnityEngine;

namespace SES.AIControl.FSM
{
    public class SStudentBackToClassBehavior : StudentBaseState
    {
        public override void EnterState(StudentBehaviorControl behaviorControl)
        {
            behaviorControl.NavigateTo(behaviorControl.currentClassroom.transform.position);
        }

        public override void OnTriggerEnter(StudentBehaviorControl behaviorControl)
        {

        }

        public override void Update(StudentBehaviorControl behaviorControl)
        {
            if (behaviorControl.nearPOI && behaviorControl.inCorridor && behaviorControl.visitedPOI == false)
            {
                behaviorControl.StopForPOI();
            }

            if (behaviorControl.nav.remainingDistance <= behaviorControl.nav.stoppingDistance &&
                behaviorControl.nav.pathPending == false)
            {
                behaviorControl.currentClassroom.studentsBucket.ReceiveStudent(behaviorControl);
                behaviorControl.NavigateTo(behaviorControl.currentDesk.transform.position);
                behaviorControl.currentClassroom.classScheduler.RequestStatus(behaviorControl);
            }
        }

        public override string ToString()
        {
            return "Going Back To Class";
        }


    }
}