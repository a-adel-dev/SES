using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SES.Core;
using UnityEngine.AI;
using SES.AIControl.FSM;
using SES.Spaces;
using System;

namespace SES.AIControl
{

    public class TeacherBehaviorControl : MonoBehaviour, ITeacherAI
    {
        public IClassroom currentClass { get; set; }
        public Spot currentDesk { get; set; }
        public NavMeshAgent nav { get; set; }
        public ISpace teacherroom { get; set; }

        public ILab currentLab { get; set; }

        public bool inCorridor { get; set; } = false;
        public bool nearPOI { get; set; } = false;
        public POI poi { get; set; }
        public bool visitedPOI { get; set; } = false;

        public STeacherBaseState currentState { get; set; }
        public string currentStateName { get; set; }
        //AgentHealth health;
        //public TeacherMovementStyle movementStyle = TeacherMovementStyle.classroom;

        //// Start is called before the first frame update
        void Start()
        {
            nav = GetComponent<NavMeshAgent>();
            //health = GetComponent<AgentHealth>();
            //if (IsInClassroom())
            //{
            //    StartWander(TeacherMovementStyle.restricted);
            //    health.SetActivityType(ActivityType.LoudTalking);
            //}
        }

        void Update()
        {
            if (currentState != null)
            {
                currentStateName = currentState.ToString();
                currentState.Update(this);
            }
        }

        void TransitionToState(STeacherBaseState state)
        {
            currentState = state;
            currentState.EnterState(this);
        }

        public void ClassroomRestricted()
        {
            TransitionToState(new STeacherInClassRestricted());
        }

        public void ClassroomFree()
        {
            TransitionToState(new STeacherInClassFree());
        }

        public void StopForPOI()
        {
            TransitionToState(new STeacherPOI());
        }

        public void Rest()
        {
            TransitionToState(new STeacherResting());
        }

        internal void BehaviorGoToBathroom()
        {
            throw new NotImplementedException();
        }

        internal void BehaviorGoToLocker()
        {
            TransitionToState(new STeacherLocker());
        }

        public void GoToTeacherroom()
        {
            TransitionToState(new STeacherGoingToTeacherroom());
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Corridor>())
            {
                inCorridor = true;
            }
            if (other.GetComponent<POI>())
            {
                nearPOI = true;
                poi = other.GetComponent<POI>();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<Corridor>())
            {
                inCorridor = false;
            }
            if (other.GetComponent<POI>())
            {
                nearPOI = false;
                poi = null;
            }
        }


        public void SetCurrentClassroom(IClassroom classroom)
        {
            currentClass = classroom;
        }

        ///// <summary>
        ///// Sets the teacher to be in a classroom or out based on the status parameter
        ///// </summary>
        ///// <param name="status">True if teacher is in a class, false if not</param>
        //public void SetInClassroomto(bool status)
        //{
        //    inClassroom = status;
        //    teacherNav.ClearTeacherDesk();
        //}
        ///// <summary>
        ///// returns true if teacher is currently in class
        ///// </summary>
        //public bool IsInClassroom()
        //{
        //    return inClassroom;
        //}


        ///// <summary>
        ///// moves the teacher about in a classroom
        ///// </summary>
        ///// <param name="style">classroom if the teacher is free to move in the class, restricted if the teacher is bound to his area</param>
        //private void StartWander(TeacherMovementStyle style)
        //{
        //    teacherNav.SetWandering(true);
        //    if (style == TeacherMovementStyle.restricted)
        //    {
        //        StartCoroutine(teacherNav.Wander());
        //        movementStyle = TeacherMovementStyle.restricted;
        //    }
        //    else if (style == TeacherMovementStyle.classroom)
        //    {
        //        StartCoroutine(teacherNav.Wander());
        //        movementStyle = TeacherMovementStyle.classroom;
        //    }
        //}

        //private void StopWandering()
        //{
        //    teacherNav.SetWandering(false);
        //}

        public void AssignDesk(Spot spot)
        {
            throw new System.NotImplementedException();
        }

        public void AssignOriginalPosition()
        {
            throw new System.NotImplementedException();
        }

        public void AssignSpot(Spot spot)
        {
            throw new System.NotImplementedException();
        }

        public void ClearSpot()
        {
            throw new System.NotImplementedException();
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public void IdleAgent()
        {
            throw new System.NotImplementedException();
        }

        public bool IsStudent()
        {
            return false;
        }

        public bool IsTeacher()
        {
            return true;
        }

        public void NavigateTo(Vector3 location)
        {
            if (nav == null)
            {
                nav = GetComponent<NavMeshAgent>();
                nav.SetDestination(location);
            }
            else
            {
                nav.SetDestination(location);
            }
        }

        public void PauseAgent()
        {
            nav.isStopped = true;
        }

        public void ResumeAgent()
        {
            nav.isStopped = false;
        }
    }


}

