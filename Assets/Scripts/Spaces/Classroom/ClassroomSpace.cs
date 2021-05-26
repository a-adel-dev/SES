using UnityEngine;
using SES.Core;
using System.Collections.Generic;

namespace SES.Spaces.Classroom
{
    public class ClassroomSpace : MonoBehaviour, IClassroom
    {
        public ClassroomProgressionControl classScheduler;
        public SpaceStudentsBucket studentsBucket { get; set; }
        public SpotBucket classroomSubSpaces { get; set; }
        public ClassroomTeacherBucket teacher;

        public ITeacherAI classTeacher
        {
            get
            {
                return teacher.teacher;
            }

            set
            {
                teacher.teacher = value;
            }
        }



        private void Start()
        {
            studentsBucket = GetComponent<SpaceStudentsBucket>();
            teacher = GetComponent<ClassroomTeacherBucket>();
            if (studentsBucket == null)
            {
                Debug.Log($"didn't acquire studentsbucket for {gameObject.name}");
            }
            
            classroomSubSpaces = GetComponent<SpotBucket>();
            classScheduler = GetComponent<ClassroomProgressionControl>();
        }

        public void StartClass()
        {
            classScheduler.StartClass();
            //Debug.Log($"starting {gameObject.name}");
        }

        public SpotBucket GetClassroomSubspaces()
        {
            return classroomSubSpaces;
        }

        public void PauseClass()
        {
            classScheduler.PauseClass();
            //Debug.Log($"Pausing Class {gameObject.name}");
        }

        public void ResumeClass()
        {
            classScheduler.ResumeClass();
            //Debug.Log($"resuming Class {gameObject.name}");
        }

        public void EndClass()
        {
            classScheduler.EndClass();
            //Debug.Log($"Ending Class {gameObject.name}");
            //studentController.FreePupilsBehavior();

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

        public void SetActivities(bool state)
        {
            classScheduler.SetActivitiesEnabledTo(state);
        }

        public List<IStudentAI> ReleaseClass()
        {
            EmptyClass();
            List<IStudentAI> studentslist = new List<IStudentAI>();
            foreach (Spot desk in classroomSubSpaces.desks)
            {
                desk.ClearSpot();
            }
            
            foreach (IStudentAI student in studentsBucket.spaceOriginalStudents)
            {
                student.ClearCurrentClass();
                student.currentDesk = null;
                studentslist.Add(student);
                studentsBucket.studentsCurrentlyInSpace.Remove(student);
            }
            return studentslist;
        }

        public void EmptyClass()
        {

            classScheduler.EmptyClass();
        }

        public bool IsClassEmpty()
        {
            var state = classScheduler.currentState;
            return state.GetType() == typeof(SClassroomEmpty);
        }

        public void ExitClassroom(IStudentAI agent)
        {
            studentsBucket.studentsCurrentlyInSpace.Remove(agent);
        }

        public void ReceiveStudent(IStudentAI student)
        {
            studentsBucket.studentsCurrentlyInSpace.Add(student);
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



    }
}
