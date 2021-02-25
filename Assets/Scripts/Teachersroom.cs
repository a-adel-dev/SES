using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teachersroom : MonoBehaviour
{
    bool classesInsession = false;
    SchoolManager schoolManager;
    /// <summary>
    /// Teachers belonging to this room
    /// </summary>
    List<TeacherAI> roomTeachers = new List<TeacherAI>();
    /// <summary>
    /// Teachers currently in room
    /// </summary>
    List<TeacherAI> TeachersInRoom = new List<TeacherAI>();


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
}
