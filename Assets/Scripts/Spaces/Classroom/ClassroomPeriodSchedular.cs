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
        private SClassroomBaseState pausedState;
        protected void TransitionToState(SClassroomBaseState state)
        {
            currentState = state;
            state.EnterState(this);
        }
        #endregion

        private void Update()
        {
            if (currentState != null)
            {
                currentState.Update(this);
            } 
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
            currentState.resumed = true;
            pausedState = currentState;
            TransitionToState(new SClassroomIdle());
        }

        public void ResumeClass()
        {
            if (pausedState != null)
            {
                TransitionToState(pausedState);
                pausedState = null;
            }
        }

    }
}
