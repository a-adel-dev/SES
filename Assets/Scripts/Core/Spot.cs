using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SES.Core
{
    public class Spot : MonoBehaviour
    {
        private bool available = true;
        private IAI occupyingAgent;


        // add an agent to a spot and make it occupied
        public void FillSpot(IAI agent)
        {
            available = false;
            occupyingAgent = agent;
        }

        //make the spot available and return the agent that was occupying the spot
        public IAI ClearSpot()
        {
            available = true;
            IAI agent = occupyingAgent;
            occupyingAgent = null;
            return agent;
        }

        //check if the spot was available
        public bool ISpotAvailable()
        {
            return available;
        }
    }
}
