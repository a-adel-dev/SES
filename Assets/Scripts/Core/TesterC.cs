﻿//using SES.AIControl;
//using SES.Core;
//using SES.Spaces;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Tester : MonoBehaviour
//{
//    public TeacherAI teacherAgent;
//    public Teachersroom teacherroom;
//    List<ITeacherAI> teachers = new List<ITeacherAI>();
//    int index = 1;
//    // Start is called before the first frame update
//    void Start()
//    {
//        for (int i = 0; i < 10; i++)
//        {
//            GameObject teacherObject = Instantiate(teacherAgent.gameObject, transform.position, Quaternion.identity);
//            teacherObject.name = $"teacher {index}";
//            TeacherAI teacherAI = teacherObject.GetComponent<TeacherAI>();
//            teachers.Add(teacherAI);
//            index++;
//        }
//        Debug.Log($"numbers of initial teachers is {teachers.Count}");
//        foreach (ITeacherAI teacher in teachers)
//        {
//            teacherroom.AddToOriginalRoomTeachers(teacher);
//            teacherroom.AddTeacherToTeacherRoom(teacher);
//        }

//        teacherroom.RemoveTeacherFromTeacherRoom(teachers[UnityEngine.Random.Range(0, teachers.Count)]);
//        teacherroom.RemoveTeacherFromTeacherRoom(teachers[UnityEngine.Random.Range(0, teachers.Count)]);
//        teacherroom.RemoveTeacherFromTeacherRoom(teachers[UnityEngine.Random.Range(0, teachers.Count)]);
//        teacherroom.RemoveTeacherFromTeacherRoom(teachers[UnityEngine.Random.Range(0, teachers.Count)]);

//        Debug.Log($"Number of room original teachers should be 10, it is {teacherroom.originalRoomTeachers.Count}");
//        Debug.Log($"Number of teachers currently in room is {teacherroom.teachersCurrentlyInRoom.Count}");
//        Debug.Log($"Number of teachers out of room is {teacherroom.outOfRoomTeachers.Count}");
//        Debug.Log($"Original Room Teachers:");
//        printAgentsInAGroup(teacherroom.originalRoomTeachers);
//        Debug.Log($"teahers in room:");
//        printAgentsInAGroup(teacherroom.teachersCurrentlyInRoom);
//        Debug.Log($"teahers out of room:");
//        printAgentsInAGroup(teacherroom.outOfRoomTeachers);
//    }

//    private void printAgentsInAGroup(List<ITeacherAI> group)
//    {
//        foreach (TeacherAI teacher in group)
//        {
//            Debug.Log(teacher.gameObject.name);
//        }
//    }



//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//}
