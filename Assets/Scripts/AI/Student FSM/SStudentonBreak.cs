using UnityEngine;
using SES.Core;
namespace SES.AIControl.FSM
{
    public class SStudentonBreak : StudentBaseState
    {
        float timer = 0f;
        public override void EnterState(StudentBehaviorControl behaviorControl)
        {
            behaviorControl.ResumeAgent();
        }

        public override void Update(StudentBehaviorControl behaviorControl)
        {
            PassTime(behaviorControl);
        }

        void CheckAutonomy(StudentBehaviorControl behaviorControl)
        {
            int chance = Random.Range(0, 100);
            if (chance < SimulationDefaults.breakAutonomyChance)
            {
                behaviorControl.BeAutonomus();
            }
        }

        private void PassTime(StudentBehaviorControl behaviorControl)
        {
            timer += Time.deltaTime;
            if (timer >= SimulationParameters.timeStep)
            {
                timer -= SimulationParameters.timeStep;
                CheckAutonomy(behaviorControl);
            }
        }
    }
}