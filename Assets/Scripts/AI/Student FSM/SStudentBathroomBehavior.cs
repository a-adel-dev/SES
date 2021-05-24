using UnityEngine;
using SES.Core;

namespace SES.AIControl.FSM
{
    public class SStudentBathroomBehavior : StudentBaseState
    {
        public override void EnterState(StudentBehaviorControl behaviorControl)
        {

            behaviorControl.currentClassroom.ExitClassroom(behaviorControl);
            behaviorControl.bathroomToVisit = behaviorControl.school.RequestBathroom(behaviorControl);
            behaviorControl.NavigateTo(behaviorControl.bathroomToVisit.GetGameObject().transform.position);
        }

        public override void Update(StudentBehaviorControl behaviorControl)
        {
            if (behaviorControl.nav.remainingDistance <= behaviorControl.nav.stoppingDistance &&
                behaviorControl.nav.pathPending == false)
            {
                behaviorControl.VisitToilet();
            }
        }

        public override string ToString()
        {
            return "Going To Bathroom";
        }
    }
}