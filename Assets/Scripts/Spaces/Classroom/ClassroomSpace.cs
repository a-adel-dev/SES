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

        public Vector3 entrance
        {
            get
            {
                return classroomSubSpaces.entrance.transform.position;
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
            foreach (Spot desk in classroomSubSpaces.desks)
            {
                if (desk.ISpotAvailable())
                {
                    desk.FillSpot(student);
                    return desk;
                }
            }
            return null;
        }

        public Spot RequestLocker(IAI student)
        {
            foreach (Spot locker in ListHandler.Shuffle(classroomSubSpaces.lockers))
            {
                if (locker.ISpotAvailable())
                {
                    locker.FillSpot(student);
                    return locker;
                }
            }
            return null;
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

        public bool IsClassEmpty()
        {
            var state = classScheduler.currentState;
            return state.GetType() == typeof(SClassroomEmpty);
        }

        public void StudentExitClassroom(IStudentAI agent)
        {
            studentsBucket.ReleaseStudent(agent);
        }

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
    }
}
