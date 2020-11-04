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
    private SubSpace currentSubSpace;
    
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
        GameObject closestBoard = FindNearestBoard();

        SubspaceManager closestBoardSpaceManager = closestBoard.GetComponent<SchoolSpace>().subSpaceManager;
        if (closestBoardSpaceManager.GetAvailableSubSpacesCount() == 0)
            Task.current.Fail();
        else if (closestBoard != null)
        {
            currentSubSpace = closestBoardSpaceManager.PopAvailableSubSpace(this); // add to his inventory
            if (currentSubSpace != null)
            {
                navMesh.SetDestination(currentSubSpace.space.transform.position);
                Task.current.Succeed();
            }
        }
    }

    [Task]
    void MoveToNearestBathroom()
    {
        GameObject closestBathroom = FindNearestBathroom();

        SubspaceManager closestBathroomSpaceManager = closestBathroom.GetComponent<SchoolSpace>().subSpaceManager;
        if (closestBathroomSpaceManager.GetAvailableSubSpacesCount() == 0)
            Task.current.Fail();
        else if (closestBathroom != null)
        {
            currentSubSpace = closestBathroomSpaceManager.PopAvailableSubSpace(this); // add to his inventory
            if (currentSubSpace != null)
            {
                navMesh.SetDestination(currentSubSpace.space.transform.position);
                Task.current.Succeed();
            }
        }
    }

    private GameObject FindNearestBoard()
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

        return closestBoard;
    }

    private GameObject FindNearestBathroom()
    {
        GameObject[] bathrooms;
        float nearest = Mathf.Infinity;
        GameObject closestBathroom = null;
        bathrooms = GameObject.FindGameObjectsWithTag("Bathroom");
        foreach (GameObject bathroom in bathrooms)
        {
            if (Vector3.Distance(transform.position, bathroom.transform.position) < nearest)
            {
                nearest = Vector3.Distance(this.transform.position, bathroom.transform.position);
                closestBathroom = bathroom;
            }
        }

        return closestBathroom;
    }

    [Task]
    void ReleaseSubLocation()
    {
        currentSubSpace.space.getParentSpace().subSpaceManager.ReleaseSpace(this);
        currentSubSpace = null;
        Task.current.Succeed();
    }
}
