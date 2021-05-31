using UnityEngine;
using SES.Core;


namespace SES.AIControl.FSM
{
    public class SStudentAutonomus : StudentBaseState
    {
        
        public override void EnterState(StudentBehaviorControl behaviorControl)
        {
            int randomIndex = Random.Range(1, 10);
            if (randomIndex < SimulationDefaults.bathroomChance)
            {
                behaviorControl.BehaviorGoToBathroom();
            }
            else
            {
                behaviorControl.BehaviorGoToLocker();
            }
            behaviorControl.AgentHealth.SetActivityType(ActivityType.Breathing);
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