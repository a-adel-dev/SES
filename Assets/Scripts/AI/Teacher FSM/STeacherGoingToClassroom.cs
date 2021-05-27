using UnityEngine;
using SES.Core;

namespace SES.AIControl.FSM
{
    class STeacherGoingToClassroom : STeacherBaseState
    {


        public override void EnterState(TeacherBehaviorControl behaviorControl)
        {
            behaviorControl.NavigateTo(behaviorControl.currentClass.GetGameObject().transform.position);
        }

        public override void Update(TeacherBehaviorControl behaviorControl)
        {
            if (behaviorControl.nav.remainingDistance <= behaviorControl.nav.stoppingDistance &&
                behaviorControl.nav.pathPending == false)
            {
                behaviorControl.ClassroomFree();
            }
        }


        public override string ToString()
        {
            return "Going to classroom";
        }
    }
}
