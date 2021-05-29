using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SES.Core;
using UnityEngine.AI;
using SES.AIControl.FSM;
using SES.Spaces;
using System;
using SES.School;

namespace SES.AIControl
{
    public class TeacherBehaviorControl : MonoBehaviour, ITeacherAI
    {
        public IClassroom currentClass { get; set; }
        public Spot currentDesk { get; set; }
        public NavMeshAgent nav { get; set; }
        public ITeachersroom teacherroom { get; set; }

        public ILab currentLab { get; set; }

        public bool inCorridor { get; set; } = false;
        public bool nearPOI { get; set; } = false;
        public POI poi { get; set; }
        public bool visitedPOI { get; set; } = false;

        public IBathroom bathroomToVisit { get; set; }
        public ISchool school { get; set; }

        public STeacherBaseState currentState { get; set; }
        public string currentStateName { get; set; }
        //AgentHealth health;
        //public TeacherMovementStyle movementStyle = TeacherMovementStyle.classroom;

        //// Start is called before the first frame update
        void Start()
        {
            nav = GetComponent<NavMeshAgent>();
            school = FindObjectOfType<SchoolDayProgressionController>();
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
            TransitionToState(new STeacherBathroom());
        }

        internal void BehaviorGoToLocker()
        {
            TransitionToState(new STeacherLocker());
        }

        public void GoToTeacherroom()
        {
            TransitionToState(new STeacherGoingToTeacherroom());
        }

        public void GoToClassroom()
        {
            teacherroom.ExitTeacherroom(this);
            TransitionToState(new STeacherGoingToClassroom());
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
                visitedPOI = false;
            }
        }


        public void SetCurrentClassroom(IClassroom classroom)
        {
            currentClass = classroom;
        }

        public void ClearCurrentClassroom()
        {
            currentClass = null;
        }

        public void ExitTeacherroom()
        {
            currentDesk.ClearSpot();
            currentDesk = null;
        }

        public void VisitToilet()
        {
            TransitionToState(new STeacherToilet());
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

        public bool IsInTeacherroom()
        {
            if (teacherroom != null)
            {
                return teacherroom.teachers.Contains(this);
            }
            else
            {
                return false;
            }
            
        }

        public void GoToAnotherLevel(Vector3 location)
        {
            throw new NotImplementedException();
        }
    }


}

