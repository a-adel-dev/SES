using UnityEngine;
using UnityEngine.AI;
using SES.AIControl.FSM;
using SES.Core;

namespace SES.AIControl
{
    public class StudentBehaviorControl : MonoBehaviour, IStudentAI
    {

        public StudentBaseState currentState { get; set; }
        public string currentStateName { get; set; }
        public Vector3 originalPosition { get; set; }
        public Spot currentDesk { get; set; }
        public IClassroom CurrentClassroom { get; set; }
        public ILab CurrentLab { get; set; }
        public NavMeshAgent nav { get; set; }
        public IBathroom bathroomToVisit { get; set; }
        public ISchool school { get; set; }
        public bool visitedPOI { get; set; } = false;
        public bool inCorridor { get; set; } = false;
        public bool nearPOI { get; set; } = false;
        public ISpace poi { get; set; }
        public Spot lockerToVisit { get; set; }

        public string currentLabText = "";
        public string currentClassText = "";

        #region FSM
        public readonly SStudentInClassroom inClassroom = new SStudentInClassroom();
        public readonly SStudentAutonomus autonomous = new SStudentAutonomus();
        public readonly SStudentInTransit inTransit = new SStudentInTransit();
        public readonly SStudentDoingActivity active = new SStudentDoingActivity();
        public readonly SStudentonBreak onBreak = new SStudentonBreak();
        public readonly SStudentIdle idle = new SStudentIdle();

        private void Awake()
        {
            nav = GetComponent<NavMeshAgent>();
        }
        void Update()
        {
            if (currentDesk == null)
            {
                Debug.Log($"{gameObject.name}: I don't have a desk!");
            }
            currentClassText = CurrentClassroom == null? "..." : CurrentClassroom.GetGameObject().name;
            currentLabText = CurrentLab == null ? "..." : CurrentLab.GetGameObject().name;

            if (currentState != null)
            {
                currentStateName = currentState.ToString();
                currentState.Update(this);
            }
        }

        void TransitionToState(StudentBaseState state)
        {
            currentState = state;
            currentState.EnterState(this);
        }

        public void StartClass()
        {
            TransitionToState(inClassroom);
        }

        public void IdleAgent()
        {
            TransitionToState(idle);
        }
        public void BeAutonomus()
        {
            TransitionToState(autonomous);
        }

        public void StartActivity()
        {
            TransitionToState(active);
        }

        public void BreakTime()
        {
            TransitionToState(onBreak);
        }

        public void TransitStudent()
        {
            TransitionToState(inTransit);
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

        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Corridor"))
            {
                inCorridor = true;
            }
            if (other.CompareTag("POI"))
            {
                nearPOI = true;
                poi = other.GetComponent<ISpace>();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Corridor"))
            {
                inCorridor = false;
            }
            if (other.CompareTag("POI"))
            {
                nearPOI = false;
                poi = null;
                visitedPOI = false;
            }
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

        public void LookAtBoard()
        {
            Vector3 boardModifiedDirection = new Vector3(CurrentClassroom.classroomSubSpaces.Board.position.x,
                                                            0,
                                                            CurrentClassroom.classroomSubSpaces.Board.position.z);
            transform.LookAt(boardModifiedDirection);
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public void GoToAnotherLevel(Vector3 location)
        {
            nav.SetDestination(location);
        }

        public void NavigateTo(Vector3 location)
        {
            Vector3 modifiedLocation = new Vector3(location.x, 0, location.z);
            nav.SetDestination(modifiedLocation);
        }


        public void SetSpawnLocation()
        {
            originalPosition = transform.localPosition;
        }

        public void ResetDay()
        {
            nav.enabled = false;
            transform.localPosition = originalPosition;
            nav.enabled = true;
            nav.SetDestination(originalPosition);
            IdleAgent();
            CurrentClassroom.ReceiveStudent(this);
        }

        public void SetStoppingDistance(float distance)
        {
            nav.stoppingDistance = distance;
        }

        public bool IsFree()
        {
            return currentState.GetType() == typeof(SStudentInClassroom);
        }

        public void ClearLocker()
        {
            if (lockerToVisit != null)
            {
                if (CurrentClassroom != null)
                {
                    CurrentClassroom.classroomSubSpaces.ReturnLocker(lockerToVisit);
                }
                else if (CurrentLab != null)
                {
                    CurrentLab.SubSpaces.ReturnLocker(lockerToVisit);
                }
            }
            lockerToVisit = null;
        }
    }
}