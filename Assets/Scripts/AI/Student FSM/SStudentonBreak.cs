using UnityEngine;
using SES.Core;
namespace SES.AIControl.FSM
{
    public class SStudentonBreak : StudentBaseState
    {
        float timer = 0f;
        float timeStep ;
        public override void EnterState(StudentBehaviorControl behaviorControl)
        {
            //Debug.Log($"on break");
            timeStep = SimulationParameters.timeStep * 2 ;
            behaviorControl.ResumeAgent();
        }

        public override void Update(StudentBehaviorControl behaviorControl)
        {
            PassTime(behaviorControl);
        }

        void CheckAutonomy(StudentBehaviorControl behaviorControl)
        {
            int chance = Random.Range(0, 100);
            if (chance < behaviorControl.breakAutonomyChance)
            {
                behaviorControl.ReleaseControl();
            }
        }

        private void PassTime(StudentBehaviorControl behaviorControl)
        {
            timer += Time.deltaTime;
            if (timer >= timeStep)
            {
                timer -= timeStep;
                CheckAutonomy(behaviorControl);
            }
        }
    }
}