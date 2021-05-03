using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class SimulationProperties : MonoBehaviour
{
    SchoolManager schoolManager;
    public int simLength { get; private set; } = 14;
    public int numPeriods { get; private set; } = 4;
    public int periodLength { get; private set; } = 40;
    public bool activitiesEnabled { get; private set; } = true;
    public float timeStep = 4f;
    public float childWalkingSpeed = 0.6f;
    public float adultWalkingSpeed = 1.5f;
    public int sessionActivityMinTime = 8;
    public bool halfCapacity = false;
    public bool classroomHalfCapacity = false;
    public int cooldownClassExit = 0;
    // Use this for initialization
    void Awake()
    {
        schoolManager = GetComponent<SchoolManager>();
    }

    public void SetSimLength(int time)
    {
        simLength = time;
    }

    public void SetNumPeriods(int num)
    {
        numPeriods = num;
    }

    public void SetPeriodLength(int length)
    {
        periodLength = length;
    }

    public void EnableActivities(bool state)
    {
        activitiesEnabled = state;
        if (state)
        {
            foreach (Classroom classroom in schoolManager.subspaces.classrooms)
            {
                classroom.activityPlanner.EnableActivities();
            }
        }
    }
    public void SetWalkingSpeed()
    {
        var agents = FindObjectsOfType<Health>();
        foreach (Health agent in agents)
        {
            if (agent.GetComponent<AI>())
            {
                agent.GetComponent<NavMeshAgent>().speed = childWalkingSpeed * (60f / timeStep);
            }
            else
            {
                agent.GetComponent<NavMeshAgent>().speed = adultWalkingSpeed * (60f / timeStep);
            }
        }
    }
    public void SetSessionActivityMinTime(int time)
    {
        foreach (Classroom classroom in schoolManager.subspaces.classrooms)
        {
            classroom.activityPlanner.SetActivityMinTime(time);
        }
    }
}
