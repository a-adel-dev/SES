using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SES.Core;

namespace SES.Spaces.Classroom
{
    public class ClassroomPeriodSchedular : MonoBehaviour
    {
        #region FSM
        private SClassroomBaseState currentState;
        protected void TransitionToState(SClassroomBaseState state)
        {
            currentState = state;
            state.EnterState(this);
        }
        #endregion

        private void Start()
        {
            PauseClass();
        }

        private void Update()
        {
            currentState.Update(this);
        }

        public void StartClass()
        {
            TransitionToState(new SClassroomInSession());
        }

        public void EndClass()
        {
            TransitionToState(new SClassroomOnBreak());
        }

        public void EmptyClass()
        {
            TransitionToState(new SClassroomEmpty());
        }

        public void PauseClass()
        {
            TransitionToState(new SClassroomIdle());
        }

    }
}
