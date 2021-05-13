using UnityEngine;
using SES.Core;

namespace SES.Spaces.Classroom
{
    public class SClassActivity : SClassroomBaseState
    {
        public SClassroomInSession originalState;
        public int activityPeriod;
        public float sessionTimer = 0f;
        float timeStep;
        float timer = 0f;

        public override void EnterState(ClassroomProgressionControl schedular)
        {
            if (resumed == false)
            {
                timeStep = SimulationParameters.timeStep;
                //Debug.Log($"Activity, activity period is  {activityPeriod}");
                //make student active
                foreach (IStudentAI student in schedular.studentsBucket.studentsCurrentlyInSpace)
                {
                    student.StartActivity();
                }
            }
        }

        public override void Update(ClassroomProgressionControl schedular)
        {
            PassTime();
            if (activityPeriod - sessionTimer <= 2f)
            { 
                foreach (IStudentAI student in schedular.studentsBucket.studentsCurrentlyInSpace)
                {
                    student.StartClass();
                }
            }

            if (IsActivityPeriodOver())
            {
                //Debug.Log($"activity over, returning to {originalState}");
                schedular.TransitionToState(originalState);
            }
        }

        private void PassTime()
        {
            timer += Time.deltaTime;
            if (timer >= timeStep)
            {
                sessionTimer++;
                //Debug.Log($"session timer is {sessionTimer}, activity period is {activityPeriod}");
                timer -= timeStep;

            }
        }

        private bool IsActivityPeriodOver()
        {
            return (sessionTimer >= (float)activityPeriod);
        }
    }
}