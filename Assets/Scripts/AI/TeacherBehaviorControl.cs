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
        public IAgentHealth AgentHealth { get; set; }

        public STeacherBaseState currentState { get; set; }
        public string currentStateName { get; set; }

        private void Awake()
        {
            AgentHealth = GetComponent<IAgentHealth>();
        }
        void Start()
        {
            nav = GetComponent<NavMeshAgent>();
            school = FindObjectOfType<SchoolDayProgressionController>();
            
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

        public void VisitToilet()
        {
            TransitionToState(new STeacherToilet());
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public void IdleAgent()
        {
            throw new System.NotImplementedException();
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

