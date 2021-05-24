using SES.Core;
using UnityEngine;
using SES.School;
using System.Collections.Generic;
using SES.Spaces.Classroom;
using SES.Spaces;
using SES.AIControl;
using UnityEngine.AI;

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
                int counter = 1;
                if (classroomHalfCapacity)
                {
                    for (int i = 0; i < classroom.classroomSubSpaces.desks.Count; i = i + 2)
                    {
                        GameObject student = Instantiate(studentprefab, classroom.classroomSubSpaces.desks[i].transform.position, Quaternion.identity);
                        student.name = $"{classroom.name}_student_{counter}";
                        AddToClassroom(classroom, student);
                        student.GetComponent<StudentBehaviorControl>().AssignDesk(classroom.classroomSubSpaces.desks[i]);
                        counter++;
                    }
                }
                else
                {
                    foreach (Spot desk in classroom.classroomSubSpaces.desks)
                    {
                        GameObject student = Instantiate(studentprefab, desk.transform.position, Quaternion.identity);
                        student.name = $"{classroom.name}_student_{counter}";
                        AddToClassroom(classroom, student);
                        student.GetComponent<StudentBehaviorControl>().AssignDesk(desk);
                        counter++;
                    }
                }
            }
        }

        private static void AddToClassroom(ClassroomSpace classroom, GameObject student)
        {
            StudentBehaviorControl behavior = student.GetComponent<StudentBehaviorControl>();
            classroom.studentsBucket.AddToSpaceOriginalStudents(student.GetComponent<IStudentAI>());
            classroom.studentsBucket.ReceiveStudent(student.GetComponent<IStudentAI>());
            student.transform.parent = classroom.transform;
            behavior.AssignOriginalPosition();
            student.GetComponent<NavMeshAgent>().speed = SimulationDefaults.childrenWalkingSpeed * (60f / SimulationParameters.timeStep);
            TotalAgentsBucket.AddToStudents(behavior);

            behavior.IdleAgent();
            behavior.AssignMainClassroom(classroom);
            behavior.InitializeProperties();
        }

        public void SpawnTeachers()
        {
            int counter = 1;
            foreach (ClassroomSpace classroom in classrooms)
            {
                GameObject teacher = Instantiate(teacherprefab, classroom.classroomSubSpaces.entrance.transform.position, Quaternion.identity);
                NavMeshAgent teacherNav = teacher.GetComponent<NavMeshAgent>();
                teacherNav.speed = SimulationDefaults.adultWalkingSpeed * (60f / SimulationDefaults.timeStep);
                teacher.name = $"teacher_{counter}";
                TeacherBehaviorControl behavior = teacher.GetComponent<TeacherBehaviorControl>();
                TotalAgentsBucket.AddToTeachers(behavior);
                behavior.SetCurrentClassroom(classroom);
                behavior.WanderRestricted();
            }

            for (int i = 0; i < 2; i++)
            {
                foreach (Lab lab in school.subspaces.labs)
                {
                    GameObject teacher = Instantiate(teacherprefab, lab.labObjects.entrance.transform.position, Quaternion.identity);
                    NavMeshAgent teacherNav = teacher.GetComponent<NavMeshAgent>();
                    teacherNav.speed = SimulationDefaults.adultWalkingSpeed * (60f / SimulationDefaults.timeStep);
                    teacher.name = $"teacher_{counter}";
                    TotalAgentsBucket.AddToTeachers(teacher.GetComponent<TeacherBehaviorControl>());
                }
            }

            foreach (Teachersroom teachersroom in school.subspaces.teachersrooms)
            {
                for (int i = 0; i < teachersroom.subspaces.desks.Count; i = i + 2)
                {
                    GameObject teacher = Instantiate(teacherprefab, teachersroom.subspaces.desks[i].transform.position, Quaternion.identity);
                    NavMeshAgent teacherNav = teacher.GetComponent<NavMeshAgent>();
                    teacherNav.speed = SimulationDefaults.adultWalkingSpeed * (60f / SimulationDefaults.timeStep);
                    teacher.name = $"teacher_{counter}";
                    TotalAgentsBucket.AddToTeachers(teacher.GetComponent<TeacherBehaviorControl>());
                }
            }
            
        }

    }
}