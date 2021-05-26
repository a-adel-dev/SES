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
            //Debug.Log($"in classroom");
            behaviorControl.visitedPOI = false;
            //Debug.Log(SimulationParameters.timeStep);
        }

        public override void Update(StudentBehaviorControl behaviorControl)
        {
            if (behaviorControl.currentClassroom != null)
            {
                behaviorControl.LookAtBoard();
            }
            PassTime(behaviorControl);
            Debug.Log("In classroom");
        }

        void CheckAutonomy(StudentBehaviorControl behaviorControl)
        {
            int chance = Random.Range(0, 100);
            if (chance < behaviorControl.baseAutonomyChance)
            {
                behaviorControl.ReleaseControl();
            }
        }

        private void PassTime(StudentBehaviorControl behaviorControl)
        {
            
            timer += Time.deltaTime;
            if (timer >= SimulationDefaults.timeStep)
            {
                timer -= SimulationDefaults.timeStep;
                Debug.Log("checking autonomy");
                CheckAutonomy(behaviorControl);
            }
        }

        public override string ToString()
        {
            return "In Class";
        }
    }
}
