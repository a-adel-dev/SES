using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SES.Core;

namespace SES.Spaces.Classroom
{
    public class ClassroomPeriodSchedular : MonoBehaviour
    {
        int periodTime = 40;
        SchoolDayState schoolDayState;
        public int activeSectionIndex { get; private set; } = 0;
        //ActivityPlanner activityPlanner;
        List<int> classStructureTimes = new List<int>();
        public int classTime { get; private set; } = 0;
        public int sectionTime { get; private set; } = 0;

        // Start is called before the first frame update
        void Start()
        {
            //activityPlanner = GetComponent<ActivityPlanner>();
        }

        // Update is called once per frame
        void Update()
        {
            EvolveClassTime();
        }

        public void TimeStep()
        {
            sectionTime++;
        }

        public List<int> StructureAClass()
        {
            //Debug.Log($"structuring class");
            int classSections = Random.Range(2, 8); // generate random class sections
            List<int> randomPartitions = new List<int>(); //a list to hold the partition numbers

            for (int i = 1; i < classSections + 1; i++)
            {
                randomPartitions.Add(Random.Range(1, 10));
            }

            int totalOfRandomPartitions = 0;

            for (int i = 0; i < randomPartitions.Count; i++)
            {
                totalOfRandomPartitions += randomPartitions[i];
            }

            for (int i = 0; i < randomPartitions.Count; i++)
            {
                classStructureTimes.Add(Mathf.FloorToInt(periodTime * (randomPartitions[i] / (float)totalOfRandomPartitions)));
            }

            return classStructureTimes;
        }

        public void ResetClassStructure()
        {
            classStructureTimes = new List<int>();
            activeSectionIndex = 0;
            //Debug.Log("class structure destroyed!");
        }

        public void EvolveClassTime()
        {
            if (schoolDayState != SchoolDayState.classesInSession || classStructureTimes.Count <= 0 )//TODO: Make sure to not start the class if it was empty
                return;
            if (sectionTime < classStructureTimes[activeSectionIndex])
            {
                //Debug.Log("class section no. " + (activeSection + 1).ToString());
                //there is a logic bug here as activities can not set to be on during the first section
            }
            else if (sectionTime >= classStructureTimes[activeSectionIndex])
            {
                sectionTime -= classStructureTimes[activeSectionIndex];
                if (activeSectionIndex < classStructureTimes.Count - 1)
                {
                    activeSectionIndex++;
                    //activityPlanner.SetActivityStatus(classStructureTimes[activeSectionIndex]);
                }
            }
        }

        public List<int> GetClassStructureTimes()
        {
            return classStructureTimes;
        }

        public void SetSchoolDayState(SchoolDayState state)
        {
            schoolDayState = state;
        }

        public void SetPeriodTime(int time)
        {
            periodTime = time;
        }
    }
}
