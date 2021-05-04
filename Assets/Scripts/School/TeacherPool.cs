using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeacherPool : MonoBehaviour
{
    SchoolManager schoolManager;
    List<TeacherAI> teachersPool = new List<TeacherAI>();
    List<TeacherAI> labTeachersPool = new List<TeacherAI>();
    List<TeacherAI> orphandTeachers = new List<TeacherAI>();
    int teacherRoomIndex = 0; //An index to keep trak of which teacher room will be used to assign an orphand teacher to

    private void Awake()
    {
        schoolManager = GetComponent<SchoolManager>();
    }

    private void Start()
    {
        Invoke(nameof(AllocateOrpahanedTeachers), 5.0f);
    }
    public List<TeacherAI> GetSchoolTeachers()
    {
        return teachersPool;
    }

    public void AddToTeachersPool(TeacherAI teacher)
    {
        teachersPool.Add(teacher);
    }

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

    
    public void AllocateOrpahanedTeachers()
    {

        if (orphandTeachers.Count <= 0)
        {
            return;
        }

        foreach (TeacherAI teacher in orphandTeachers.ToArray())
        {
            if (teacherRoomIndex == schoolManager.subspaces.teachersrooms.Length)
            {
                teacherRoomIndex = 0;
            }
            schoolManager.subspaces.teachersrooms[teacherRoomIndex].AddToRoomTeachers(teacher);
            schoolManager.subspaces.teachersrooms[teacherRoomIndex].AddToClassroomTeachers(teacher);
            teacher.AssignTeachersRoom(schoolManager.subspaces.teachersrooms[teacherRoomIndex]);
            orphandTeachers.Remove(teacher);
            teacherRoomIndex++;
        }
    }

    public void AddOrphandTeacher(TeacherAI teacher)
    {
        orphandTeachers.Add(teacher);
    }
}
