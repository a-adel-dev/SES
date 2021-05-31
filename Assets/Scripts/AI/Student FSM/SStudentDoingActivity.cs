using SES.Core;
namespace SES.AIControl.FSM
{
    public class SStudentDoingActivity : StudentBaseState
    {
        public override void EnterState(StudentBehaviorControl behaviorControl)
        {
            behaviorControl.ResumeAgent();
            behaviorControl.AgentHealth.SetActivityType(ActivityType.Talking);
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