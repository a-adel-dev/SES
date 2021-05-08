﻿//using UnityEngine;
//using UnityEngine.AI;
//using Panda;
//using SES.Core;
//using SES.Spaces;
//using SES.Spaces.Classroom;
//using SES.Health;
//using SES.School;

//namespace SES.AIControl
//{
//    public class AI : MonoBehaviour
//    {
//        //cached variables 
//        NavMeshAgent agent;
//        PandaBehaviour behaviorTree;

//        //properties
//        private bool controlled = false;
//        Vector3 originalPosition;
//        public ClassroomSpace currentClass { get; private set; }
//        ClassroomSpace mainClassroom;
//        Lab currentLab;
//        Vector3 labPosition = Vector3.zero;
//        Bathroom currentBathroom;
//        bool onDesk;
//        Spot currentSpot;
//        //Vector3 destination;
//        bool clearToGo = false;
//        bool wentToLocker = false;
//        [SerializeField]
//        float clearenceChance = 0.1f;
//        [SerializeField]
//        float clearenceChanceMultiplier = 3f;
//        bool increasedClearence = false;
//        Bathroom nearestBathroom;
//        bool doingBehavior = false;
//        bool nearPOI = false;
//        StudentLocation status;
//        AgentHealth health;
//        SchoolManager schoolManager;

//        [SerializeField] Material originalMaterial;
//        [SerializeField] Material busyMaterial;


//        void Start()
//        {
//            schoolManager = GetComponent<SchoolManager>();
//            agent = GetComponent<NavMeshAgent>();
//            gameObject.GetComponent<Renderer>().material = originalMaterial;
//            behaviorTree = GetComponent<PandaBehaviour>();
//            health = GetComponent<AgentHealth>();
//        }


//        void Update()
//        {
//            //agent.SetDestination(destination);
//            //SetDestination(distination); 
//            SetIdlePose();
//            //var remaining = (agent.destination - this.transform.position);
//            //Debug.DrawRay(this.transform.position, remaining, Color.red);

//        }


//        /*=============================================
//         * Properties Getters, setters
//         * ============================================
//         */
//        public void SetStudentStatusTo(StudentLocation _status)
//        {
//            status = _status;
//        }

//        public StudentLocation GetStudentStatus()
//        {
//            return status;
//        }

//        public void AssignLabPosition(Vector3 position)
//        {
//            labPosition = position;
//        }

//        [Task]
//        public bool IsDependent()
//        {
//            return controlled;
//        }
//        [Task]
//        bool GotSpot()
//        {
//            return (currentSpot != null);
//        }

//        public void SetControlledTo(bool status)
//        {
//            controlled = status;
//            behaviorTree.enabled = !status;
//            if (status)
//            {
//                gameObject.GetComponent<Renderer>().material = busyMaterial;
//                health.SetActivityType(ActivityType.Talking);
//            }
//            else
//            {
//                gameObject.GetComponent<Renderer>().material = originalMaterial;
//                health.SetActivityType(ActivityType.Breathing);
//            }

//        }

//        public void SetOriginalPosition(Vector3 position)
//        {
//            originalPosition = position;
//        }

//        public void SetMainClassroom(ClassroomSpace classroom)
//        {
//            mainClassroom = classroom;
//        }

//        public bool CheckClearence()
//        {
//            //Debug.Log("checked Clearence!");
//            if (Random.Range(0f, 1f) < clearenceChance)
//            {
//                return true;
//            }
//            else
//            {
//                return false;
//            }
//        }

//        public void RestrictPupil()
//        {
//            clearToGo = false;
//        }

//        public void IncreaseClearenceChance()
//        {
//            clearenceChance *= clearenceChanceMultiplier;
//            increasedClearence = true;
//        }

//        public void ResetClearenceChance()
//        {
//            if (!increasedClearence) { return; }
//            clearenceChance /= clearenceChanceMultiplier;
//        }

//        public void ResetPupil()
//        {
//            if (!doingBehavior)
//            {
//                clearToGo = CheckClearence();
//            }
//        }
//        /*
//        ===============================================
//                  Collision space detection
//        ================================================
//        */
//        private void OnTriggerEnter(Collider other)
//        {
//            if (other.CompareTag("Bathroom"))
//            {
//                currentBathroom = other.GetComponent<Bathroom>();
//            }
//        }

//        public void SetCurrentClass(ISpace classroom)
//        {
//            ClassroomSpace _class = classroom as ClassroomSpace;
//            currentClass = _class;
//        }

//        public void SetCurrentLab(ISpace lab)
//        {
//            Lab _lab = lab as Lab;
//            currentLab = _lab;
//        }

