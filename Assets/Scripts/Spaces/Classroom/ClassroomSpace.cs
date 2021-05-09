using UnityEngine;
using SES.Core;
using System.Collections.Generic;

namespace SES.Spaces.Classroom
{
    public class ClassroomSpace : MonoBehaviour, IClassroom
    {
        public ClassroomPeriodSchedular classScheduler;
        //public SpaceStudentsBucket studentsBucket;
        public SpotBucket classroomSubSpaces;
        //public AgentSpawner spawner;
        //public BehaviorController studentController;
        ////public ActivityPlanner planner;
        ////SchoolDayState schoolDayState;
        ////bool classEmpty = false;
        ////bool spawned = false;
        ////[SerializeField] GameObject teacherPrefab;
        ////[SerializeField] List<Spot> teacherSpawnPoint = new List<Spot>();

        //private void Awake()
        //{
        //    classScheduler = GetComponent<ClassroomPeriodSchedular>();
        //    studentsBucket = GetComponent<SpaceStudentsBucket>();
        //    
        //    spawner = GetComponent<AgentSpawner>();
        //    studentController = GetComponent<BehaviorController>();

        //}

        private void Start()
        {
            classroomSubSpaces = GetComponent<SpotBucket>();
            classScheduler = GetComponent<ClassroomPeriodSchedular>();
            //SpawnAgents();
            //planner = GetComponent<ActivityPlanner>();
        }

        ////public void SpawnAgents()
        ////{
        ////    if (spawned) { return; }
        ////    spawner.SpawnAgents(classroomSubSpaces.desks);
        ////    spawner.SpawnAgents(teacherPrefab, teacherSpawnPoint);
        ////    spawned = true;
        ////}

        public void StartClass()
        {
            classScheduler.StartClass();
            Debug.Log($"starting {gameObject.name}");
            //studentController.ResetPupilBehavior();
        }

        public void PauseClass()
        {
            classScheduler.PauseClass();
            Debug.Log($"Pausing Class {gameObject.name}");
        }

        public void ResumeClass()
        {
            classScheduler.ResumeClass();
            Debug.Log($"resuming Class {gameObject.name}");
        }

        public void EndClass()
        {
            classScheduler.EndClass();
            Debug.Log($"Ending Class {gameObject.name}");
            //studentController.FreePupilsBehavior();

        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        ////    public void SendClassToLab(Lab lab)
        ////    {
        ////        if (studentsBucket.studentsCurrentlyInSpace.Count == 0) { return; }
        ////        //foreach of the students 
        ////        foreach (IStudentAI student in studentsBucket.studentsCurrentlyInSpace)
        ////        {
        ////            //assign all students to a lab
        ////            student.SetCurrentLab(lab);
        ////            //set students status to inLab
        ////            student.SetStudentLocationTo(StudentState.inLab);
        ////            student.AssignLab(lab);
        ////            student.GetLabPosition(lab);

        ////        }
        ////        foreach (IStudentAI student in studentsBucket.spaceOriginalStudents)
        ////        {
        ////            student.SetControlledTo(true);
        ////            student.GoToLab();
        ////            student.Enterlab(lab);
        ////        }
        ////        studentsBucket.ClearStudentsInSpace();
        ////        classEmpty = true;
        ////        //TODO: check the status of out of class pupils
        ////    }

        ////    public void RecieveStudents()
        ////    {
        ////        classEmpty = false;
        ////    }


        ////    public void EgressClass(Vector3 exit)
        ////    {
        ////        return;
        ////        //studentController.EgressClass(exit);
        ////    }


        ////    public void SetSchoolDayState(SchoolDayState state)
        ////    {
        ////        schoolDayState = state;
        ////        classScheduler.SetSchoolDayState(state);
        ////    }

        ////    public void SetTimeStep(float timeStep)
        ////    {
        ////        throw new System.NotImplementedException();
        ////    }


    }
}
