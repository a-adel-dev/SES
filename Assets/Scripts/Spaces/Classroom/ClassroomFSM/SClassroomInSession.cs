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
        List<int> classSections = new List<int>();

        public override void EnterState(ClassroomPeriodSchedular schedular)
        {
            InitializeValues();
            StructureClass();
            CheckActivity();
        }

        public override void Update(ClassroomPeriodSchedular schedular)
        {
            PassTime();
        }

        void InitializeValues()
        {
            minSectionNumber = SimulationParameters.minClassSectionNumber;
            maxSectionNumber = SimulationParameters.maxClassSectionNumber;
            timeStep = SimulationParameters.timeStep;
        }

        private void PassTime()
        {
            timer += Time.deltaTime;
            if (timer >= timeStep)
            {
                if (lastSection)
                {
                    return;
                }
                if (currentSectionIndex + 1 == classSections.Count)
                {
                    CheckActivity();
                    lastSection = true;
                    return;
                }
                timer -= timeStep;
                sessionTimer++;

                if (sessionTimer >= classSections[currentSectionIndex])
                {
                    currentSectionIndex++;
                    sessionTimer = 0;
                    CheckActivity();
                }
            }
        }

        private void CheckActivity()
        {

            if (classSections[currentSectionIndex] >= SimulationParameters.minClassActivityTime)
            {
                Debug.Log("Activity");
            }
            else
            {
                Debug.Log("No activity");
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
            classSections = classroomSectionList;
        }
    }
}