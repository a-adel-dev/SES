using UnityEngine;
using UnityEngine.AI;
using Panda;

public class AI : MonoBehaviour
{
    //cached variables 
    NavMeshAgent agent;
    SchoolManager school;
    PandaBehaviour behaviorTree;


    //properties
    private bool busy = false;
    Vector3 originalPosition;
    Classroom currentClass;
    Classroom mainClassroom;
    Bathroom currentBathroom;
    bool onDesk;
    Spot currentSpot;
    //Vector3 destination;
    bool clearToGo = false;
    bool wentToLocker = false;
    [SerializeField]
    float clearenceChance = 0.1f;
    [SerializeField]
    float clearenceChanceMultiplier = 3f;
    bool increasedClearence = false;
    Bathroom nearestBathroom;
    bool doingBehavior = false;
    bool nearPOI = false;

    //temp properties
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        school = FindObjectOfType<SchoolManager>();
    }

    
    void Update()
    {
        //agent.SetDestination(destination);
        //SetDestination(distination);
        behaviorTree = GetComponent<PandaBehaviour>();
        SetIdlePose();
        var remaining = (agent.destination - this.transform.position);
        Debug.DrawRay(this.transform.position, remaining, Color.red);

    }

    
    /*=============================================
     * Properties Getters, setters
     * ============================================
     */
    [Task]
    public bool IsBusy()
    {
        return busy;
    }
    [Task]
    bool GotSpot()
    {
        return (currentSpot != null);
    }

    public void SetBusyTo(bool status)
    {
        busy = status;
        behaviorTree.enabled = !status;
    }

    public void SetOriginalPosition(Vector3 position)
    {
        originalPosition = position;
    }

    public void SetMainClassroom(Classroom classroom)
    {
        mainClassroom = classroom;
    }

    public void SetCurrentClassroom(Classroom classroom)
    {
        currentClass = classroom;
    }

    bool checkClearence()
    {
        //Debug.Log("checked Clearence!");
        if (Random.Range(0f, 1f) < clearenceChance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RestrictPupil()
    {
        clearToGo = false;
    }

    public void IncreaseClearenceChance()
    {
        clearenceChance *= clearenceChanceMultiplier;
        increasedClearence = true;
    }

    public void ResetClearenceChance()
    {
        if (!increasedClearence) { return; }
        clearenceChance /= clearenceChanceMultiplier;
    }

    public void ResetPupil()
    {
        if (!doingBehavior)
        {
            clearToGo = checkClearence();
        }
    }
    /*
    ===============================================
              Collision space detection
    ================================================
    */
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bathroom"))
        {
            currentBathroom = other.GetComponent<Bathroom>();
        }
    }

    public void SetCurrentClass(Classroom classroom)
    {
        currentClass = classroom;
    }

    public void ClearCurrentClass()
    {
        currentClass = null;
    }

    /*
     * =====================================
     *            Directions Controls
     * ======================================
    */
    private void LookAtBoard()
    {
        if(onDesk && currentClass != null)
        {
            Vector3 boardDirection = currentClass.board.gameObject.transform.position;
            agent.updateRotation = false;
            //should involve a slerp
            transform.LookAt(new Vector3 (boardDirection.x, 0, boardDirection.z));
            agent.updateRotation = true;
        }
    }

    public void setStoppingDistance(float dist)
    {
        agent.stoppingDistance = dist;
    }

    public void GuideTo(Vector3 destination)
    {
        
        agent.SetDestination( new Vector3 (destination.x, 0f , destination.z));
    }

    /*
     * ================================
     *          Spot Management
     * ================================
     */
    public void AssignSpot(Spot spot)
    {
        currentSpot = spot;
    }

    public Spot ReleaseSpot()
    {
        Spot releasedSpot = currentSpot;
        currentSpot = null;
        return releasedSpot;
    }


    public void EnterClass(Classroom classroom)
    {
        classroom.AddToPupilsInClass(this);
        SetCurrentClass(classroom);
        //pupil.SetBusyTo(true);
    }

    public void SetNearPOI(bool status)
    {
        nearPOI = status;
    }

    /*=================================
     * Continuous Methods
     * ================================
     */
    private void SetIdlePose()
    {
        if (Vector3.Distance(transform.position, originalPosition) < .1f)
        {
            onDesk = true;
            LookAtBoard();
        }
        else
        {
            onDesk = false;
        }
    }

    /*===================================
     * Behaviors
     * ==================================
     */
    [Task]
    void SetDoingBehavior(bool status)
    {
        doingBehavior = status;
        Task.current.Succeed();
    }
    [Task]
    public void BackToDesk()
    {
        GuideTo(originalPosition);
        if (behaviorTree.enabled)
        {
            Task.current.Succeed();
        }
    }


    [Task]
    void ExitClass()
    {

        currentClass.RemoveFromClass(this);
        ClearCurrentClass();
        Task.current.Succeed();
    }

    [Task]
    void GetBathroom()
    {
        nearestBathroom = school.GetNearestBathroom(this);
        if (nearestBathroom == null)
        {
            Task.current.Fail();
            return;
        }
        Task.current.Succeed();
    }

    [Task]
    private void GoToBathroom()
    {

        GuideTo(nearestBathroom.transform.position);
        Task.current.Succeed();
    }

    [Task]
    void ConfirmReach()
    {
        if (Task.isInspected)
        {
            Task.current.debugInfo = string.Format("t = {0:0.00}", Time.time);
        }

        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            Task.current.Succeed();
        }
    }
    [Task]
    void ConfirmBathroomReach()
    {
        if (Task.isInspected)
        {
            Task.current.debugInfo = string.Format("t = {0:0.00}", Time.time);
        }

        if (agent.remainingDistance <= 1 && !agent.pathPending)
        {
            Task.current.Succeed();
        }
    }
    
    [Task]
    void GetToilet()
    {
        if (currentBathroom == null) { return; }
        currentSpot = currentBathroom.GetAToilet(this);
        if (currentSpot == null)
        {
            Task.current.Fail();
            return;
        }
        Task.current.Succeed();
    }

    [Task]
    void GoToToilet()
    {
        GuideTo(currentSpot.transform.position);
        Task.current.Succeed();
    }

    [Task]
    void ExitBathroom()
    {
        currentBathroom.ReleaseToilet(currentSpot);
        currentBathroom = null;
        Task.current.Succeed();
    }

    [Task]
    bool ClearToGo()
    {
        return clearToGo;
    }



    [Task]
    void ClearVisitStatus()
    {
        wentToLocker = false;
        clearToGo = false;
        checkClearence();
        Task.current.Succeed();
    }

    [Task]
    void ReassignOriginalClassroom()
    {
        currentClass = mainClassroom;
        EnterClass(currentClass);
        Task.current.Succeed(); 
    }

    [Task]
    void GoToLocker()
    {
        currentSpot = currentClass.GetLocker();
        if (currentSpot == null)
        {
            Task.current.Fail();
            return;
        }
        else
        {
            wentToLocker = true;
            GuideTo(currentSpot.transform.position);
            Task.current.Succeed();
        }
    }

    [Task]
    void ReleaseLocker()
    {
        currentClass.ReturnLocker(currentSpot);
        currentSpot = null;
        Task.current.Succeed();
    }

    [Task]
    bool WentToLocker()
    {
        return wentToLocker;
    }

    [Task]
    bool NearPOI()
    {
        return nearPOI;
    }


    [Task]
    bool GoToPOI()
    {
        int randomIndex = Random.Range(0, 100);
        if (randomIndex < 20)
        {
            //pick a POI
            //assign it to currentspot
            return true;
        }
        else
        {
            return false;
        }
        
    }

    [Task]
    void TurnOffPOI()
    {
        nearPOI = false;
        Task.current.Succeed();
    }

    [Task]
    void GuideToPOI()
    {
        GuideTo(currentSpot.transform.position);
        Task.current.Succeed();
    }

    [Task]
    void ConfirmDeskReach()
    {
        if (Task.isInspected)
        {
            Task.current.debugInfo = string.Format("t = {0:0.00}", Time.time);
        }

        if (Vector3.Distance(transform.position, originalPosition) <= agent.stoppingDistance && !agent.pathPending)
        {
            Task.current.Succeed();
        }
    }
}
