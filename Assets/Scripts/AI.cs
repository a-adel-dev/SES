using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    NavMeshAgent agent;
    private bool ready = true;
    public bool move = false;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            agent.SetDestination(new Vector3(-20f, 0, 0));
        }
    }

    public void Move()
    {
       agent.SetDestination(new Vector3(-20f, 0, 0));  
    }

    public void SetReadyStatusTO(bool status)
    {
        ready = status;
    }

    public bool IsReady()
    {
        return ready;
    }
}
