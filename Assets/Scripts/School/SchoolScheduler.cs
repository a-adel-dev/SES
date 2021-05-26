using System.Collections.Generic;
using UnityEngine;
using SES.Core;
using SES.Spaces;
using SES.Spaces.Classroom;

namespace SES.School
{
    public class SchoolScheduler : MonoBehaviour, ISchool
    {
        public SchoolSubSpacesBucket subspaces;
        public string SchoolState = "";
        public bool activitiesEnabled;
        public bool relocationEnabled;
        public List<IClassroom> remainingEgressClassrooms;
        public int remainingEgressStudents;
        public List<ClassroomSpace> workingClassrooms = new List<ClassroomSpace>();

        List<ClassLabPair> classLabPairs = new List<ClassLabPair>();

        public int periodLength { get; set; } = 45;
        public int breakLength { get; set; } = 5;
        public int numPeriods { get; set; } = 2;
        public int simLength { get; set; } = 2;
        public float timeStep { get; set; } = 0.5f;



        #region FSm
        public SSchoolBaseState currentState;
        private SSchoolBaseState pausedState;

        public readonly SClassesInSession classesInSession = new SClassesInSession();
        public readonly SSchoolBreakTime breakTime = new SSchoolBreakTime();
        public readonly SEgressTime egressTime = new SEgressTime();
        public readonly SOffTime offTime = new SOffTime();
        public readonly SSimOver simOver = new SSimOver();
        public readonly SPaused paused = new SPaused();

        public void TransitionToState(SSchoolBaseState state)
        {
            currentState = state;
            currentState.EnterState(this);
        }
        #endregion
        private void Start()
        {
            subspaces = GetComponent<SchoolSubSpacesBucket>();
        }

        private void Update()
        {
            if (currentState != null)
            {
                currentState.Update(this);
            }
        }
        public void InitializeProperties()
        {
            periodLength = SimulationParameters.periodLength;
            breakLength = SimulationParameters.breakLength;
            numPeriods = SimulationParameters.numPeriods;
            simLength = SimulationParameters.simLength;
            timeStep = SimulationParameters.timeStep;
            activitiesEnabled = SimulationParameters.activitiesEnabled;
            relocationEnabled = SimulationParameters.relocationEnabled;
            remainingEgressClassrooms = new List<IClassroom>(subspaces.classrooms);
            remainingEgressStudents = TotalAgentsBucket.GetStudents().Count;
            workingClassrooms = new List<ClassroomSpace>(subspaces.classrooms);
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
            foreach (IClassroom classroom in workingClassrooms)
            {
                classroom.SetActivities(activitiesEnabled);
                classroom.StartClass();
            }
        }
        public void PauseClasses()
        {
            foreach (IClassroom classroom in workingClassrooms)
            {
                classroom.PauseClass();
            }
        }

        public void ResumeClasses()
        {
            foreach (IClassroom classroom in workingClassrooms)
            {
                classroom.ResumeClass();
            }
        }
        public void EndPeriod()
        {
            foreach (IClassroom classroom in workingClassrooms)
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
                    studentsToEgress = nearestClassroom.ReleaseClass();
                    remainingEgressClassrooms.Remove(nearestClassroom);
                    //send the class to the egress point
                    foreach (IStudentAI student in studentsToEgress)
                    {
                        //Debug.Log($"Navigating students to egress point");
                        student.NavigateTo(stairs.gameObject.transform.position);
                    }
                }
            }
        }

        internal void ResetEgress()
        {
            remainingEgressClassrooms = new List<IClassroom>(subspaces.classrooms);
            remainingEgressStudents = TotalAgentsBucket.GetStudents().Count;
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

        //should be an event listener
        public void EgressStudent()
        {
            remainingEgressStudents--;
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

        public void RelocateClasses()
        {
            if (SimulationDefaults.relocationEnabled == false) { return; }
            workingClassrooms = ListHandler.Shuffle(workingClassrooms);
            for (int i = 0; i < subspaces.labs.Length; i++)
            {
                //create associations between classes and labs
                classLabPairs.Add(new ClassLabPair(workingClassrooms[i], subspaces.labs[i]));
                
                //recieve a list of students from the class
                //set class to empty
                List<IStudentAI> classStudents = workingClassrooms[i].ReleaseClass();

                //set students lab to this lab
                //ask the lab to receive the list of students
                foreach (IStudentAI student in classStudents)
                {
                    student.AssignLab(subspaces.labs[i]);
                    subspaces.labs[i].ReceiveStudent(student);

                    student.StartClass();
                }
                workingClassrooms.Remove(workingClassrooms[i]);
            }
        }

        public void DecoupleClassLabs()
        {
            if (classLabPairs.Count <= 0 || classLabPairs == null)
            {
                return;
            }
            //for each entry in classlab pairs
            foreach (ClassLabPair pair in classLabPairs)
            {
                //receive students from lab
                List<IStudentAI> students =  pair.lab.EndLab();
                Debug.Log($"recieved {students.Count} students from {pair.lab.GetGameObject().name}");
                foreach (IStudentAI student in students)
                {
                    student.ClearCurrentLab();
                    //set their current class
                    student.AssignCurrentClassroom(pair.classroom);
                    Debug.Log($"assigning {pair.classroom.GetGameObject().name} to {student.GetGameObject().name}");
                    student.currentDesk = pair.classroom.RequestDesk(student);
                    //send them back to their class
                    pair.classroom.ReceiveStudent(student);
                    student.BackToDesk();
                }
            }
            //Destroy classlabpairs
            classLabPairs.Clear();
            //Reset working classes
            workingClassrooms = new List<ClassroomSpace>(subspaces.classrooms);
        }
    }
}