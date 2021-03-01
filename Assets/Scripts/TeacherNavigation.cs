using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TeacherNavigation : MonoBehaviour
{
    NavMeshAgent agent;
    TeacherAI Ai;
    
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Ai = GetComponent<TeacherAI>();
    }

    // Update is called once per frame
    void Update()
    {
        var remaining = (agent.destination - this.transform.position);
        Debug.DrawRay(this.transform.position, remaining, Color.red);
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
        GuideTo(Ai.currentClass.transform.position);
    }

    public void GoToTeachersRoom()
    {
        GuideTo(Ai.mainTeacherRoom.gameObject.transform.position);
    }
}
