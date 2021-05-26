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
        public ClassroomTeacherBucket teacher;

        public ITeacherAI classTeacher
        {
            get
            {
                return teacher.teacher;
            }

            set
            {
                teacher.teacher = value;
            }
        }

        private void Start()
        {
            teacher = GetComponent<ClassroomTeacherBucket>();
        }
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
            if (studentsBucket == null)
            {
                studentsBucket = GetComponent<SpaceStudentsBucket>();
            }
            TransitionToState(new SClassroomOnBreak());
        }

        public void ReleaseTeacher()
        {
            if (classTeacher == null)
            {
                return;
            }
            ///cleared teacher
            classTeacher.ClearCurrentClassroom();
            classTeacher.GoToTeacherroom();
            Debug.Log($"Sending {classTeacher.GetGameObject().name} to his teacher room");
            classTeacher = null;
        }

        public void RequestTeacher()
        {
            if (classTeacher != null) { return; }
            while (classTeacher == null)
            {
                int randomIndex = Random.Range(0, TotalAgentsBucket.GetTeachers().Count);
                if (TotalAgentsBucket.GetTeachers()[randomIndex].currentLab == null &&
                    TotalAgentsBucket.GetTeachers()[randomIndex].currentClass == null)
                {
                    classTeacher = TotalAgentsBucket.GetTeachers()[randomIndex];
                    classTeacher.currentClass = GetComponent<ClassroomSpace>();
                    classTeacher.GoToClassroom();
                }
            }
        }

        public void EmptyClass()
        {
            if (studentsBucket == null)
            {
                studentsBucket = GetComponent<SpaceStudentsBucket>();
            }
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

        public void RequestStatus(IStudentAI student)
        {
            if (currentState.GetType() == typeof(SClassroomInSession))
            {
                student.StartClass();
            }

            else if (currentState.GetType() == typeof(SClassroomOnBreak))
            {
                student.BreakTime();
            }

            else if (currentState.GetType() == typeof(SClassActivity))
            {
                student.StartClass();
            }
        }


    }
}
