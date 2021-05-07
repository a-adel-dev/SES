using System.Collections.Generic;
using UnityEngine;
using SES.Core;

namespace SES.Spaces
{
    public class Corridor : MonoBehaviour, ISpace
    {

        List<Spot> pointsOfInterest = new List<Spot>();
        List<IAI> pupilsInCorridors = new List<IAI>();

        // Start is called before the first frame update
        void Start()
        {
            foreach (Transform child in transform)
            {
                pointsOfInterest.Add(child.GetComponent<Spot>());
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Pupil"))
            {
                pupilsInCorridors.Add(other.GetComponent<IAI>());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Pupil"))
            {
                pupilsInCorridors.Remove(other.GetComponent<IAI>());
            }
        }

        public void SetTimeStep(float timeStep)
        {
            throw new System.NotImplementedException();
        }

        public GameObject GetGameObject()
        {
            throw new System.NotImplementedException();
        }
    }
}
