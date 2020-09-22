using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{

    Vector3 originalPosition;
    [SerializeField] Vector3 destination; //Exposed for Debugging
    NavMeshAgent navMeshAgent;
    bool moving = false;
    [SerializeField] float standingCounter = 0;
    [SerializeField] Vector3 velocity;

    [SerializeField] bool inClassroom;
    [SerializeField] bool inCorridor;
    [SerializeField] bool inBathroom;
    [SerializeField] bool inToilet;
    [SerializeField] bool hasDestination = false;
    LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        originalPosition = transform.position;
        lineRenderer = GetComponent<LineRenderer>();
        destination = transform.position;
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = navMeshAgent.velocity;
        navMeshAgent.SetDestination(destination);
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1,  destination);
        if (navMeshAgent.velocity.x < .01 && navMeshAgent.velocity.x > -.01 &&
            navMeshAgent.velocity.y < .01 && navMeshAgent.velocity.y > -.01)
        {
            standingCounter += Time.deltaTime;
        }
        else
        {
            standingCounter = 0;
        }
    }

    public void SetDestination(Vector3 newDestination)
    {
        destination = newDestination;
    }

    public bool IsMoving()
    {
        if (navMeshAgent.velocity != Vector3.zero)
            return true;
        else
            return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Classroom"))
        {
            inClassroom = true;
        }

        else if (other.CompareTag("Bathroom"))
        {
            inBathroom = true;
        }

        else if (other.CompareTag("Toilet"))
        {
            inToilet = true;
        }

        else if (other.CompareTag("Corridor"))
        {
            inCorridor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Classroom"))
        {
            inClassroom = false;
        }

        else if (other.CompareTag("Bathroom"))
        {
            inBathroom = false;
        }

        else if (other.CompareTag("Toilet"))
        {
            inToilet = false;
        }

        else if (other.CompareTag("Corridor"))
        {
            inCorridor = false;
        }
    }

    public bool GetInClassroom()
    {
        return inClassroom;
    }
    public bool GetInBathroom()
    {
        return inBathroom;
    }
    public bool GetInToilet()
    {
        return inToilet;
    }
    public bool GetInCorridor()
    {
        return inCorridor;
    }

    public void SetHasDestination(bool status)
    {
        hasDestination = status;
    }
        
    public bool GetHasDestination()
    {
        return hasDestination;
    }

    public float GetStandingCounter()
    {
        return standingCounter;
    }

    public void GoBack()
    {
        destination = originalPosition;
    }

}
