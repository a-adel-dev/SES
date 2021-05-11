using SES.Core;
using UnityEngine;
using SES.School;
using System.Collections.Generic;
using SES.Spaces.Classroom;
using SES.Spaces;

namespace SES.SimManager
{
    public class AISpawner : MonoBehaviour, IAISpawner
    {
        [SerializeField] GameObject studentprefab;
        [SerializeField] GameObject teacherprefab;

        bool classroomHalfCapacity;

        List<ClassroomSpace> classrooms = new List<ClassroomSpace>();
        Teachersroom[] teacherrooms;

        SchoolDayProgressionController school;

        private void Start()
        {
            school = FindObjectOfType<SchoolDayProgressionController>();
        }

        public void Initialize()
        {
            classrooms = school.subspaces.classrooms;
            teacherrooms = school.subspaces.teachersrooms;
            classroomHalfCapacity = SimulationParameters.classroomHalfCapacity;
        }

        public void SpawnStudents()
        {
            foreach (ClassroomSpace classroom in classrooms)
            {
                if (classroomHalfCapacity)
                {
                    for (int i = 0; i < classroom.classroomSubSpaces.desks.Count; i = i + 2)
                    {
                        Instantiate(studentprefab, classroom.classroomSubSpaces.desks[i].transform.position, Quaternion.identity);
                    }
                }
                else
                {
                    foreach (Spot desk in classroom.classroomSubSpaces.desks)
                    {
                        Instantiate(studentprefab, desk.transform.position, Quaternion.identity);
                    }
                }
            }
        }

        public void SpawnTeachers()
        {

        }
    }
}