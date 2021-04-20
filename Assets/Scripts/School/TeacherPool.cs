using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherPool : MonoBehaviour
{
    List<TeacherAI> teachersPool = new List<TeacherAI>();
    List<TeacherAI> labTeachersPool = new List<TeacherAI>();

    public List<TeacherAI> GetSchoolTeachers()
    {
        return teachersPool;
    }

    /// <summary>
    /// Adds or removes a teacher from teachers pool
    /// </summary>
    /// <param name="teacher">teacher to be added</param>
    public void AddToTeachersPool(TeacherAI teacher)
    {
        teachersPool.Add(teacher);
    }

    /// <summary>
    /// Adds or removes a teacher from lab teachers pool
    /// </summary>
    /// <param name="teacher">teacher to be added</param>
    public void AddToLabTeachersPool(TeacherAI teacher)
    {
        labTeachersPool.Add(teacher);
    }

    public void ShuffleSchoolTeachers()
    {
        int listLength = teachersPool.Count;
        int random;
        TeacherAI temp;
        while (--listLength > 0)
        {
            random = Random.Range(0, listLength);
            temp = teachersPool[random];
            teachersPool[random] = teachersPool[listLength];
            teachersPool[listLength] = temp;
        }
    }
}
