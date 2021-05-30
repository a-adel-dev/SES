using System.Collections.Generic;
using UnityEngine;
using SES.Core;
using SES.Spaces;
using SES.Spaces.Classroom;

namespace SES.School
{
    public class SchoolDayProgressionController : MonoBehaviour, ISchool
    {
        public SchoolSubSpacesBucket subspaces { get; set; }
        public string SchoolState = "";
        List<IClassroom> remainingEgressClassrooms;
        List<IClassroom> workingClasses = new List<IClassroom>();
        public int remainingEgressStudents { get; set; }
        List<ClassLabPair> classlabPairs = new List<ClassLabPair>();


        #region FSm
        private SSchoolBaseState currentState;
        private SSchoolBaseState pausedState;

        public readonly SSchoolClassesInSession classesInSession = new SSchoolClassesInSession();
        public readonly SSchoolBreakTime breakTime = new SSchoolBreakTime();
        public readonly SSchoolEgressTime egressTime = new SSchoolEgressTime();
        public readonly SSchoolOffTime offTime = new SSchoolOffTime();
        public readonly SSimOver simOver = new SSimOver();
        public readonly SSchoolPaused paused = new SSchoolPaused();

        public void TransitionToState(SSchoolBaseState state)
        {
            currentState = state;
            currentState.EnterState(this);
        }
        #endregion

        private void Update()
        {
            if (currentState != null)
            {
                currentState.Update(this);
            }
        }

        public void InitializeProperties()
        {
            subspaces = GetComponent<SchoolSubSpacesBucket>();  
        }

        public void StartSchoolDay()
        {
            
            TransitionToState(classesInSession);
            foreach (EgressPoint stair in subspaces.staircases)
            {
                stair.AddStudentEgressListener(HandleStudentEgress);
            }
        }

        public void PauseSchool()
        {
            currentState.resumed = true;
            pausedState = currentState;
            TransitionToState(paused);
        }

        public void ResumeSchool()
        {
            if (pausedState != null)
            {
                TransitionToState(pausedState);
                pausedState = null;
            }
        }

        public void StartPeriod()
        {
            remainingEgressClassrooms = new List<IClassroom>(subspaces.classrooms);
            remainingEgressStudents = TotalAgentsBucket.GetStudents().Count;
            if (SimulationParameters.RelocationEnabled)
            {
                //create ClassLab Association
                CreateClassLabPairs();
                //Send classes to their labs
                SendClassesToLab();
                //create workin classes list
                CreateWorkingClassesList();
                foreach (ClassLabPair pair in classlabPairs)
                {
                    pair.lab.StartLab();
                }
            }
            else
            {
                workingClasses = new List<IClassroom>(subspaces.classrooms);
            }

            foreach (IClassroom classroom in workingClasses)
            {
                classroom.StartClass();
            }

        }

        public void PauseClasses()
        {
            foreach (IClassroom classroom in subspaces.classrooms)
            {
                classroom.PauseClass();
            }
        }

        public void ResumeClasses()
        {
            foreach (IClassroom classroom in subspaces.classrooms)
            {
                classroom.ResumeClass();
            }
        }
        public void EndPeriod()
        {
            foreach (ClassLabPair pair in classlabPairs)
            {
                List<IStudentAI> allStudents = pair.lab.ReleaseLabStudents();
                List<IStudentAI> studentsInLab = pair.lab.GetStudentsInLab();
                pair.lab.EndLab();

                pair.classroom.MarkStudents(allStudents);
                foreach (IStudentAI student in studentsInLab)
                {
                    pair.classroom.ReceiveStudent(student);
                    student.TransitStudent();
                    student.BackToDesk();
                    student.BreakTime();
                }  
            }
            classlabPairs.Clear();
            foreach (IClassroom classroom in subspaces.classrooms)
            {
                classroom.EndClass();
            }
        }
        /// <summary>
        /// Egress A cluster of classes equal to the number of egress points
        /// in school
        /// </summary>
        public void EgressClassGroup()
        {
            //Debug.Log($"Remaining students: {remainingEgressStudents}");
            if (remainingEgressClassrooms.Count <= 0)
            {
                return;
            }
            //for all egress points
            foreach (EgressPoint stairs in subspaces.staircases)
            {
                //pick its nearest class from the remaining egress classroom list 
                IClassroom nearestClassroom = FindNearestClassroom(stairs, remainingEgressClassrooms);
                if (nearestClassroom != null)
                {
                    List<IStudentAI> studentsToEgress = new List<IStudentAI>();
                    //release control to the school
                    studentsToEgress = nearestClassroom.ReleaseAllClassStudents();
                    remainingEgressClassrooms.Remove(nearestClassroom);
                    //send the class to the egress point
                    foreach (IStudentAI student in studentsToEgress)
                    {
                        //Debug.Log($"Navigating students to egress point");
                        student.GoToAnotherLevel(stairs.gameObject.transform.position);
                    }
                }
            }
        }
        public IClassroom FindNearestClassroom(ISpace space, List<IClassroom> classrooms)
        {
            float dist = 100000f;
            IClassroom selectedClass = null;
            Vector3 spacePos = space.GetGameObject().transform.position;
            foreach (IClassroom classroom in classrooms)
            {
                //Debug.Log($"Distance is {Vector3.Distance(classroom.GetGameObject().transform.position,spacePos)}");
                if (classroom.IsClassEmpty() == false && Vector3.Distance(classroom.GetGameObject().transform.position,
                                                                            spacePos) < dist)
                {
                    selectedClass = classroom;
                    dist = Vector3.Distance(selectedClass.GetGameObject().transform.position,
                                            spacePos);
                }
            }
            return selectedClass;
        }

        private void HandleStudentEgress()
        {
            remainingEgressStudents--;
            //Debug.Log($"egress");
        }

        public Bathroom RequestBathroom(IAI agent)
        {
            return subspaces.GetNearestBathroom(agent);
        }

        void CreateClassLabPairs()
        {
            int randomIndex = Random.Range(1, subspaces.labs.Length);
            List<ClassroomSpace> classrooms = ListHandler.Shuffle(subspaces.classrooms);
            List<ILab> labs = new List<ILab>();
            foreach (ILab lab in subspaces.labs)
            {
                labs.Add(lab);
            }
            labs = ListHandler.Shuffle(labs);
            for (int i = 0; i < Mathf.Min(classrooms.Count, randomIndex); i++)
            {
                classlabPairs.Add(new ClassLabPair(classrooms[i], labs[i]));
            }
        }

        void SendClassesToLab()
        {
            foreach (ClassLabPair pair in classlabPairs)
            {
                List<IStudentAI> totalStudents = pair.classroom.ReleaseClassStudents();
                List<IStudentAI> inClassStudents = pair.classroom.RequestLabStudents();
                pair.classroom.ClearClassStudents();

                pair.lab.MarkStudents(totalStudents);                    

                foreach (IStudentAI student in inClassStudents)
                {
                    pair.lab.ReceiveStudent(student);
                    student.TransitStudent();
                }
            }
        }

        void CreateWorkingClassesList()
        {
            workingClasses.Clear();
            foreach (ClassroomSpace classroom in subspaces.classrooms)
            {
                bool isInClassLabPairs = false;
                foreach (ClassLabPair pair in classlabPairs)
                {
                    if (pair.classroom as ClassroomSpace == classroom)
                    {
                        isInClassLabPairs = true;
                    }
                }
                if (isInClassLabPairs == false)
                {
                    workingClasses.Add(classroom);
                }
            }
        }
    }
}