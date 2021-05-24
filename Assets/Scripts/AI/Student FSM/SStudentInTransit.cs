using UnityEngine;
namespace SES.AIControl.FSM
{
    public class SStudentInTransit : StudentBaseState
    {
        public override void EnterState(StudentBehaviorControl behaviorControl)
        {
            //Debug.Log($"in transit");
            behaviorControl.ResumeAgent();
        }

        public override void Update(StudentBehaviorControl behaviorControl)
        {
            
        }
    }
}