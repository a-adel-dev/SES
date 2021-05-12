using System.Collections;
using UnityEngine;
using SES.Core;
using System;

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

        public void EgressClasses()
        {

        }

    }
}