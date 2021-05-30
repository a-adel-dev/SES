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

        List<IClassroom> classrooms = new List<IClassroom>();
        Teachersroom[] teacherrooms;

        SchoolDayProgressionController school;

        private void Start()
        {
            school = FindObjectOfType<SchoolDayProgressionController>();
        }

        public void Initialize()
        {
            foreach (ClassroomSpace classroom in school.subspaces.classrooms)
            {
                classrooms.Add(classroom as IClassroom);
            }
            teacherrooms = school.subspaces.teachersrooms;
        }

        public void SpawnStudents()
        {
            foreach (IClassroom classroom in classrooms)
            {
                int counter = 1;
                int spawnCounter = classroom.classroomSubSpaces.GetAvailableDesksCount();
                for (int i = 0; i < spawnCounter; i++)
                {
                    Spot desk = classroom.classroomSubSpaces.GetAvailableDesk(); //get an available desk
                    //instantiate student
                    GameObject student = Instantiate(studentprefab, desk.transform.position, Quaternion.identity);
                    //name the student object
                    student.name = $"{classroom.GetGameObject().name}_student_{counter}";
                    //Set student class parameters
                    AddToClassroom(classroom, student, desk);
                    //increase the counter
                    counter++;
                }
            }
        }

        private static void AddToClassroom(IClassroom classroom, GameObject student, Spot desk)
        {
            //get the student AI
            StudentBehaviorControl behavior = student.GetComponent<StudentBehaviorControl>();
            behavior.school = FindObjectOfType<SchoolDayProgressionController>();//assign the school
            classroom.ReceiveStudent(student.GetComponent<IStudentAI>());//add student to the class
            behavior.CurrentClassroom = classroom;//assign student current classroom
            
            behavior.currentDesk = desk;//assign desk to the student
            desk.FillSpot(behavior);//fill desk with student

            
            student.transform.parent = classroom.GetGameObject().transform;//make the student a child of the class
            behavior.SetSpawnLocation();//set student original position for dat reset purpose
            //Set Student speed
            student.GetComponent<NavMeshAgent>().speed = SimulationDefaults.childrenWalkingSpeed
                                                        * (60f / SimulationParameters.TimeStep);
            TotalAgentsBucket.AddToStudents(behavior);//add student to the agent list
            
            behavior.IdleAgent();//stop student
        }

        public void SpawnTeachers()
        {
            int counter = 1;
            int teacherroomIndex = 0;
            //Spawn classroom teachers
            foreach (ClassroomSpace classroom in classrooms)
            {
                //Debug.Log($"{classroom.classroomSubSpaces.Entrance}");
                GameObject teacher = Instantiate(teacherprefab, classroom.classroomSubSpaces.Entrance.position, Quaternion.identity);
                NavMeshAgent teacherNav = teacher.GetComponent<NavMeshAgent>();
                teacherNav.speed = SimulationDefaults.adultWalkingSpeed * (60f / SimulationDefaults.timeStep);
                teacher.name = $"teacher_{counter}";
                TeacherBehaviorControl behavior = teacher.GetComponent<TeacherBehaviorControl>();
                TotalAgentsBucket.AddToTeachers(behavior);
                behavior.SetCurrentClassroom(classroom);
                classroom.Teacher = behavior;

                
                behavior.teacherroom = school.subspaces.teachersrooms[teacherroomIndex];
                teacherroomIndex++;
                if (teacherroomIndex >= school.subspaces.teachersrooms.Length)
                {
                    teacherroomIndex = 0;
                }
                counter++;
            }
            //spawn lab teachers
            for (int i = 0; i < 2; i++)
            {
                foreach (Lab lab in school.subspaces.labs)
                {
                    GameObject teacher = Instantiate(teacherprefab, lab.SubSpaces.Entrance.position, Quaternion.identity);
                    NavMeshAgent teacherNav = teacher.GetComponent<NavMeshAgent>();
                    TeacherBehaviorControl behavior = teacher.GetComponent<TeacherBehaviorControl>();
                    teacherNav.speed = SimulationDefaults.adultWalkingSpeed * (60f / SimulationDefaults.timeStep);
                    teacher.name = $"teacher_{counter}";
                    TotalAgentsBucket.AddToTeachers(teacher.GetComponent<TeacherBehaviorControl>());
                    behavior.currentLab = lab;
                    behavior.ClassroomFree();
                    counter++;
                }
            }
            //spawn teacherrooms teachers
            foreach (ITeachersroom teachersroom in teacherrooms)
            {
                int spawnCounter = teachersroom.subspaces.GetAvailableDesksCount();
                for (int i = 0; i < spawnCounter; i = i + 2)
                {
                    Spot desk = teachersroom.subspaces.GetAvailableDesk(); //get an available desk
                    GameObject teacher = Instantiate(teacherprefab, desk.transform.position, Quaternion.identity);
                    NavMeshAgent teacherNav = teacher.GetComponent<NavMeshAgent>();
                    TeacherBehaviorControl behavior = teacher.GetComponent<TeacherBehaviorControl>();
                    teacherNav.speed = SimulationDefaults.adultWalkingSpeed * (60f / SimulationDefaults.timeStep);
                    teacher.name = $"teacher_{counter}";
                    TotalAgentsBucket.AddToTeachers(behavior);
                    TotalAgentsBucket.AddToAvailableTeachers(behavior);
                    behavior.teacherroom = teachersroom;
                    behavior.teacherroom.AddToTeachersInRoom(behavior);
                    behavior.Rest();
                    counter++;
                }
            }
            
        }
    }
}