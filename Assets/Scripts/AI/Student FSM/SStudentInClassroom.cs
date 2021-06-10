using UnityEngine;
using SES.Core;
namespace SES.AIControl.FSM
{
    public class SStudentInClassroom : StudentBaseState
    {
        float timer = 0f;
        public override void EnterState(StudentBehaviorControl behaviorControl)
        {
            if (behaviorControl.ToiletToVisit != null)
            {
                behaviorControl.bathroomToVisit.ReleaseToilet(behaviorControl.ToiletToVisit);
                behaviorControl.ToiletToVisit = null;
            }
            behaviorControl.ResumeAgent();
            behaviorControl.BackToDesk();
            behaviorControl.gameObject.transform.GetChild(2).GetComponent<MeshRenderer>().enabled = true;
            behaviorControl.ClearLocker();
            behaviorControl.AgentHealth.SetActivityType(ActivityType.Breathing);
        }

        public override void Update(StudentBehaviorControl behaviorControl)
        {
            behaviorControl.BackToDesk();
            if (behaviorControl.CurrentClassroom != null)
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
            if (timer >= SimulationParameters.TimeStep)
            {
                timer -= SimulationParameters.TimeStep;
                CheckAutonomy(behaviorControl);
            }
        }

        public override string ToString()
        {
            return "In Classroom";
        }
    }
}
