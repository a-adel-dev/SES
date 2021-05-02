﻿using System.Collections.Generic;
using UnityEngine;


public class classRoomSpawner : MonoBehaviour
{

    [SerializeField] GameObject pupilPrefab;
    [SerializeField] GameObject teacherPrefab;
    ClassroomsObjectsBucket classroomSubSpaces;
    ClassroomStudentsBucket studentsBucket;
    [SerializeField] Transform teacherSpawnMarker;
    TeacherPool teacherspool;
    SchoolManager schoolManager;

    void Start()
    {
        schoolManager = FindObjectOfType<SchoolManager>();
        classroomSubSpaces = GetComponent<ClassroomsObjectsBucket>();
        studentsBucket = GetComponent<ClassroomStudentsBucket>();
        teacherspool = FindObjectOfType<TeacherPool>();
    }

    private void SpawnStudents()
    {
        int counter = 0; // to record student names
        foreach (Spot deskspot in classroomSubSpaces.GetClassroomDesks())
        {
            Vector3 deskPosition = deskspot.gameObject.transform.position;
            GameObject pupil = Instantiate(pupilPrefab,
                        deskPosition,
                        Quaternion.identity) as GameObject;
            AI pupilAI = pupil.GetComponent<AI>();
            pupilAI.SetStudentStatusTo(AIStatus.inClass);
            pupil.transform.parent = transform;
            pupilAI.SetOriginalPosition(new Vector3(deskPosition.x, 0, deskPosition.z));
            pupilAI.SetMainClassroom(GetComponent<Classroom>());
            pupilAI.SetCurrentClass(GetComponent<Classroom>());
            studentsBucket.AddToClassroomPupils(pupilAI);
            studentsBucket.AddToPupilsInClass(pupilAI);
            pupil.name = $"{pupilAI.currentClass.gameObject.name}_pupil {counter}";
            counter++;
        }
        studentsBucket.ShuffleClassroomPupils();
    }

    public void SpawnAgents()
    {
        SpawnStudents();
        SpawnTeacher();
    }

    void SpawnTeacher()
    {
        GameObject teacher = Instantiate(teacherPrefab, teacherSpawnMarker.position, Quaternion.identity);
        TeacherAI teacherAgent = teacher.GetComponent<TeacherAI>();
        teacherspool.AddToTeachersPool(teacherAgent);
        schoolManager.AddOrphandTeacher(teacherAgent);
        teacherAgent.SetInClassroomto(true);
        teacherAgent.AssignClassRoom(GetComponent<Classroom>());
    }

    public Transform GetTeacherSpawnerPos()
    {
        return teacherSpawnMarker;
    }
}