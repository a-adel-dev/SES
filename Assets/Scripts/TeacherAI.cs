using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MovementStyle {  classroom, restricted}
public class TeacherAI : MonoBehaviour
{
    /// <summary>
    /// checks wheather teacher is currently assigned a classroom
    /// </summary>
    bool inClassroom = false;
    /// <summary>
    /// The classroom currently assigned to teacher
    /// </summary>
    public Classroom currentClass { get; private set; }
    /// <summary>
    /// The teacher's original room
    /// </summary>
    public Lab ownLab { get; private set; }
    public Teachersroom mainTeacherRoom { get; private set; }
    public Spot currentTeacherDesk;
    TeacherNavigation teacherNav;
    Health health;
    public MovementStyle movementStyle = MovementStyle.classroom;

    // Start is called before the first frame update
    void Start()
    {
        teacherNav = GetComponent<TeacherNavigation>();
        health = GetComponent<Health>();
        if (IsInClassroom())
        {
            StartWander(MovementStyle.restricted);
            health.SetActivityType(ActivityType.LoudTalking);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsInClassroom())
        {
            StopWandering();
            health.SetActivityType(ActivityType.Talking);
        }
    }
    /// <summary>
    /// Assigns a teachers room to the teacher
    /// </summary>
    /// <param name="room">the Teacher room to be assigned to the teacher</param>
    public void AssignTeachersRoom(Teachersroom room)
    {
        mainTeacherRoom = room;
    }

    public void AssignLab(Lab lab)
    {
        ownLab = lab;
    }
    /// <summary>
    /// Assign a classroom to the teacher
    /// </summary>
    /// <param name="classroom">Classroom to be assigned to the teacher</param>
    public void AssignClassRoom(Classroom classroom)
    {
        currentClass = classroom;
    }

    public void ClearClassRoom()
    {
        currentClass = null;
    }
    /// <summary>
    /// Sets the teacher to be in a classroom or out based on the status parameter
    /// </summary>
    /// <param name="status">True if teacher is in a class, false if not</param>
    public void SetInClassroomto(bool status)
    {
        inClassroom = status;
        ClearTeacherDesk();
    }
    /// <summary>
    /// returns true if teacher is currently in class
    /// </summary>
    public bool IsInClassroom()
    {
        return inClassroom;
    }

    public void AssignTeacherDesk(Spot desk)
    {
        currentTeacherDesk = desk;
    }

    public void ClearTeacherDesk()
    {
        currentTeacherDesk = null;
    }
    /// <summary>
    /// moves the teacher about in a classroom
    /// </summary>
    /// <param name="style">classroom if the teacher is free to move in the class, restricted if the teacher is bound to his area</param>
    private void StartWander(MovementStyle style)
    {
        teacherNav.SetWandering(true);
        if (style == MovementStyle.restricted)
        {
            StartCoroutine(teacherNav.Wander());
            movementStyle = MovementStyle.restricted;
        }
        else if (style == MovementStyle.classroom)
        {
            StartCoroutine(teacherNav.Wander());
            movementStyle = MovementStyle.classroom;
        }
    }

    private void StopWandering()
    {
        teacherNav.SetWandering(false);
    }

    public void RestrictClassMovement()
    {
        teacherNav.StopWandering();
        StartWander(MovementStyle.restricted);
    }

    public void FreeClassMovement()
    {
        teacherNav.StopWandering();
        StartWander(MovementStyle.classroom);
    }

}
