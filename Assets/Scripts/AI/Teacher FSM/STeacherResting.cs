using UnityEngine;
using SES.Core;
namespace SES.AIControl.FSM
{
    public class STeacherResting : STeacherBaseState
    {
        float timer = 0f;
        float timeStep;
        int randomLookChance = 10;
        public override void EnterState(TeacherBehaviorControl behaviorControl)
        {

            timeStep = SimulationParameters.TimeStep * 5;
            if (behaviorControl.currentDesk == null)
            {
                behaviorControl.teacherroom.AddToTeachersInRoom(behaviorControl);
                behaviorControl.currentDesk = behaviorControl.teacherroom.RequestDesk(behaviorControl);
                if (behaviorControl.currentDesk != null)
                {
                    behaviorControl.NavigateTo(behaviorControl.currentDesk.transform.position);
                }
                behaviorControl.visitedPOI = false;
            }
            else
            {
                behaviorControl.NavigateTo(behaviorControl.currentDesk.transform.position);
            }
            behaviorControl.AgentHealth.SetActivityType(ActivityType.Talking);
        }

        public override void Update(TeacherBehaviorControl behaviorControl)
        {
            
            //check if you can go
            //if you can go
            //Enable Autonomus state
            PassTime(behaviorControl);
        }

        private void RandomLook(TeacherBehaviorControl behaviorControl)
        {
            int randomIndex = Random.Range(0, 100);
            if (randomIndex < randomLookChance)
            {
                Vector3 bounds_min = behaviorControl.teacherroom.GetGameObject().GetComponent<BoxCollider>().bounds.min;
                Vector3 bounds_max = behaviorControl.teacherroom.GetGameObject().GetComponent<BoxCollider>().bounds.max;
                float waypoint_x = Random.Range(bounds_min[0], bounds_max[0]);
                float waypoint_z = Random.Range(bounds_min[2], bounds_max[2]);
                Vector3 waypoint = new Vector3(waypoint_x, 0f, waypoint_z);
                behaviorControl.transform.LookAt(waypoint);
            }
        }

        void CheckAutonomy(TeacherBehaviorControl behaviorControl)
        {
            int chance = Random.Range(0, 100);
            if (chance < SimulationDefaults.baseAutonomyChance - 5)
            {
                int randomIndex = Random.Range(1, 10);
                if (randomIndex < SimulationDefaults.bathroomChance)
                {
                    //ReleaseDesk
                    //Go to bathroom
                    behaviorControl.BehaviorGoToBathroom();
                }
                else
                {
                    //Go To locker
                    behaviorControl.BehaviorGoToLocker();
                }
            }
        }

        private void PassTime(TeacherBehaviorControl behaviorControl)
        {
            timer += Time.deltaTime;
            if (timer >= timeStep)
            {
                timer -= timeStep;
                CheckAutonomy(behaviorControl);
                RandomLook(behaviorControl);
            }
        }

        public override string ToString()
        {
            return "Resting";
        }
    }
}
