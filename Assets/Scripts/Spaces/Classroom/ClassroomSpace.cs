using UnityEngine;
using SES.Core;
using System.Collections.Generic;
using System;

namespace SES.Spaces.Classroom
{
    public class ClassroomSpace : MonoBehaviour, IClassroom
    {
        ClassroomProgressionControl classScheduler;
        SpaceStudentsBucket studentsBucket { get; set; }
        public SpotBucket classroomSubSpaces { get; set; }

        public Vector3 Entrance
        {
            get
            {
                return classroomSubSpaces.Entrance.position;
            }
        }

        public ITeacherAI Teacher
        {
            get
            {
                return GetComponent<ClassTeacherBucket>().teacher;
            }
            set
            {
                GetComponent<ClassTeacherBucket>().teacher = value;
            }
        }

        private void Start()
        {
            studentsBucket = GetComponent<SpaceStudentsBucket>();
            classroomSubSpaces = GetComponent<SpotBucket>();
            classScheduler = GetComponent<ClassroomProgressionControl>();
        }

        public void StartClass()
        {
            classScheduler.StartClass();
        }

        public void PauseClass()
        {
            classScheduler.PauseClass();
        }

        public void ResumeClass()
        {
            classScheduler.ResumeClass();
        }

        public void EndClass()
        {
            classScheduler.EndClass();
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public Spot RequestDesk(IAI student)
        {
            Spot desk = classroomSubSpaces.GetAvailableDesk(student);
            if (desk != null)
            {
                return desk;
            }
            else
            {
                Debug.LogError($"Could not find a free desk in {gameObject.name} ");
                return null;
            }
        }

        public Spot RequestLocker(IAI student)
        {
            Spot locker =  classroomSubSpaces.GetRandomLocker(student);
            if (locker != null)
            {
                return locker;
            }
            else
            {
                Debug.Log($"Could not find a free locker in {gameObject.name} ");
                return null;
            }
        }

        public List<IStudentAI> ReleaseAllClassStudents()
        {
            classScheduler.EmptyClass();
            List<IStudentAI> studentslist = new List<IStudentAI>();
            List<IStudentAI> spaceStudents = studentsBucket.GetStudentsInSpace();
            List<IStudentAI> outofSpaceStudents = studentsBucket.GetStudentsOutOfSpace();
            foreach (IStudentAI student in spaceStudents)
            {
                student.TransitStudent();
                studentslist.Add(student);
            }

            foreach (IStudentAI student in outofSpaceStudents)
            {
                student.TransitStudent();
                studentslist.Add(student);
            }
            return studentslist;
        }

        public List<IStudentAI> ReleaseClassStudents()
        {
            List<IStudentAI> students = new List<IStudentAI>();
            foreach (IStudentAI student in studentsBucket.GetStudentsInSpace())
            {
                student.TransitStudent();
                students.Add(student);
            }
            foreach (IStudentAI student in studentsBucket.GetStudentsOutOfSpace())
            {
                students.Add(student);
            }

            return students;
        }

        public List<IStudentAI> RequestLabStudents()
        {
            return studentsBucket.GetStudentsInSpace();
        }


        public void EmptyClass()
        {
            studentsBucket.ResetSpace();
            classScheduler.EmptyClass(); 
        }

        public bool IsClassEmpty()
        {
            var state = classScheduler.currentState;
            return state.GetType() == typeof(SClassroomEmpty);
        }

        public void StudentExitClassroom(IStudentAI agent)
        {
            studentsBucket.ReleaseStudent(agent);
        }
        /// <summary>
        /// adds a student to the current class students list
        /// </summary>
        /// <param name="student">student to be added</param>
        public void ReceiveStudent(IStudentAI student)
        {
            studentsBucket.ReceiveStudent(student);
        }

        public void RequestStatus(IStudentAI student)
        {
            if (classScheduler.currentState.GetType() == typeof(SClassroomInSession))
            {
                student.StartClass();
            }

            else if (classScheduler.currentState.GetType() == typeof(SClassroomOnBreak))
            {
                student.BreakTime();
            }

            else if (classScheduler.currentState.GetType() == typeof(SClassActivity))
            {
                student.StartActivity();
            }
        }
        /// <summary>
        /// puts the students in the class students list, assign them the class,
        /// nulls their lab, and assign them a desk each.
        /// </summary>
        /// <param name="students">A list of the students to be marked</param>
        public void MarkStudents(List<IStudentAI> students)
        {
            foreach (IStudentAI student in students)
            {
                student.CurrentClassroom = this;
                student.CurrentLab = null;
                student.currentDesk = RequestDesk(student);
                studentsBucket.ReceiveStudent(student);
            }
        }

        public void InitializeSubSpaces()
        {
            classroomSubSpaces.Initialize();
        }
    }
}
