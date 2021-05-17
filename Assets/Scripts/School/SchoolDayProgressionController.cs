using System.Collections.Generic;
using UnityEngine;
using SES.Core;
using System;
using SES.Spaces;

namespace SES.School
{
    public class SchoolDayProgressionController : MonoBehaviour
    {
        public int periodLength { get; set; } = 45;
        public int breakLength { get; set; } = 5;
        public int numPeriods { get; set; } = 2;
        public int simLength { get; set; } = 2;
        public float timeStep { get; set; } = 0.5f;


        public SchoolSubSpacesBucket subspaces;
        public string SchoolState = "";
        public bool activitiesEnabled;



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
        }

        public void StartSchoolDay()
        {
            TransitionToState(classesInSession);
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
        public bool EgressClassGroup()
        {
            //TODO: pass in the remaining classes.
            bool success = false;
            //for all egress points
            foreach (EgressPoint stairs in subspaces.staircases)
            {
                List<IClassroom> remainingClassrooms = new List<IClassroom>(subspaces.classrooms);
                //pick its nearest class from the remaining egress classroom list 
                IClassroom nearestClassroom = FindNearestClassroom(stairs, remainingClassrooms);
                if (nearestClassroom != null)
                {
                    success = true;
                    List<IStudentAI> studentsToEgress = new List<IStudentAI>();
                    //release control to the school
                    studentsToEgress = nearestClassroom.ReleaseClass();
                    //send the class to the egress point
                    foreach (IStudentAI student in new List<IStudentAI>())
                    {
                        Debug.Log($"Navigating students to egress point");
                        student.NavigateTo(stairs.gameObject.transform.position);
                    }
                }
            }
            return success;
        }

        public IClassroom FindNearestClassroom (ISpace space, List<IClassroom> classrooms)
        {
            float dist = 100000f;
            IClassroom selectedClass = null;
            Vector3 spacePos = space.GetGameObject().transform.position;
            foreach (IClassroom classroom in classrooms)
            {
                Debug.Log($"Distance is {Vector3.Distance(classroom.GetGameObject().transform.position,spacePos)}");
                if (Vector3.Distance(classroom.GetGameObject().transform.position,
                                    spacePos) < dist)
                {
                    selectedClass = classroom;
                    dist = Vector3.Distance(selectedClass.GetGameObject().transform.position,
                                            spacePos);
                }
            }
            return selectedClass;
        }

    }
}