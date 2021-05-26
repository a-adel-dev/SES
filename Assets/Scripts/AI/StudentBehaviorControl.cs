using UnityEngine;
using SES.AIControl.FSM;
using SES.Core;
using UnityEngine.AI;
using SES.Spaces.Classroom;
using SES.School;
using SES.Spaces;
using System;

namespace SES.AIControl
{
    public class StudentBehaviorControl : MonoBehaviour, IStudentAI
    {
        public StudentBaseState currentState { get; set; }
        public string currentStateName { get; set; }
        public Spot currentDesk { get; set; }
        public Spot currentSpot { get; set; }
        public Vector3 originalPosition { get; set; }
        public ClassroomSpace currentClassroom { get; set; }
        public ILab currentLab { get; set; }
        public int baseAutonomyChance { get; set; }
        public int breakAutonomyChance { get; set; }
        public bool visitedPOI { get; set; } = false;

        public NavMeshAgent nav { get; set; }
        public IBathroom bathroomToVisit { get; set; }
        public ISchool school { get; set; }
        public bool inCorridor { get; set; } = false;
        public bool nearPOI { get; set; } = false;
        public POI poi { get; set; }

        public readonly SStudentInClassroom inClassroom = new SStudentInClassroom();
        public readonly SStudentAutonomus autonomous = new SStudentAutonomus();
        public readonly SStudentInTransit inTransit = new SStudentInTransit();
        public readonly SStudentDoingActivity active = new SStudentDoingActivity();
        public readonly SStudentonBreak onBreak = new SStudentonBreak();
        public readonly SStudentIdle idle = new SStudentIdle();

        private void Awake()
        {
            nav = GetComponent<NavMeshAgent>();
            school = FindObjectOfType<SchoolScheduler>();
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

        void TransitionToState(StudentBaseState state)
        {
            currentState = state;
            currentState.EnterState(this);
            currentStateName = currentState.ToString();
        }

        public void InitializeProperties()
        {
            baseAutonomyChance = SimulationDefaults.baseAutonomyChance;
            breakAutonomyChance = SimulationDefaults.breakAutonomyChance;
        }

        public void StartClass()
        {
            TransitionToState(inClassroom);
        }

        public void IdleAgent()
        {
            TransitionToState(idle);
        }

        public void PauseAgent()
        {
            GetComponent<NavMeshAgent>().isStopped = true;
        }

        public void ResumeAgent()
        {
            GetComponent<NavMeshAgent>().isStopped = false;
        }

        public void BackToDesk()
        {
            NavigateTo(currentDesk.gameObject.transform.position);
        }

        public void AssignCurrentClassroom(IClassroom classroom)
        {
            currentClassroom = classroom as ClassroomSpace;
        }

        public void AssignLab(ILab lab)
        {
            currentLab = lab;
        }

        public void LookAtBoard()
        {
            Vector3 boardModifiedDirection = new Vector3(currentClassroom.classroomSubSpaces.board.transform.position.x,
                                                            0,
                                                            currentClassroom.classroomSubSpaces.board.transform.position.z);
            transform.LookAt(boardModifiedDirection);
        }

        public Spot RequestDesk(ISpace space) => currentDesk = space.RequestDesk(this);

        

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public bool IsTeacher()
        {
            return GetComponent<TeacherBehaviorControl>();
        }

        public bool IsStudent()
        {
            return GetComponent<StudentBehaviorControl>();
        }

        public void ReleaseControl()
        {
            TransitionToState(autonomous);
        }

        public void StartActivity()
        {
            TransitionToState(active);//i.e prevent autonomus actions
        }

        public void NavigateTo(Vector3 location)
        {
            nav.SetDestination(location);
        }

        public void BreakTime()
        {
            TransitionToState(onBreak);
        }

        public void TransitStudent()
        {
            TransitionToState(inTransit);
        }

        public void ResetDay()
        {
            nav.enabled = false;
            transform.localPosition = originalPosition;
            nav.enabled = true;
            nav.SetDestination(originalPosition);
            IdleAgent();
        }

        public void AssignOriginalPosition()
        {
            originalPosition = transform.localPosition;
        }

        public void AssignDesk(Spot desk)
        {
            currentDesk = desk;
        }
        public void ReleaseDesk()
        {
            currentDesk.ClearSpot();
        }

        public void AssignSpot(Spot spot)
        {
            currentSpot = spot;
            spot.FillSpot(this);
        }
        public void ClearSpot()
        {
            currentSpot.ClearSpot();
        }

        public void SetStoppingDistance(float distance)
        {
            nav.stoppingDistance = distance;
        }

        public void BehaviorGoToLocker()
        {
            TransitionToState(new SStudentLockerBehavior());
        }

        public void BehaviorGoToBathroom()
        {
            TransitionToState(new SStudentBathroomBehavior());
        }

        public void VisitToilet()
        {
            TransitionToState(new SStudentToiletBehavior());
        }

        public void GoToClassroom()
        {
            TransitionToState(new SStudentBackToClassBehavior());
        }

        internal void StopForPOI()
        {
            TransitionToState(new SStudentNearPOIBehavior());
        }

        public void ClearCurrentClass()
        {
            currentClassroom = null;
        }

        public void ClearCurrentLab()
        {
            currentLab = null;
        }
    }
}