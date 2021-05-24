//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using SES.Core;

//namespace SES.School
//{
//    public class TeacherPool : MonoBehaviour
//    {
//        List<ITeacherAI> teachersPool = new List<ITeacherAI>();
//        List<ITeacherAI> labTeachersPool = new List<ITeacherAI>();
//        List<ITeacherAI> orphandTeachers = new List<ITeacherAI>();
//        SchoolSubSpacesBucket schoolSubspaces;
//        int teacherRoomIndex = 0; //An index to keep trak of which teacher room will be used to assign an orphand teacher to

//        private void Start()
//        {
//            Debug.Log($"teacherpool attached to {this.gameObject.name}");
//            schoolSubspaces = GetComponent<SchoolSubSpacesBucket>();
//            Invoke(nameof(AllocateOrpahanedTeachers), 5.0f);
//        }
//        public List<ITeacherAI> GetSchoolTeachers()
//        {
//            return teachersPool;
//        }

//        public void AddToTeachersPool(ITeacherAI teacher)
//        {
//            teachersPool.Add(teacher);
//        }

//        public void AddToLabTeachersPool(ITeacherAI teacher)
//        {
//            labTeachersPool.Add(teacher);
//        }

//        public void ShuffleSchoolTeachers()
//        {
//            teachersPool = ListHandler.Shuffle(teachersPool);
//        }


//        public void AllocateOrpahanedTeachers(int teacherroomsCount)
//        {

//            if (orphandTeachers.Count <= 0)
//            {
//                return;
//            }

//            foreach (ITeacherAI teacher in orphandTeachers.ToArray())
//            {
//                if (teacherRoomIndex == teacherroomsCount)
//                {
//                    teacherRoomIndex = 0;
//                }
//                //schoolSubspaces.teachersrooms[teacherRoomIndex].AddToOriginalRoomTeachers(teacher);
//                //schoolSubspaces.teachersrooms[teacherRoomIndex].RemoveTeacherFromTeacherRoom(teacher);
//                teacher.AssignTeachersRoom(schoolSubspaces.teachersrooms[teacherRoomIndex]);
//                orphandTeachers.Remove(teacher);
//                teacherRoomIndex++;
//            }
//        }

//        public void AddOrphandTeacher(ITeacherAI teacher)
//        {
//            orphandTeachers.Add(teacher);
//        }
//    }
//}
