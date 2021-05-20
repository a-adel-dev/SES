using UnityEngine;
using SES.Core;
using System.Collections.Generic;

namespace SES.Spaces.Classroom
{
    public class SClassActivity : SClassroomBaseState
    {
        public SClassroomInSession originalState;
        public int activityPeriod;
        public float sessionTimer = 0f;
        float timeStep;
        float timer = 0f;
        List<IActivity> activityList = new List<IActivity>();
        IActivity currentActivity;

        public override void EnterState(ClassroomProgressionControl schedular)
        {
            if (resumed == false)
            {
                PickActivity(schedular);
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
                EndActivity();
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

        private void PickActivity(ClassroomProgressionControl schedular)
        {

            //add board Activity to activity list
            ActivityBoard boardActivity = new ActivityBoard(schedular.studentsBucket.studentsCurrentlyInSpace,
                                                            schedular.GetComponent<SpotBucket>().boardSpots);
            activityList.Add(boardActivity);

            //add group Activity to activity list
            ActivityGroup groupActivity = new ActivityGroup(schedular.studentsBucket.studentsCurrentlyInSpace,
                                                            schedular.GetComponent<SpotBucket>().desks);
            activityList.Add(groupActivity);

            //choose a random activity
            int randomIndex = Random.Range(0, activityList.Count);
            currentActivity = activityList[randomIndex];

            //run activity
            currentActivity.StartActivity();
        }

        private void EndActivity()
        {
            currentActivity.EndActivity();
        }
    }
}