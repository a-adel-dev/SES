using System.Collections;
using UnityEngine;


public class SimulationProperties : MonoBehaviour
{
    SchoolManager schoolManager;
    public int simLength { get; private set; } = 14;
    public int numPeriods { get; private set; } = 4;
    public int periodLength { get; private set; } = 40;
    public bool activitiesEnabled { get; private set; } = true;
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
}
