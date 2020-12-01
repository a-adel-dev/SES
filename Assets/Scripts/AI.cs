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
    
    
    //public bool move = false;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (move)
        {
            agent.SetDestination(new Vector3(-20f, 0, 0));
        }
        */
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
            //unested
        }
    }
}
