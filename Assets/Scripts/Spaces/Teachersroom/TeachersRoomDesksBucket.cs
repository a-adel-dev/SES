//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using SES.Core;

//namespace SES.Spaces.TeacherRoom
//{
//    public class TeachersRoomDesksBucket : MonoBehaviour
//    {
//        public List<Spot> desks;
//        List<Spot> availableDesks = new List<Spot>();

//        public void PopulateAvailableDesks()
//        {
//            foreach (Spot desk in desks)
//            {
//                if (desk.ISpotAvailable())
//                {
//                    availableDesks.Add(desk);
//                }
//            }
//        }

//        public Spot GetAvailableDesk()
//        {
//            Spot selectedDesk = availableDesks[Random.Range(0, availableDesks.Count)];
//            availableDesks.Remove(selectedDesk);
//            return selectedDesk;
//        }

//        public List<Spot> GetDesks()
//        {
//            return desks;
//        }

//        public void ShuffleDesks()
//        {
//            desks = ListHandler.Shuffle(desks);
//        }
//    }
//}
