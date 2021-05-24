using UnityEngine;
namespace SES.AIControl.FSM
{
    public class SStudentDoingActivity : StudentBaseState
    {
        public override void EnterState(StudentBehaviorControl behaviorControl)
        {
            behaviorControl.ResumeAgent();
        }

        public override void Update(StudentBehaviorControl behaviorControl)
        {

        }

        public override string ToString()
        {
            return "Doing activity";
        }
    }
}