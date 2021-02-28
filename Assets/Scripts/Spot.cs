using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spot : MonoBehaviour
{
    private bool available = true;
    private AI _agent;
    private TeacherAI _teacher;

    // add an agent to a spot and make it occupied
    public void FillSpot(AI agent)
    {
        available = false;
        _agent = agent;
    }

    public void FillSpot(TeacherAI teacher)
    {
        available = false;
        _teacher = teacher;
    }

    //make the spot available and return the agent that was occupying the spot
    public void ClearSpot()
    {
        available = true;
    }

    //check if the spot was available
    public bool ISpotAvailable()
    {
        return available;
    }
}
