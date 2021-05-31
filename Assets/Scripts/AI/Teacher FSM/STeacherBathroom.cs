using UnityEngine;
using SES.Core;

namespace SES.AIControl.FSM
{
    public class STeacherBathroom : STeacherBaseState
    {
        public override void EnterState(TeacherBehaviorControl behaviorControl)
        {
            if (behaviorControl.currentDesk != null)
            {
                behaviorControl.currentDesk.ClearSpot();
            }
            behaviorControl.teacherroom.ExitTeacherroom(behaviorControl);
            behaviorControl.bathroomToVisit = behaviorControl.school.RequestBathroom(behaviorControl);
            behaviorControl.NavigateTo(behaviorControl.bathroomToVisit.GetGameObject().transform.position);
            behaviorControl.AgentHealth.SetActivityType(ActivityType.Breathing);
        }

        public override void Update(TeacherBehaviorControl behaviorControl)
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