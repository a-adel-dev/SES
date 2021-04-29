using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthStats : MonoBehaviour
{
    Health[] totalAgents;
    List<Health> totalInfected = new List<Health>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        return totalInfected.Count;
    }

    void TimeStep()
    {
        foreach (Health agent in totalAgents)
        {
            if (agent.IsInfected())
            {
                totalInfected.Add(agent);
            }
        }
    }
}
