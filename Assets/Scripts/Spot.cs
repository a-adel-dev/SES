using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spot : MonoBehaviour
{
    private bool available = true;
    private AI _agent;

    // add an agent to a spot and make it occupied
    public void FillSpot(AI agent)
    {
        available = false;
        _agent = agent;
    }

    //make the spot available and return the agent that was occupying the spot
    public AI ClearSpot()
    {
        available = true;
        return _agent;
    }

    //check if the spot was available
    public bool ISpotAvailable()
    {
        return available;
    }
}