//        public void ClearCurrentClass()
//        {
//            currentClass = null;
//        }

//        public void ClearCurrentLab()
//        {
//            currentLab = null;
//            labPosition = new Vector3();
//        }

//        /*
//         * =====================================
//         *            Directions Controls
//         * ======================================
//        */
//        private void LookAtBoard()
//        {
//            if (onDesk && currentClass != null)
//            {
//                Vector3 boardDirection = currentClass.classroomSubSpaces.GetClassBoard().transform.position;
//                agent.updateRotation = false;
//                //should involve a slerp
//                transform.LookAt(new Vector3(boardDirection.x, 0, boardDirection.z));
//                agent.updateRotation = true;
//            }
//        }

//        public void setStoppingDistance(float dist)
//        {
//            agent.stoppingDistance = dist;
//        }
//        /// <summary>
//        /// Directs agent to a point, ignoring its hight information
//        /// </summary>
//        /// <param name="destination">The position of the target location</param>
//        public void GuideTo(Vector3 destination)
//        {
//            agent.SetDestination(new Vector3(destination.x, 0f, destination.z));
//        }
//        /// <summary>
//        /// Directs agent to a point
//        /// </summary>
//        /// <param name="destination">The position of the target location</param>
//        public void MoveTo(Vector3 destination)
//        {
//            agent.SetDestination(destination);
//        }

//        public void GoToLab()
//        {
//            GuideTo(labPosition);
//        }

//        /*
//         * ================================
//         *          Spot Management
//         * ================================
//         */
//        public void AssignSpot(Spot spot)
//        {
//            currentSpot = spot;
//        }

//        public Spot ReleaseSpot()
//        {
//            Spot releasedSpot = currentSpot;
//            currentSpot = null;
//            return releasedSpot;
//        }


//        public void EnterClass(ISpace currentOriginalClass)
//        {
//            ClassroomSpace classroom = currentOriginalClass as ClassroomSpace;
//            classroom.studentsBucket.AddToStudentsCurrentlyInSpace(this);
//            SetCurrentClass(classroom);
//        }

//        public void AssignLab(ISpace lab)
//        {
//            Lab _lab = lab as Lab; 
//            _lab.labStudents.AddToSpaceOriginalStudents(this);
//            SetCurrentLab(lab);
//        }

//        public void Enterlab(ISpace lab)
//        {
//            Lab _lab = lab as Lab;
//            _lab.labStudents.AddToStudentsCurrentlyInSpace(this);
//        }

//        public void SetNearPOI(bool status)
//        {
//            nearPOI = status;
//        }

//        /*=================================
//         * Continuous Methods
//         * ================================
//         */
//        private void SetIdlePose()
//        {
//            if (Vector3.Distance(transform.position, originalPosition) < .1f)
//            {
//                onDesk = true;
//                LookAtBoard();
//            }
//            else
//            {
//                onDesk = false;
//            }
//        }

//        /*===================================
//         * Behaviors
//         * ==================================
//         */
//        [Task]
//        void SetDoingBehavior(bool status)
//        {
//            doingBehavior = status;
//            Task.current.Succeed();
//        }
//        [Task]
//        public void BackToDesk()
//        {
//            if (status == StudentLocation.inClass)
//            {
//                GuideTo(originalPosition);
//                if (behaviorTree.enabled)
//                {
//                    Task.current.Succeed();
//                }
//            }
//            else if (status == StudentLocation.inLab)
//            {
//                GuideTo(labPosition);
//                Enterlab(currentLab);
//                if (behaviorTree.enabled)
//                {
//                    Task.current.Succeed();
//                }
//            }
//        }


//        [Task]
//        void ExitClass()
//        {
//            currentClass.studentsBucket.RemoveFromClass(this);
//            ClearCurrentClass();
//            Task.current.Succeed();
//        }

//        [Task]
//        void GetBathroom()
//        {
//            nearestBathroom = schoolManager.subspaces.GetNearestBathroom(this);
//            if (nearestBathroom == null)
//            {
//                Task.current.Fail();
//                return;
//            }
//            Task.current.Succeed();
//        }

//        [Task]
//        private void GoToBathroom()
//        {

//            GuideTo(nearestBathroom.transform.position);
//            Task.current.Succeed();
//        }

//        [Task]
//        void ConfirmReach()
//        {
//            if (Task.isInspected)
//            {
//                Task.current.debugInfo = string.Format("t = {0:0.00}", Time.time);
//            }

