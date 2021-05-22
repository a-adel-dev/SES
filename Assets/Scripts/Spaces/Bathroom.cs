using System.Collections.Generic;
using UnityEngine;
using SES.Core;

namespace SES.Spaces
{
    public class Bathroom : MonoBehaviour, IBathroom
    {
        List<Spot> toilets = new List<Spot>();
        List<Spot> availableToilets = new List<Spot>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Toilet"))
            {
                toilets.Add(other.GetComponent<Spot>());
                availableToilets.Add(other.GetComponent<Spot>());
            }
        }


        public void ReleaseToilet(Spot toilet)
        {
            toilet.ClearSpot();
            availableToilets.Add(toilet);
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public Spot RequestToilet(IAI student)
        {
            ListHandler.Shuffle(toilets);
            foreach (Spot toilet in toilets)
            {
                if (toilet.ISpotAvailable())
                {
                    toilet.FillSpot(student);
                    return toilet;
                }
            }
            return null;
        }

        public Spot RequestDesk(IAI agent)
        {
            throw new System.NotImplementedException();
        }
    }
}
