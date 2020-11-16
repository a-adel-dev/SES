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
    private SchoolMajorSpace currentSpace;
    private float rotSpeed = 2.0f;
    float dotProd;
    
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
    void MoveToNearestEntityOfType(string entityType)
    {
        SchoolSpace closestEntity = FindNearestEntityOfType(entityType);

        SubspaceManager closestEntitySpaceManager = closestEntity.GetComponent<SchoolSpace>().subSpaceManager;
        if (closestEntitySpaceManager.GetAvailableSubSpacesCount() == 0)
            Task.current.Fail();
        else if (closestEntity != null)
        {
            currentSubSpace = closestEntitySpaceManager.PopAvailableSubSpace(this); // add to his inventory
            if (currentSubSpace != null)
            {
                navMesh.SetDestination(currentSubSpace.space.transform.position);
                Task.current.Succeed();
            }
        }
    }

    private SchoolSpace FindNearestEntityOfType(string entityType)
    {

        List<SchoolSpace> entities = new List<SchoolSpace>();

        float nearest = Mathf.Infinity;
        SchoolSpace closestEntity = null;

        if (entityType == "board")
        {
            entities = currentSpace.spaceManager.GetSpacesOfType("Board");
        }
        else if (entityType == "locker")
        {
            entities = currentSpace.spaceManager.GetSpacesOfType("Locker");
        }
        else if (entityType == "bathroom")
        {
            GameObject[] bathrooms = GameObject.FindGameObjectsWithTag("Bathroom");

            for (int i = 0; i < bathrooms.Length; i++)
            {
                entities.Add(bathrooms[i].GetComponent<SchoolSpace>());
            }
        }

        if (entities != null)
        {
            foreach (SchoolSpace entity in entities)
            {
                if (Vector3.Distance(transform.position, entity.transform.position) < nearest)
                {
                    nearest = Vector3.Distance(this.transform.position, entity.transform.position);
                    closestEntity = entity;
                }
            }
        }
        return closestEntity;
    }

    [Task]
    void ReleaseSubLocation()
    {
        currentSubSpace.space.getParentSpace().subSpaceManager.ReleaseSpace(this);
        currentSubSpace = null;
        Task.current.Succeed();
    }

    [Task]
    void LookAtBoard()
    {
        navMesh.updateRotation = false;
        var goal = currentSpace.spaceManager.GetSpacesOfType("Board")[0].transform;
        var lookAtGoal = new Vector3(goal.position.x,
                                    transform.position.y,
                                    goal.position.z);

        Vector3 direction = lookAtGoal - this.transform.position;
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                                    Quaternion.LookRotation(direction),
                                                    Time.deltaTime * rotSpeed);


        dotProd = Vector3.Dot(direction.normalized, transform.forward);
        if (dotProd > .95f)
        {
            Task.current.Succeed();
        }
        navMesh.updateRotation = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<SchoolMajorSpace>())
        {
            currentSpace = other.gameObject.GetComponent<SchoolMajorSpace>();
        }
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();

        style.fontSize = 24;

        //GUI.Label(new Rect(10, 0, 0, 0), "dotProd: " + dotProd, style);
        //GUI.Label(new Rect(10, 25, 0, 0), currentSpace.spaceManager.GetSpacesOfType("Board")[0].name, style);
    }
}
