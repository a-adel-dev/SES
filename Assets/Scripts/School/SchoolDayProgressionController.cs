using System.Collections.Generic;
using UnityEngine;
using SES.Core;
using System;
using SES.Spaces;

namespace SES.School
{
    public class SchoolDayProgressionController : MonoBehaviour, ISchool
    {
        public SchoolSubSpacesBucket subspaces;
        public string SchoolState = "";
        public bool activitiesEnabled;
        public bool relocationEnabled;
        public List<IClassroom> remainingEgressClassrooms;
        public int remainingEgressStudents;

        public int periodLength { get; set; } = 45;
        public int breakLength { get; set; } = 5;
        public int numPeriods { get; set; } = 2;
        public int simLength { get; set; } = 2;
        public float timeStep { get; set; } = 0.5f;



        #region FSm
        private SSchoolBaseState currentState;
        private SSchoolBaseState pausedState;

        public readonly SClassesInSession classesInSession = new SClassesInSession();
        public readonly SBreakTime breakTime = new SBreakTime();
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
            //Create ClassLabPairs
            //allocate classes to send to labs
            //Instruct classes in classLabPairs to releaseStudents to 'thhis' control
            //ask the labs for a list of lab positions to assign to students
            //Direct released classes students to their Labs
            //relinquish students control to labs
            //start classes in the rest of the classes.

            foreach (IClassroom classroom in subspaces.classrooms)
            {
                classroom.SetActivities(activitiesEnabled);
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
    }
}