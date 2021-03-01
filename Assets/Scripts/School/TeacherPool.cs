using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherPool : MonoBehaviour
{
    List<TeacherAI> teachersPool = new List<TeacherAI>();
    SchoolManager schoolManager;

    private void Start()
    {
        schoolManager = gameObject.GetComponent(typeof(SchoolManager)) as SchoolManager;
    }


    public List<TeacherAI> GetSchoolTeachers()
    {
        return teachersPool;
    }

    /// <summary>
    /// Adds or removes a teacher from teacherspool
    /// </summary>
    /// <param name="teacher">teacher to be added or removed</param>
    /// <param name="mode">"add" to add a teacher, "remove" to remove a teacher</param>
    public void AddToTeachersPool(TeacherAI teacher)
    {
        teachersPool.Add(teacher);
    }

    public void ShuffleTeachers()
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

    /// set all teachers to be not in class
    /// pick teachers equal to number of classes + number of labs
    /// set them to be inclass
    /// send them to their classes
    ///


}
