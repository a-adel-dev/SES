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
    float minToiletTime = 4f;
    float maxToiletTime = 10f;
    bool wentToBathroom = false;
    bool clearToGo = false;
    float clearenceChance = 0.1f;

    //temp properties
    float remainingDistance;
    
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
        remainingDistance = agent.remainingDistance;
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

    public void ResetPupil()
    {
        clearToGo = checkClearence();
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
    [Task]
    public void BackToDesk()
    {
        GuideTo(originalPosition);
        
        if (behaviorTree.enabled)
        {
            Task.current.Succeed();
        }
    }

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

    public void ExitClass(Classroom classroom)
    {
        classroom.RemoveFromClass(this);
        ClearCurrentClass();
    }

    public void EnterClass(Classroom classroom)
    {
        classroom.AddToPupilsInClass(this);
        SetCurrentClass(classroom);
        //pupil.SetBusyTo(true);
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
    private void GoToBathroom()
    {
        wentToBathroom = true;
        ExitClass(currentClass);
        Bathroom nearestBathroom = school.GetNearestBathroom(this);
        if (nearestBathroom == null)
        {
            Task.current.Fail();
            return;
        }
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
    void GoToToilet()
    {
        if (currentBathroom == null) { return; }
        Spot toilet = currentBathroom.GetAToilet(this);
        if (toilet == null) 
        { 
            Task.current.Fail();
            return;
        }
        currentSpot = toilet;
        GuideTo(toilet.transform.position);
        Task.current.Succeed();
    }

    [Task]
    void ExitBathroom()
    {
        currentBathroom.ReleaseToilet(currentSpot);
        Task.current.Succeed();
    }
    [Task]
    bool ClearToGo()
    {
        return clearToGo;
    }

    [Task]
    bool WentToBathroom()
    {
        return wentToBathroom;
    }

    [Task]
    void ClearStatus()
    {
        wentToBathroom = false;
        Task.current.Succeed();
    }

    [Task]
    void ResetClassRoom()
    {
        currentClass = mainClassroom;
        EnterClass(currentClass);
        Task.current.Succeed();
        clearToGo = false;
    }


}
