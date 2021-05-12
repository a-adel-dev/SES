using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using SES.Core;
/*
namespace SES.Spaces.Classroom
{
    
    public class ActivityPlanner : MonoBehaviour
    {
        bool activitiesEnabled = false;
        int sessionActivityMinTime;
        SpaceStudentsBucket studentsBucket;
        IActivity boardActivity;
        IActivity groupActivity;
        SpotBucket classroomObjects;
        bool startedActivity = false;
        IActivity currentActivity;

        void Awake()
        {
            //classSchedular = GetComponent<ClassroomPeriodSchedular>();
            studentsBucket = GetComponent<SpaceStudentsBucket>();
            boardActivity = GetComponent<ActivityBoard>();
            //groupActivity = GetComponent<ActivityGroup>();
        }

        private void Update()
        {
            EndActivity(currentActivity);
        }

        public void SetActivityStatus(int index)
        {
            if (!activitiesEnabled) { return; }
            if (index > sessionActivityMinTime)
            {
                foreach (IStudentAI student in studentsBucket.studentsCurrentlyInSpace)
                {
                    student.StartActivity();
                }
                StartActivity(index);
            }
        }

        private void StartActivity(int index)
        {
            if (Random.Range(0f, 1) < 0.5f)
            {
                currentActivity = boardActivity;
                StartCoroutine((boardActivity as ActivityBoard).StartBoardActivity(studentsBucket.studentsCurrentlyInSpace, classroomObjects.boardSpots, index));
            }
            else
            {

                startedActivity = true;
                currentActivity = groupActivity;
                StartCoroutine(groupActivity.StartGroupActivity(studentsBucket.studentsCurrentlyInSpace, classroomObjects.desks, index));

            }
        }

        public void EndActivity(IActivity activity)
        {
            if (startedActivity == false) { return; }
            if (startedActivity && activity.GetActivityInProgressState() == false)
            {
                foreach (IStudentAI Student in studentsBucket.studentsCurrentlyInSpace)
                {
                    Student.StartClass();
                }
            }
            currentActivity = null;
            startedActivity = false;
        }

        public void EnableActivities()
        {
            activitiesEnabled = true;
        }

        public void SetActivityMinTime(int time)
        {
            sessionActivityMinTime = time;
        }
    }
}
*/