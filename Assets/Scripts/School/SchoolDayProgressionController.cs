using System.Collections;
using UnityEngine;
using SES.Core;

namespace SES.School
{
    public class SchoolDayProgressionController : MonoBehaviour
    {
        short periodLength = 40;
        short numPeriods = 4;
        short simLength = 3;
        float timeStep = 0.5f;

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
            InitializeStates();
            StartSchoolDay();
        }

        private void InitializeStates()
        {
            classesInSession.timeStep = timeStep;
            breakTime.numPeriods = numPeriods;
            offTime.simLength = simLength;
        }

        public void StartSchoolDay()
        {
            TransitionToState(classesInSession);
        }

    }
}