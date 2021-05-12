using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SES.Core;

namespace SES.Spaces
{
    public class Teachersroom : MonoBehaviour, ISpace
    {

        //public List<ITeacherAI> originalRoomTeachers = new List<ITeacherAI>();
        //public List<ITeacherAI> teachersCurrentlyInRoom = new List<ITeacherAI>();
        //public List<ITeacherAI> outOfRoomTeachers = new List<ITeacherAI>();
        //public float timeStep;


        //public void AddToOriginalRoomTeachers(ITeacherAI teacher)
        //{
        //    if (originalRoomTeachers.Contains(teacher) == false)
        //    {
        //        originalRoomTeachers.Add(teacher);
        //    }
        //}

        //public void RemoveTeacherFromTeacherRoom(ITeacherAI teacher)
        //{
        //    if(teachersCurrentlyInRoom.Contains(teacher) == false)
        //    {
        //        Debug.Log($"{teacher.GetGameObject().name} is already out of {this.gameObject.name}");
        //        return;
        //    }
        //    outOfRoomTeachers.Add(teacher);
        //    teachersCurrentlyInRoom.Remove(teacher);
        //}


        //public void AddTeacherToTeacherRoom(ITeacherAI teacher)
        //{
        //    if (originalRoomTeachers.Contains(teacher) == false) 
        //    {
        //        Debug.Log($"{teacher.GetGameObject().name} does not belong to {this.gameObject.name}");
        //    }
        //    outOfRoomTeachers.Remove(teacher);
        //    teachersCurrentlyInRoom.Add(teacher);
        //}

        //private void ShuffleRoomOriginalTeachers()
        //{
        //    originalRoomTeachers = ListHandler.Shuffle(originalRoomTeachers);
        //}

        //public void SetTimeStep(float _timeStep)
        //{
        //    timeStep = _timeStep;
        //}
        public GameObject GetGameObject()
        {
            throw new System.NotImplementedException();
        }

        public Spot RequestDesk(IAI teacher)
        {
            throw new System.NotImplementedException();
        }
    }
}
