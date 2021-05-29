using UnityEngine;
using SES.Core;
namespace SES.AIControl.FSM
{
    public class SStudentInClassroom : StudentBaseState
    {
        float timer = 0f;
        public override void EnterState(StudentBehaviorControl behaviorControl)
        {
            behaviorControl.ResumeAgent();
            behaviorControl.BackToDesk();
            behaviorControl.GetComponent<MeshRenderer>().enabled = true;
        }

        public override void Update(StudentBehaviorControl behaviorControl)
        {
            if (behaviorControl.currentClassroom != null)
            {
                behaviorControl.LookAtBoard();
            }
            PassTime(behaviorControl);
        }

        void CheckAutonomy(StudentBehaviorControl behaviorControl)
        {
            int chance = Random.Range(0, 100);
            if (chance < SimulationDefaults.baseAutonomyChance)
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

        public override string ToString()
        {
            return "In Classroom";
        }
    }
}
