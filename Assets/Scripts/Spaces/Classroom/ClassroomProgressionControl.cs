using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SES.Core;

namespace SES.Spaces.Classroom
{
    public class ClassroomProgressionControl : MonoBehaviour
    {
        public SpaceStudentsBucket studentsBucket { get; set; }
        [SerializeField] string currentStateName;
        public ClassTeacherBucket teacherBucket { get; set; }
        public SpotBucket Subspaces { get; set; }

        private void Start()
        {
            teacherBucket = GetComponent<ClassTeacherBucket>();
            Subspaces = GetComponent<SpotBucket>();
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

        public void DoActivity(int activityPeriod, SClassroomInSession perviousState)
        {
            pausedState = currentState;
            SClassActivity activity = new SClassActivity();
            activity.originalState = perviousState;
            activity.activityPeriod = activityPeriod;
            TransitionToState(activity);
        }

        public void ReleaseTeacher()
        {
            if (teacherBucket.teacher == null)
            {
                return;
            }
            ///cleared teacher
            teacherBucket.teacher.ClearCurrentClassroom();
            TotalAgentsBucket.AddToAvailableTeachers(teacherBucket.teacher);
            teacherBucket.teacher.GoToTeacherroom();
            //Debug.Log($"Sending {teacherBucket.teacher.GetGameObject().name} to his teacher room");
            teacherBucket.teacher = null;
        }

        public void RequestTeacher()
        {
            if (teacherBucket.teacher != null) { return; }
            ITeacherAI teach = TotalAgentsBucket.GetAvailableTeacher();
            teacherBucket.teacher = teach;
            if (teacherBucket.teacher == null)
            {
                Debug.LogError($"Could not find a teacher, {gameObject.name}");
            }
            //Debug.Log($"found teacher: {teach.GetGameObject().name}");
        }
    }
}
