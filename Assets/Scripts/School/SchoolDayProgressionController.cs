using System.Collections;
using UnityEngine;
using SES.Core;
using System;

namespace SES.School
{
    public class SchoolDayProgressionController : MonoBehaviour
    {
        public DateTimeRecorder timeRecorder { get; set; }
        public short periodLength { get; set; } = 45;
        public short breakLength { get; set; } = 5;
        public short numPeriods { get; set; } = 2;
        public short simLength { get; set; } = 2;
        public float timeStep { get; set; } = 0.5f;

        #region FSm
        private SSchoolBaseState currentState;

        public readonly SClassesInSession classesInSession = new SClassesInSession();
        public readonly SBreakTime breakTime = new SBreakTime();
        public readonly SEgressTime egressTime = new SEgressTime();
        public readonly SOffTime offTime = new SOffTime();
        public readonly SSimOver simOver = new SSimOver();

        private void Update()
        {
            currentState.Update(this);
        }

        public void TransitionToState(SSchoolBaseState state)
        {
            currentState = state;
            currentState.EnterState(this);
        }
        #endregion


        private void Start()
        {
            InitializeProperties();
            timeRecorder = GetComponent<DateTimeRecorder>();
            StartSchoolDay();
        }

        private void InitializeProperties()
        {
            periodLength = SimulationVariables.periodLength;
            breakLength = SimulationVariables.breakLength;
            numPeriods = SimulationVariables.numPeriods;
            simLength = SimulationVariables.simLength;
            timeStep = SimulationVariables.timeStep;
        }

        public void StartSchoolDay()
        {
            TransitionToState(classesInSession);
        }

    }
}