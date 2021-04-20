using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TeacherNavigation : MonoBehaviour
{
    NavMeshAgent agent;
    TeacherAI Ai;
    bool wandering = false;
    SchoolManager schoolManager;
    float timeStep;

    // Start is called before the first frame update
    void Start()
    {
        schoolManager = FindObjectOfType<SchoolManager>();
        timeStep = schoolManager.simTimeScale;
        agent = GetComponent<NavMeshAgent>();
        Ai = GetComponent<TeacherAI>();
        
    }

    // Update is called once per frame
    void Update()
    {
        var remaining = (agent.destination - this.transform.position);
        Debug.DrawRay(this.transform.position, remaining, Color.blue);
    }

    /// <summary>
    /// Directs agent to a point, ignoring its hight information
    /// </summary>
    /// <param name="destination">The position of the target location</param>
    public void GuideTo(Vector3 destination)
    {
        agent.SetDestination(new Vector3(destination.x, 0f, destination.z));
    }

    public void GoToClassRoom()
    {
        SetWandering(false);
        GuideTo(Ai.currentClass.GetTeacherSpawnerPos().position);
        SetWandering(true);
    }

    public void GoToTeachersRoom()
    {
        Debug.Log($"teacher is going to teachersRoom");
        Spot desk = Ai.mainTeacherRoom.GetComponent<DesksBucket>().GetAvailableDesk();
        GuideTo(desk.transform.position);
        desk.FillSpot(GetComponent<TeacherAI>());
    }


    public void SetWandering(bool status)
    {
        wandering = status;
    }

    public IEnumerator Wander()
    {
        

        while (wandering)
        {
            BoxCollider area = null;
            if (Ai.currentClass != null)
            {
                area = Ai.currentClass.GetTeacherSpace();
            }
            else if (Ai.ownLab != null)
            {
                area = Ai.ownLab.GetTeacherSpace();
            }
            Vector3 bounds_min = area.bounds.min;
            Vector3 bounds_max = area.bounds.max;
            float waypoint_x = Random.Range(bounds_min[0], bounds_max[0]);
            float waypoint_z = Random.Range(bounds_min[2], bounds_max[2]);
            Vector3 waypoint= new Vector3(waypoint_x, 0f, waypoint_z );
            Debug.Log($"moving");
            GuideTo(waypoint);
            yield return new WaitForSeconds(Random.Range(5f, 20f ) * timeStep);
        }
    }
}