//            if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
//            {
//                Task.current.Succeed();
//            }
//        }
//        [Task]
//        void ConfirmBathroomReach()
//        {
//            if (Task.isInspected)
//            {
//                Task.current.debugInfo = string.Format("t = {0:0.00}", Time.time);
//            }

//            if (agent.remainingDistance <= 1 && !agent.pathPending)
//            {
//                Task.current.Succeed();
//            }
//        }

//        [Task]
//        void GetToilet()
//        {
//            if (currentBathroom == null) { return; }
//            currentSpot = currentBathroom.GetAToilet(this);
//            if (currentSpot == null)
//            {
//                Task.current.Fail();
//                return;
//            }
//            Task.current.Succeed();
//        }

//        [Task]
//        void GoToToilet()
//        {
//            GuideTo(currentSpot.transform.position);
//            Task.current.Succeed();
//        }

//        [Task]
//        void ExitBathroom()
//        {
//            currentBathroom.ReleaseToilet(currentSpot);
//            currentBathroom = null;
//            Task.current.Succeed();
//        }

//        [Task]
//        bool ClearToGo()
//        {
//            return clearToGo;
//        }

//        [Task]
//        void ClearVisitStatus()
//        {
//            wentToLocker = false;
//            clearToGo = false;
//            CheckClearence();
//            Task.current.Succeed();
//        }

//        [Task]
//        void ReassignOriginalClassroom()
//        {
//            currentClass = mainClassroom;
//            EnterClass(currentClass);
//            Task.current.Succeed();
//        }

//        [Task]
//        void GoToLocker()
//        {
//            if (status == StudentLocation.inClass)
//            {
//                currentSpot = currentClass.classroomSubSpaces.GetRandomLocker();
//                if (currentSpot == null)
//                {
//                    Task.current.Fail();
//                    return;
//                }
//                else
//                {
//                    wentToLocker = true;
//                    GuideTo(currentSpot.transform.position);
//                    Task.current.Succeed();
//                }
//            }
//            else
//            {
//                currentSpot = currentLab.labObjects.GetRandomLocker();
//                if (currentSpot == null)
//                {
//                    Task.current.Fail();
//                    return;
//                }
//                else
//                {
//                    wentToLocker = true;
//                    GuideTo(currentSpot.transform.position);
//                    Task.current.Succeed();
//                }
//            }
//        }

//        [Task]
//        void ReleaseLocker()
//        {
//            if (status == StudentLocation.inClass)
//            {
//                currentClass.classroomSubSpaces.ReturnLocker(currentSpot);
//                currentSpot = null;
//                Task.current.Succeed();
//            }
//            else
//            {
//                currentLab.labObjects.ReturnLocker(currentSpot);
//                currentSpot = null;
//                Task.current.Succeed();
//            }
//        }

//        [Task]
//        bool WentToLocker()
//        {
//            return wentToLocker;
//        }

//        [Task]
//        bool NearPOI()
//        {
//            return nearPOI;
//        }


//        [Task]
//        bool GoToPOI()
//        {
//            int randomIndex = Random.Range(0, 100);
//            if (randomIndex < 20)
//            {
//                //pick a POI
//                //assign it to currentspot
//                return true;
//            }
//            else
//            {
//                return false;
//            }

//        }

//        [Task]
//        void TurnOffPOI()
//        {
//            nearPOI = false;
//            Task.current.Succeed();
//        }

//        [Task]
//        void GuideToPOI()
//        {
//            GuideTo(currentSpot.transform.position);
//            Task.current.Succeed();
//        }

//        [Task]
//        void ConfirmDeskReach()
//        {
//            if (Task.isInspected)
//            {
//                Task.current.debugInfo = string.Format("t = {0:0.00}", Time.time);
//            }

//            if (Vector3.Distance(transform.position, originalPosition) <= agent.stoppingDistance && !agent.pathPending)
//            {
//                Task.current.Succeed();
//            }
//        }

//        public Transform GetTransform()
//        {
//            return transform;
//        }

//        public void GetLabPosition(ISpace lab)
//        {
//            Lab _lab = lab as Lab;
//            Spot labPosition = _lab.AssignLabPositionToStudent();
//            labPosition.FillSpot(this);
//        }

//        public GameObject GetGameObject()
//        {
//            return gameObject;
//        }

//        public bool IsInfected()
//        {
//            return health.healthCondition == HealthCondition.infected;
//        }

//        public bool IsTeacher()
//        {
//            return (GetComponent<TeacherAI>());
//        }

//        public bool ISStudent()
//        {
//            return GetComponent<AI>() != null;
//        }

//    }
//}