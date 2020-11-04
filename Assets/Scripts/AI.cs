using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Panda;

public class AI : MonoBehaviour
{
    public Vector3 originalPosition;
    private NavMeshAgent navMesh;
    public Vector3 destination;
    private SchoolSubSpace currentSubSpace;
    
    // Start is called before the first frame update
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        GetOriginalLocation();
    }

    // Update is called once per frame
    void Update()
    {
        navMesh.destination = destination;
    }

 
    public void GetOriginalLocation()
    {
        originalPosition = transform.position;
    }

    [Task]
    void PickDestination()
    {
        navMesh.SetDestination(new Vector3(-4,0,10));
        Task.current.Succeed();
    }

    [Task]
    void ConfirmReach()
    {
        if (Task.isInspected)
        {
            Task.current.debugInfo = string.Format("dest = {0}", navMesh.destination);
        }
            
        if (navMesh.remainingDistance <= navMesh.stoppingDistance && !navMesh.pathPending)
        {
            Task.current.Succeed();
        }

    }
    [Task]
    void MoveBackToOriginalPosition()
    {
        navMesh.SetDestination(originalPosition);
        Task.current.Succeed();
    }

    [Task]
    void MoveToNearestBoard()
    {
        
        GameObject[] boards;
        float nearest = Mathf.Infinity;
        GameObject closestBoard = null;
        boards = GameObject.FindGameObjectsWithTag("Board");
        foreach (GameObject board in boards)
        {
            if (Vector3.Distance(transform.position, board.transform.position) < nearest)
            {
                nearest = Vector3.Distance(this.transform.position, board.transform.position);
                closestBoard = board;
            }
        }
        
        SchoolSpace closestBoardSpaces = closestBoard.GetComponent<SchoolSpace>();
        if (!closestBoardSpaces.isSubSpaceAvailable())
            Task.current.Fail();
        else if (closestBoard != null)
        {
            currentSubSpace = closestBoardSpaces.AssignSpaceToAgent(this);
            Debug.Log("Current: " + currentSubSpace);
            navMesh.SetDestination(currentSubSpace.transform.position);
            Task.current.Succeed();
        }
        
        
    }

    [Task]
    void ReleaseSubLocation()
    {
        currentSubSpace.getParentSpace().ReleaseSpace(this);
        currentSubSpace = null;
        Task.current.Succeed();
    }
}
