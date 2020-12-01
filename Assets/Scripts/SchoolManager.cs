using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolManager : MonoBehaviour
{
    //Sim Parameters
    [Range(0.1f, 60.0f)] 
    [SerializeField] float simTimeScale = .2f;
    [SerializeField] int numPeriods = 4;
    [SerializeField] int periodLength = 40;
    [Range(5, 30)]
    [SerializeField] int sessionActivityMinTime = 8;

    //space lists
    List<Classroom>     classrooms = new List<Classroom>();
    List<Bathroom>      bathrooms = new List<Bathroom>();
    List<Corridor>      corridors = new List<Corridor>();
    List<Teachersroom>  teachersrooms = new List<Teachersroom>();
    List<Lab>           labs = new List<Lab>();

    //class global properties
    public bool classInSession { get; private set; }
    [SerializeField]  int schoolTime = 0; // exposed for debugging
    bool schoolDay = false;

    //Class internal Properties
    List<int> classTimes = new List<int>();
    float timer = 0f;
    int currentPeriodIndex = 0; 

    private void Awake()
    {
        AllocateSubSpaces();
        ScheduleClasses();
        
    }

    private void Start()
    {
        StartSchoolDay();
    }

    private void StartSchoolDay()
    {
        schoolDay = true;
        classInSession = true;
        foreach (Classroom classroom in classrooms)
        {
            classroom.StartClass();
        }
    }

    private void AllocateSubSpaces()
    {
        var classroomsArray = FindObjectsOfType<Classroom>();
        foreach (var classroom in classroomsArray)
        {
            classrooms.Add(classroom);
        }

        var bathroomArray = FindObjectsOfType<Bathroom>();
        foreach (var bathroom in bathroomArray)
        {
            bathrooms.Add(bathroom);
        }

        var corridorArray = FindObjectsOfType<Corridor>();
        foreach (var corridor in corridorArray)
        {
            corridors.Add(corridor);
        }

        var teacherroomsArray = FindObjectsOfType<Teachersroom>();
        foreach (var teacherRoom in teacherroomsArray)
        {
            teachersrooms.Add(teacherRoom);
        }

        var labsArray = FindObjectsOfType<Lab>();
        foreach (var lab in labsArray)
        {
            labs.Add(lab);
        }
    }

    private void Update()
    {
        RecordTime();
        OssilateClassSessions();
    }

    private void OssilateClassSessions()
    {
        if (!schoolDay)
            return;
        if (currentPeriodIndex == classTimes.Count)
        {
            EndSchoolDay();
            classTimes = new List<int>();
        }
        if (currentPeriodIndex % 2 == 0)
        {
            if (schoolTime > classTimes[currentPeriodIndex])
            {
                classInSession = false;
                currentPeriodIndex++;
                foreach (Classroom classroom in classrooms)
                {
                    classroom.EndClass();
                }

            }
        }
        else if (currentPeriodIndex % 2 != 0)
        {
            if (schoolTime > classTimes[currentPeriodIndex])
            {
                classInSession = true;
                currentPeriodIndex++;
                foreach (Classroom classroom in classrooms)
                {
                    classroom.StartClass();
                }
            }
        }
        
    }

    private void EndSchoolDay()
    {
        schoolDay = false;
        classInSession = false;
        Time.timeScale = 0;
    }

    private void RecordTime()
    {
        timer += Time.deltaTime;
        if (timer >= simTimeScale)
        {
            timer -= simTimeScale;
            schoolTime++;
        }
        //Debug.Log(classTime);
    }

    public int GetPeriodTime()
    {
        return periodLength;
    }

    public float GetTimeStep()
    {
        return simTimeScale;
    }

    public int GetSessionActivityMinTime()
    {
        return sessionActivityMinTime;
    }

    private void ScheduleClasses()
    {
        for (int i = 0; i < numPeriods * 2; i++)
        {
            if (i == 0)
            {
                classTimes.Add(40);
                continue;
            }
            else if (i % 2 != 0)
            { 
                classTimes.Add(classTimes[i - 1] + (60 - periodLength));
            }
            else if (i % 2 == 0)
            {
                classTimes.Add(classTimes[i - 1] + periodLength);
            }
        }

    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 24;
        GUI.Label(new Rect(10, 0, 0, 0), "School Time:" + schoolTime, style);

        if (classInSession)
        {
            GUI.Label(new Rect(200, 0, 0, 0), "Classes in Session" ,  style);
        }
    }
}
