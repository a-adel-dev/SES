using System.Collections;
using UnityEngine;
using SES.AIControl.FSM;
using SES.Core;
using UnityEngine.AI;
using SES.Spaces;
using SES.Spaces.Classroom;


namespace SES.AIControl
{
    public class StudentBehaviorControl : MonoBehaviour, IStudentAI
    {
        //public GameObject target;
        public StudentBaseState currentState;
        public string currentStateName;
        public Spot currentDesk;
        public Spot mainDesk;
        public Vector3 originalPosition;
        public ClassroomSpace mainClassroom;
        public int baseAutonomyChance;
        public int breakAutonomyChance;
        public float timeStep;
        NavMeshAgent nav;

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
            //NavigateTo(target.transform.position);
            if (currentState != null)
            {
                currentStateName = currentState.ToString();
                currentState.Update(this);
            }
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

        public void InitializeProperties()
        {
            baseAutonomyChance = SimulationDefaults.baseAutonomyChance;
            breakAutonomyChance = SimulationDefaults.breakAutonomyChance;
            timeStep = SimulationParameters.timeStep;
        }

        public void StartClass()
        {
            TransitionToState(inClassroom);
        }

        public void IdleStudent()
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

        public void AssignMainClassroom(ClassroomSpace classroom)
        {
            mainClassroom = classroom;
        }

        public void LookAtBoard()
        {
            Vector3 boardModifiedDirection = new Vector3(mainClassroom.classroomSubSpaces.board.transform.position.x,
                                                            0,
                                                            mainClassroom.classroomSubSpaces.board.transform.position.z);
            transform.LookAt(boardModifiedDirection);
        }

        public Spot RequestDesk(ISpace space) => currentDesk = space.RequestDesk(this);

        public void ReleaseDesk()
        {
            currentDesk.ClearSpot();
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public bool IsTeacher()
        {
            return GetComponent<TeacherAI>();
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

        public void Idle()
        {
            TransitionToState(idle);
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
            IdleStudent();
        }

        public void AssignOriginalPosition()
        {

            originalPosition = transform.localPosition;
        }

        public void AssignDesk(Spot desk)
        {
            currentDesk = desk;
        }
    }
}