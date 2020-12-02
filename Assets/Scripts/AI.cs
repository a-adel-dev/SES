using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    NavMeshAgent agent;
    private bool busy = false;
    Vector3 originalPosition;
    Classroom currentClass;
    bool idle = true;
    Spot currentSPot;
    Vector3 distination;
    
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        SetDestination(distination);
        SetIdlePose();
    }

    private void SetIdlePose()
    {
        if (Vector3.Distance(transform.position, originalPosition) < .1f)
        {
            idle = true;
            LookAtBoard();
        }
        
    }

    public void SetBusyStatusTO(bool status)
    {
        busy = status;
    }

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

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Classroom"))
        {
            other.GetComponent<Classroom>().GetPupilOutofClassroom(this);
            //untested
        }
    }

    public void BackToDesk()
    {
        distination = originalPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Classroom"))
        {
            currentClass = other.GetComponent<Classroom>();
        }
    }

    private void LookAtBoard()
    {
        if(idle)
        {
            Vector3 boardDirection = currentClass.board.gameObject.transform.position;
            agent.updateRotation = false;
            //should involve a slerp
            transform.LookAt(new Vector3 (boardDirection.x, 0, boardDirection.z));
            agent.updateRotation = true;
        }
    }

    public void AssignSpot(Spot spot)
    {
        currentSPot = spot;
    }

    public Spot ReleaseSpot()
    {
        Spot releasedSpot = currentSPot;
        currentSPot = null;
        return releasedSpot;
    }

    public void SetDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
    }
}
