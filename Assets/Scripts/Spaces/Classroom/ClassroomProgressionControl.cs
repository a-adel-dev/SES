using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SES.Core;

namespace SES.Spaces.Classroom
{
    public class ClassroomProgressionControl : MonoBehaviour
    {
        public bool activitiesEnabled;
        public SpaceStudentsBucket studentsBucket;
        public string currentStateName;
        #region FSM
        public SClassroomBaseState currentState { get; private set; }
        private SClassroomBaseState pausedState;
        public void TransitionToState(SClassroomBaseState state)
        {
            currentState = state;
            state.EnterState(this);
        }
        #endregion

        private void Update()
        {
            if (currentState != null)
            {
                currentStateName = currentState.ToString();
                currentState.Update(this);
            } 
        }

        public void StartClass()
        {
            if (studentsBucket == null)
            {
                studentsBucket = GetComponent<SpaceStudentsBucket>();
            }
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
                foreach (IStudentAI student in studentsBucket.studentsCurrentlyInSpace)
                {
                    student.ResumeAgent();
                }

                TransitionToState(pausedState);
                pausedState = null;
            }
        }

        public void SetActivitiesEnabledTo(bool state)
        {
            activitiesEnabled = state;
        }

        public void DoActivity(int activityPeriod, SClassroomInSession perviousState)
        {
            pausedState = currentState;
            SClassActivity activity = new SClassActivity();
            activity.originalState = perviousState;
            activity.activityPeriod = activityPeriod;
            TransitionToState(activity);
        }
    }
}
