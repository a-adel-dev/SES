using System.Collections.Generic;
using UnityEngine;

public class ClassroomStudentsBucket : MonoBehaviour
{
    List<AI> classroomPupils = new List<AI>();
    List<AI> pupilsInClass = new List<AI>();

    public void AddToClassroomPupils(AI pupil)
    {
        classroomPupils.Add(pupil);
    }

    public void AddToPupilsInClass(AI pupil)
    {
        pupilsInClass.Add(pupil);
    }

    public List<AI> GetClassroomStudents()
    {
        return classroomPupils;
    }

    public List<AI> GetPupilsInClass()
    {
        return pupilsInClass;
    }

    public void ShuffleClassroomPupils()
    {
        int listLength = pupilsInClass.Count;
        int random;
        AI temp;
        while (--listLength > 0)
        {
            random = Random.Range(0, listLength);
            temp = pupilsInClass[random];
            pupilsInClass[random] = pupilsInClass[listLength];
            pupilsInClass[listLength] = temp;
        }
    }

    public bool IsInsideClass(AI pupil)
    {
        return (pupilsInClass.Contains(pupil));
    }

    public void RemoveFromClass(AI agent)
    {
        if (pupilsInClass.Contains(agent))
        {
            pupilsInClass.Remove(agent);
        }
    }

    public void ClearStudentsInClass()
    {
        pupilsInClass = new List<AI>();
    }
}
