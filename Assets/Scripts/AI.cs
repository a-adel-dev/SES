using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    
    //cached variables 
    NavMeshAgent agent;


    //properties
    private bool busy = false;
    Vector3 originalPosition;
    Classroom currentClass;
    bool idle = true;
    Spot currentSpot;
    Vector3 distination;
    
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        SetDestination(distination);
        SetIdlePose();
    }

    
    /*=============================================
     * Properties Getters, setters
     * ============================================
     */

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
    /*
    ===============================================
              Collision space detection
    ================================================
    */
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Classroom"))
        {
            currentClass = other.GetComponent<Classroom>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Classroom"))
        {
            other.GetComponent<Classroom>().GetPupilOutofClassroom(this);
            //untested
        }
    }

    /*
     * =====================================
     *            Directions Controls
     * ======================================
    */
    public void BackToDesk()
    {
        distination = originalPosition;
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


    /*=================================
     * Continuous Methods
     * ================================
     */
    public void SetDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    private void SetIdlePose()
    {
        if (Vector3.Distance(transform.position, originalPosition) < .1f)
        {
            idle = true;
            LookAtBoard();
        }
    }
}
