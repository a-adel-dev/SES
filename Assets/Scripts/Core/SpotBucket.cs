using System.Collections.Generic;
using UnityEngine;

namespace SES.Core
{
    public class SpotBucket : MonoBehaviour
    {

        [SerializeField] List<Spot> desks = new List<Spot>();
        [SerializeField] List<Spot> lockers = new List<Spot>();
        [SerializeField] List<Spot> boardSpots = new List<Spot>();
        [SerializeField] BoxCollider teachersSpace;
        [SerializeField] Transform board;
        [SerializeField] Transform entrance;

        public string availableDesksCount = "";
        public string availableLockersCount = "";


        Stack<Spot> availableDesks  = new Stack<Spot>();
        Stack<Spot> availableLockers  = new Stack<Spot>();


        public List<Spot> BoardSpots
        {
            get
            {
                return boardSpots;
            }
            set
            {
                boardSpots = value;
            }
        }

        public List<Spot> Desks { get => desks; }
        public BoxCollider TeacherSpace { get => teachersSpace; }
        public Transform Board { get => board; }
        public Transform Entrance { get => entrance; }

        



        private void Start()
        {
            PopulateAvailableLockers();
            PopulateAvailableDesks();
        }

        private void Update()
        {
            availableDesksCount = availableDesks.Count.ToString();
            availableLockersCount = availableLockers.Count.ToString();
        }

        public void PopulateAvailableDesks()
        {
            if (desks == null) { return; }
            for (int i = 0; i < desks.Count; i += (SimulationParameters.classroomHalfCapacity ? 2 : 1))
            {
                availableDesks.Push(desks[i]);
            }
        }

        private void PopulateAvailableLockers()
        {
            if (lockers == null) { return; }
            foreach (Spot locker in lockers)
            {
                availableLockers.Push(locker);
            }
        }

        public Spot GetRandomLocker(IAI agent)
        {
            if (availableLockers.Count <= 0)
            {
                return null;
            }
            else
            {
                Spot locker = availableLockers.Pop();
                locker.FillSpot(agent);
                return locker;
            }
        }

        public void ReturnLocker(Spot locker)
        {
            //if (availableLockers.Contains(locker)==false && lockers.Contains(locker))
            //{
            locker.ClearSpot();
            availableLockers.Push(locker);
            //}
            //else
            //{
            //    //Debug.LogError("locker is not in space!");
            //}
        }

        public Spot GetAvailableDesk()
        {
            return availableDesks.Pop();
        }
        public Spot GetAvailableDesk(IAI agent)
        {
            if (availableDesks.Count <= 0)
            {
                return null;
            }
            else
            {
                Spot selectedDesk = availableDesks.Pop();
                selectedDesk.FillSpot(agent);
                return selectedDesk;
            }
        }

        public void ClearDesk(Spot desk)
        {
            //if (availableDesks.Contains(desk) == false && desks.Contains(desk))
            //{
            desk.ClearSpot();
            availableDesks.Push(desk);
            //}
            //else
            //{
            //    //Debug.LogError("desk is not in space!");
            //}
        }

        public int GetAvailableDesksCount()
        {
            return availableDesks.Count;
        }

        public void ResetDesks()
        {
            foreach (Spot desk in desks)
            {
                desk.ClearSpot();  
            }
            PopulateAvailableDesks();
        }

        public void ResetLockers()
        {
            foreach (Spot locker in lockers)
            {
                locker.ClearSpot();
            }
            PopulateAvailableLockers();
        }
    }

}