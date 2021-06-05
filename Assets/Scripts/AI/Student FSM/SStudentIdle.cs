using SES.Core;
namespace SES.AIControl.FSM
{
    public class SStudentIdle : StudentBaseState
    {
        public override void EnterState(StudentBehaviorControl behaviorControl)
        {
            behaviorControl.PauseAgent();
            behaviorControl.AgentHealth.SetActivityType(ActivityType.Paused);
        }

        public override void Update(StudentBehaviorControl behaviorControl)
        {

        }

        public override string ToString()
        {
            return "Idle";
        }
    }
}
