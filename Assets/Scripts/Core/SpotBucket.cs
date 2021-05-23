using System.Collections.Generic;
using UnityEngine;

namespace SES.Core
{
    public class SpotBucket : MonoBehaviour
    {
        public List<Spot> desks = new List<Spot>();
        public List<Spot> lockers = new List<Spot>();
        public List<Spot> boardSpots = new List<Spot>();
        public BoxCollider teachersSpace;
        public GameObject board;
        public GameObject entrance;
        List<Spot> availableLockers;
        List<Spot> availableDesks = new List<Spot>();

        private void Start()
        {
            PopulateAvailableLockers();
            PopulateAvailableDesks();
        }

        public void PopulateAvailableDesks()
        {
            if (desks == null) { return; }
            foreach (Spot desk in desks)
            {
                if (desk.ISpotAvailable())
                {
                    availableDesks.Add(desk);
                }
            }
        }

        private void PopulateAvailableLockers()
        {
            if (lockers == null) { return; }
            availableLockers = new List<Spot>(lockers);
        }

        public Spot GetRandomLocker()
        {
            if (availableLockers.Count <= 0)
            {
                return null;
            }
            else
            {
                Spot randomLocker;
                int randomIndex = Random.Range(0, availableLockers.Count);
                randomLocker = availableLockers[randomIndex];
                availableLockers.Remove(randomLocker);
                return randomLocker;
            }
        }

        public void ReturnLocker(Spot locker)
        {
            if (availableLockers.Contains(locker)==false && lockers.Contains(locker))
            {
                availableLockers.Add(locker);
            }
            else
            {
                Debug.LogError("locker is not in class!");
            }
        }


        public Spot GetAvailableDesk()
        {
            if (availableDesks.Count <= 0)
            {
                Debug.Log($"Could not find an available desk!");
            }
            Spot selectedDesk = availableDesks[Random.Range(0, availableDesks.Count)];
            availableDesks.Remove(selectedDesk);
            return selectedDesk;
        }
    }

}