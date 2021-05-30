using UnityEngine;
using SES.Core;

namespace SES.AIControl.FSM
{
    class STeacherInClassRestricted : STeacherBaseState
    {
        float timer;
        float timeStep;
        int sessionTimer;
        int wanderTime;

        public override void EnterState(TeacherBehaviorControl behaviorControl)
        {
            timeStep = SimulationParameters.TimeStep;
            Wander(behaviorControl);
            wanderTime = Random.Range(1, 5);
        }

        public override void Update(TeacherBehaviorControl behaviorControl)
        {
            PassTime(behaviorControl);
            LookAtStudents(behaviorControl);
        }

        private static void Wander(TeacherBehaviorControl behaviorControl)
        {
            Vector3 bounds_min = behaviorControl.currentClass.classroomSubSpaces.TeacherSpace.bounds.min;
            Vector3 bounds_max = behaviorControl.currentClass.classroomSubSpaces.TeacherSpace.bounds.max;
            float waypoint_x = Random.Range(bounds_min[0], bounds_max[0]);
            float waypoint_z = Random.Range(bounds_min[2], bounds_max[2]);
            Vector3 waypoint = new Vector3(waypoint_x, 0f, waypoint_z);
            behaviorControl.NavigateTo(waypoint);
        }

        private void PassTime(TeacherBehaviorControl behaviorControl)
        {
            timer += Time.deltaTime;
            if (timer >= timeStep)
            {
                timer -= timeStep;
                sessionTimer++;  
            }

            if (sessionTimer >= wanderTime)
            {
                Wander(behaviorControl);
                wanderTime = Random.Range(1, 5);
                sessionTimer = 0;
            }
        }

        public void LookAtStudents(TeacherBehaviorControl behaviorControl)
        {

            Vector3 studentsDirecton = new Vector3(behaviorControl.currentClass.GetGameObject().transform.position.x,
                                                            0,
                                                            behaviorControl.currentClass.GetGameObject().transform.position.z);
            behaviorControl.transform.LookAt(studentsDirecton);
        }

        public override string ToString()
        {
            return "Teaching a class with restricted movement"; 
        }
    }
}
