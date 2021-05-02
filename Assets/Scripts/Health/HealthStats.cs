using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthStats : MonoBehaviour
{
    Health[] totalAgents;
    List<Health> totalInfected = new List<Health>();
    public int numInfected = 0;
    public List<Health> teachers = new List<Health>();
    public List<Health> students = new List<Health>();

    public void CollectAgents()
    {
        totalAgents = FindObjectsOfType<Health>();
    }

    public int GetNumAgents()
    {
        if(totalAgents == null) { return 0;}
        return totalAgents.Length;
    }

    public int GetNumInfected()
    {
        return numInfected;
    }
    private void Update()
    {
        numInfected = Health.numInfected;
    }
    void TimeStep()
    {
        foreach (Health agent in totalAgents)
        {
            if (agent.IsInfected() && !totalInfected.Contains(agent))
            {
                totalInfected.Add(agent);
            }
        }   
    }

    public void PopulateAgentLists()
    {
        foreach (Health agent in totalAgents)
        {
            if (agent.GetComponent<TeacherAI>())
            {
                teachers.Add(agent);
            }
            else
            {
                students.Add(agent);
            }
        }
    }

    public List<Health> GetStudents()
    {
        return students;
    }

    public List<Health> GetTeachers()
    {
        return teachers;
    }
}
