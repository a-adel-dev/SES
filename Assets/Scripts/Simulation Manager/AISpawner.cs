using SES.Core;
using UnityEngine;
using SES.School;

namespace SES.SimManager
{
    public class AISpawner : MonoBehaviour, IAISpawner
    {
        [SerializeField] IStudentAI studentprefab;
        [SerializeField] ITeacherAI teacherprefab;
        bool schoolHalfCapacity = SimulationParameters.schoolHalfCapacity;
        bool classroomHalfCapacity = SimulationParameters.classroomHalfCapacity;

        IClassroom[] classrooms;
        ISpace[] teacherrooms;

        SchoolDayProgressionController school;

        private void Start()
        {
            school = FindObjectOfType<SchoolDayProgressionController>();
            classrooms = school.subspaces.classrooms;
            teacherrooms = school.subspaces.teachersrooms;
        }

        public void SpawnStudents()
        {

        }

        public void SpawnTeachers()
        {

        }
    }
}