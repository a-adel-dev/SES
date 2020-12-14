using UnityEngine;
using UnityEngine.AI;
using Panda;

public class AI : MonoBehaviour
{
    
    //cached variables 
    NavMeshAgent agent;
    SchoolManager school;


    //properties
    private bool busy = false;
    Vector3 originalPosition;
    Classroom currentClass;
    Classroom mainClassroom;
    Bathroom currentBathroom;
    bool onDesk;
    Spot currentSpot;
    Vector3 destination;
    float minToiletTime = 4f;
    float maxToiletTime = 10f;
    bool wentToBathroom = false;

    //temp properties
    float remainingDistance;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        school = FindObjectOfType<SchoolManager>();
        
    }

    
    void Update()
    {
        agent.SetDestination(destination);
        //SetDestination(distination);
        SetIdlePose();
        remainingDistance = agent.remainingDistance;
        var remaining = (destination - this.transform.position);
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
        destination = originalPosition;
        Task.current.Succeed();
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
        
        this.destination = new Vector3 (destination.x, 0f , destination.z);
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
        if (!ClearToGo())
        {
            Task.current.Fail();
            return;
        }
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
            Task.current.debugInfo = string.Format("dest = {0}", agent.destination);
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
            Task.current.debugInfo = string.Format("dest = {0}", agent.destination);
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

    bool ClearToGo()
    {
        if (Random.Range(0f, 1f) <.1f )
        {
            return true;
        }
        else
        {
            return false;
        }
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
        Task.current.Succeed();
    }


}
