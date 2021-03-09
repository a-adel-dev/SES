using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teachersroom : MonoBehaviour
{
    bool classesInsession = false;
    SchoolManager schoolManager;
    /// <summary>
    /// Teachers belonging to this teachers room
    /// </summary>
    List<TeacherAI> roomTeachers = new List<TeacherAI>();
    /// <summary>
    /// Teachers currently in teachers room
    /// </summary>
    List<TeacherAI> teachersInRoom = new List<TeacherAI>();

    List<TeacherAI> teachersInClassroom = new List<TeacherAI>();


    // Start is called before the first frame update
    void Start()
    {
        schoolManager = FindObjectOfType<SchoolManager>();
    }

    // Update is called once per frame
    void Update()
    {
        classesInsession = schoolManager.classInSession;
    }


    public void AddToRoomTeachers(TeacherAI teacher)
    {
        //if (!roomTeachers.Contains(teacher))
        {
            roomTeachers.Add(teacher);
        }
    }

    
    public void AddToClassroomTeachers(TeacherAI teacher)
    {
        teachersInClassroom.Add(teacher);
    }

    private void ShuffleRoomTeachers()
    {
        int listLength = teachersInRoom.Count;
        int random;
        TeacherAI temp;
        while (--listLength > 0)
        {
            random = Random.Range(0, listLength);
            temp = teachersInRoom[random];
            teachersInRoom[random] = teachersInRoom[listLength];
            teachersInRoom[listLength] = temp;
        }
    }

}
