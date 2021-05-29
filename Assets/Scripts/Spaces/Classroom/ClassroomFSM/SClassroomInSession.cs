using UnityEngine;
using SES.Core;
using System.Collections.Generic;

namespace SES.Spaces.Classroom
{
    public class SClassroomInSession : SClassroomBaseState
    {
        int minSectionNumber;
        int maxSectionNumber;
        float timer = 0f;
        float timeStep;
        int sessionTimer = 0;
        int currentSectionIndex = 0;
        bool lastSection = false;
        bool wasInActivity = false;
        List<int> classSections = new List<int>();

        public override void EnterState(ClassroomProgressionControl schedular)
        {
            
            foreach (IStudentAI student in schedular.studentsBucket.GetStudentsInSpace())
            {
                student.StartClass();
            }
            //Debug.Log($"In session");
            if (resumed == false)
            {
                InitializeValues();
                StructureClass();
                CheckActivity(schedular);
                if (schedular.teacherBucket.teacher != null)
                {
                    schedular.teacherBucket.teacher.ClassroomFree();
                }
                Debug.Log("requesting teacher");
                schedular.RequestTeacher();
                schedular.teacherBucket.teacher.currentClass = schedular.GetComponent<IClassroom>();
                schedular.teacherBucket.teacher.GoToClassroom();
            }
            else if (wasInActivity)
            {
                wasInActivity = false;
                CheckActivity(schedular);
            }
        }

        public override void Update(ClassroomProgressionControl schedular)
        {
            PassTime(schedular);
            //Debug.Log($"Current index is {currentSectionIndex}, time {classSections[currentSectionIndex]}, out of {classSections.Count} sections");
        }

        void InitializeValues()
        {
            minSectionNumber = SimulationDefaults.minClassSectionNumber;
            maxSectionNumber = SimulationDefaults.maxClassSectionNumber;
            timeStep = SimulationParameters.timeStep;
        }

        private void PassTime(ClassroomProgressionControl schedular)
        {
            timer += Time.deltaTime;
            if (timer >= timeStep)
            {
                if (lastSection)
                {
                    return;
                }
                //check if last section
                if (currentSectionIndex + 1 >= classSections.Count)
                {
                    lastSection = true;
                    CheckActivity(schedular);
                    sessionTimer = 0;
                    return;
                }
                timer -= timeStep;
                sessionTimer++;
                //check if next section
                if (sessionTimer >= classSections[currentSectionIndex])
                {
                    currentSectionIndex++;
                    CheckActivity(schedular);
                    sessionTimer = 0;
                }
            }
        }

        public void CheckActivity(ClassroomProgressionControl schedular)
        {
            //Debug.Log($"checking activity at {sessionTimer}");
            if (SimulationParameters.activitiesEnabled == false)
            {
                return;
            }
            //if current section is larger than activity minimum threshold
            if (classSections[currentSectionIndex] >= SimulationDefaults.minClassActivityTime)
            {
                //set class to be resumed
                resumed = true;
                //flag the session to be in activity
                wasInActivity = true;
                //Debug.Log($"Activity time is {classSections[currentSectionIndex]}");
                //transition to activity state
                schedular.DoActivity((classSections[currentSectionIndex]), this);
                //if this is not the last session
                if (lastSection == false)
                {
                    //advance class section index
                    currentSectionIndex++;
                    sessionTimer = 0;
                }
            }
        }

        private void StructureClass()
        {
            int numSections = Random.Range(minSectionNumber, maxSectionNumber);

            List<int> randomPartitions = new List<int>();

            for (int i = 0; i < numSections; i++)
            {
                randomPartitions.Add(Random.Range(1, 10));
            }

            int totalofRandomPartitions = 0;

            for (int i = 0; i < randomPartitions.Count; i++)
            {
                totalofRandomPartitions += randomPartitions[i];
            }

            List<int> classroomSectionList = new List<int>();

            for (int i = 0; i < randomPartitions.Count; i++)
            {
                int value = Mathf.FloorToInt(randomPartitions[i] / (float)totalofRandomPartitions
                                                * SimulationParameters.periodLength);
                classroomSectionList.Add(value);
            }

            classroomSectionList = CleanUpList(classroomSectionList);

            classSections = classroomSectionList;
            PrintList(classSections);
        }

        private void PrintList(List<int> classSections)
        {
            string list = "[";
            foreach (int value in classSections)
            {
                list += value.ToString();
                list += " , ";
            }
            list += "]";
            //Debug.Log(list);
        }

        List<int> CleanUpList(List<int> classroomSectionList)
        {
            List<int> cleanedUpList = new List<int>(classroomSectionList);
            int sumListValues = 0;
            foreach (int value in cleanedUpList)
            {
                sumListValues += value;
            }
            if (sumListValues < SimulationParameters.periodLength)
            {
                int difference = SimulationParameters.periodLength - sumListValues;
                cleanedUpList[0] += difference;
            }
            else if (sumListValues > SimulationParameters.periodLength)
            {
                int difference = sumListValues - SimulationParameters.periodLength;
                cleanedUpList[0] -= difference;
            }
            return cleanedUpList;
        }
    }
}