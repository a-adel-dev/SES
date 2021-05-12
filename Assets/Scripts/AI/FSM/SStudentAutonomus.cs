using UnityEngine;
namespace SES.AIControl.FSM
{
    public class SStudentAutonomus : StudentBaseState
    {
        public override void EnterState(StudentBehaviorControl behaviorControl)
        {
            behaviorControl.ResumeAgent();
            //Debug.Log($"Autonomus");
            //activiate panda
        }

        public override void OnTriggerEnter(StudentBehaviorControl behaviorControl)
        {
            
        }

        public override void Update(StudentBehaviorControl behaviorControl)
        {
            
        }

        public override string ToString()
        {
            return "Autonomus";
        }
    }
}