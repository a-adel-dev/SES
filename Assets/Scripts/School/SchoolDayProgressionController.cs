using System.Collections;
using UnityEngine;
using SES.Core;
using System;

namespace SES.School
{
    public class SchoolDayProgressionController : MonoBehaviour
    {
        public short periodLength { get; set; } = 45;
        public short breakLength { get; set; } = 5;
        public short numPeriods { get; set; } = 2;
        public short simLength { get; set; } = 2;
        public float timeStep { get; set; } = 0.5f;

        SchoolSubSpacesBucket subspaces;

        

        #region FSm
        private SSchoolBaseState currentState;

        public readonly SClassesInSession classesInSession = new SClassesInSession();
        public readonly SBreakTime breakTime = new SBreakTime();
        public readonly SEgressTime egressTime = new SEgressTime();
        public readonly SOffTime offTime = new SOffTime();
        public readonly SSimOver simOver = new SSimOver();

        public void TransitionToState(SSchoolBaseState state)
        {
            currentState = state;
            currentState.EnterState(this);
        }
        #endregion


        private void Start()
        {
            subspaces = GetComponent<SchoolSubSpacesBucket>();
            InitializeProperties();
            StartSchoolDay();
        }

        private void Update()
        {
            currentState.Update(this);
        }

        private void InitializeProperties()
        {
            periodLength = SimulationParameters.periodLength;
            breakLength = SimulationParameters.breakLength;
            numPeriods = SimulationParameters.numPeriods;
            simLength = SimulationParameters.simLength;
            timeStep = SimulationParameters.timeStep;
        }

        public void StartSchoolDay()
        {
            DateTimeRecorder.StartSchoolDate();
            TransitionToState(classesInSession);
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
                classroom.StartClass();
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