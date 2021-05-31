using SES.Core;
namespace SES.AIControl.FSM
{
    public class SStudentInTransit : StudentBaseState
    {
        public override void EnterState(StudentBehaviorControl behaviorControl)
        {
            //Debug.Log($"in transit");
            behaviorControl.ResumeAgent();
            behaviorControl.ClearLocker();
            behaviorControl.AgentHealth.SetActivityType(ActivityType.Breathing);
        }

        public override void Update(StudentBehaviorControl behaviorControl)
        {
            
        }
    }
}