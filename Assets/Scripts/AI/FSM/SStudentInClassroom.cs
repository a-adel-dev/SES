using UnityEngine;
namespace SES.AIControl.FSM
{
    public class SStudentInClassroom : StudentBaseState
    {
        float timer = 0f;
        float timeStep;
        public override void EnterState(StudentBehaviorControl behaviorControl)
        {
            timeStep = behaviorControl.timeStep;
            behaviorControl.ResumeAgent();
            behaviorControl.BackToDesk();
            //Debug.Log($"in classroom");
        }

        public override void OnTriggerEnter(StudentBehaviorControl behaviorControl)
        {
            
        }

        public override void Update(StudentBehaviorControl behaviorControl)
        {
            behaviorControl.LookAtBoard();
            //check if you can go
            //if you can go
            //Enable Autonomus state
            PassTime(behaviorControl);
        }

        void checkAutonomy(StudentBehaviorControl behaviorControl)
        {
            int chance = Random.Range(0, 100);
            if (chance < 1.0f)
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
                checkAutonomy(behaviorControl);
            }
        }

        public override string ToString()
        {
            return "In Classroom";
        }
    }
}
