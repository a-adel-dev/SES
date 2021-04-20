using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabTeacherSpawner : MonoBehaviour
{
    [SerializeField] GameObject teacherPrefab;
    TeacherPool labTeacherspool;
    [SerializeField] List<Transform> spawnPositions = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        labTeacherspool = FindObjectOfType<TeacherPool>();
        SpawnTeachers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnTeachers()
    {        
        for (int i = 0; i < spawnPositions.Count ; i++)
        {
            GameObject teacher = Instantiate(teacherPrefab, spawnPositions[i].transform.position, Quaternion.identity);
            TeacherAI teacherAgent = teacher.GetComponent<TeacherAI>();
            teacherAgent.SetInClassroomto(false);
            labTeacherspool.AddToLabTeachersPool(teacherAgent);
            teacherAgent.AssignLab(gameObject.GetComponent<Lab>());
            teacherAgent.SetInClassroomto(true);
        }

    }
}
