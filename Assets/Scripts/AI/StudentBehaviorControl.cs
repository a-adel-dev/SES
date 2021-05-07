using System.Collections;
using UnityEngine;
using SES.AIControl.FSM;
using SES.Core;

namespace SES.AIControl
{
    public class StudentBehaviorControl : MonoBehaviour
    {
        StudentState state { get; set; }

        private StudentBaseState currentState;

        public readonly StateInClassroom inClassroom = new StateInClassroom();
        public readonly StateInLab inLab = new StateInLab();
        public readonly StateInTransit inTransit = new StateInTransit();
        public readonly StateActive active = new StateActive();
        public readonly StateOnBreak onBreak = new StateOnBreak();

        void Update()
        {
            currentState.Update(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            currentState.OnTriggerEnter(this);
        }

        void TransitionToState(StudentBaseState state)
        {
            currentState = state;
            currentState.EnterState(this);
        }

        public void SetStudentState(StudentState _state)
        {
            state = _state;
            switch(state)
            {
                case StudentState.inClass:
                    TransitionToState(inClassroom);
                    break;
                case StudentState.inLab:
                    TransitionToState(inLab);
                    break;
                case StudentState.inTransit:
                    TransitionToState(inTransit);
                    break;
                case StudentState.onBreak:
                    TransitionToState(onBreak);
                    break;
                case StudentState.active:
                    TransitionToState(active);
                    break;
            }
        }

    }
}