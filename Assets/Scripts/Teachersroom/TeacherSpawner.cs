using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherSpawner : MonoBehaviour
{
    [SerializeField] List<Spot> desks;
    [SerializeField] GameObject teacherPrefab;
    TeacherPool teacherspool;

    // Start is called before the first frame update
    void Start()
    {
        
        teacherspool = FindObjectOfType<TeacherPool>();
        SpawnTeachers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnTeachers()
    {
        int numTeachersTobeSpawned = desks.Count / 2;
        ShuffleDesks();
        for (int i = 0; i < numTeachersTobeSpawned; i++)
        {
            GameObject teacher = Instantiate(teacherPrefab, desks[i].transform.position, Quaternion.identity);
            TeacherAI teacherAgent= teacher.GetComponent<TeacherAI>();
            teacherAgent.SetInClassroomto(false);
            teacherspool.AddToTeachersPool(teacherAgent);
            teacherAgent.AssignTeachersRoom(gameObject.GetComponent<Teachersroom>());
        }
        
    }

    private void ShuffleDesks()
    {
        int listLength = desks.Count;
        int random;
        Spot temp;
        while (--listLength > 0)
        {
            random = Random.Range(0, listLength);
            temp = desks[random];
            desks[random] = desks[listLength];
            desks[listLength] = temp;
        }
    }
}
