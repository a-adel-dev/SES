//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using SES.Core;

//namespace SES.Spaces.lab
//{
//    public class LabTeacherSpawner : MonoBehaviour
//    {
//        [SerializeField] GameObject teacherPrefab;
//        [SerializeField] List<Transform> spawnPositions = new List<Transform>();

//        // Start is called before the first frame update
//        void Start()
//        {
//            SpawnTeachers();
//        }

//        // Update is called once per frame

//        void SpawnTeachers()
//        {
//            for (int i = 0; i < spawnPositions.Count; i++)
//            {
//                GameObject teacher = Instantiate(teacherPrefab, spawnPositions[i].transform.position, Quaternion.identity);

//                TeacherAI teacherAgent = teacher.GetComponent<TeacherAI>();
//                teacherAgent.SetInClassroomto(false);
//                labTeacherspool.AddToLabTeachersPool(teacherAgent);
//                teacherAgent.AssignLab(gameObject.GetComponent<Lab>());
//                teacherAgent.SetInClassroomto(true);
//            }

//        }
//    }
//}
