using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherAI : MonoBehaviour
{
    /// <summary>
    /// checks wheather teacher is currently assigned a classroom
    /// </summary>
    bool inClassroom = false;
    /// <summary>
    /// The classroom currently assigned to teacher
    /// </summary>
    Classroom currentClass;
    /// <summary>
    /// The teacher's original room
    /// </summary>
    Teachersroom mainTeacherRoom;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Assigns a teachers room to the teacher
    /// </summary>
    /// <param name="room">the Teacher room to be assigned to the teacher</param>
    public void AssignTeachersRoom(Teachersroom room)
    {
        mainTeacherRoom = room;
    }
    /// <summary>
    /// Assign a classroom to the teacher
    /// </summary>
    /// <param name="classroom">Classroom to be assigned to the teacher</param>
    public void AssignClassRoom(Classroom classroom)
    {
        currentClass = classroom;
    }
    /// <summary>
    /// Sets the teacher to be in a classroom or out based on the status parameter
    /// </summary>
    /// <param name="status">True if teacher is in a class, false if not</param>
    public void SetInClassroomto(bool status)
    {
        inClassroom = status;
    }
    /// <summary>
    /// returns true if teacher is currently in class
    /// </summary>
    public bool IsInClassroom()
    {
        return inClassroom;
    }
}
