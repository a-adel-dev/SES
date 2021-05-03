using System;
using System.Collections.Generic;
using UnityEngine;

public class SchoolManager : MonoBehaviour
{
    List<Classroom>     inPlaceClassrooms = new List<Classroom>(); //classrooms with classes in place i.e. not in a lab
    List<ClassLabPair>  classlabPairList = new List<ClassLabPair>();
    List<TeacherAI> teacherspool;

    public SchoolSubSpacesBucket subspaces;
    public TeacherPool teacherPoolController;
    public SimulationProperties sim;
    public SchoolDaySchedular schedular;
    public bool classInSession { get; private set; }
    public int schoolTime = 0;
    public DateTime schoolDateTime { get; private set; }
    bool schoolDay = false;

    int currentPeriodIndex = 0;
    ClassroomState currentState = ClassroomState.inSession;
    ClassroomState previousState = ClassroomState.onBreak;
    HealthStats healthStats;
    GeneralHealthParamaters healthParameters;

    private void Awake()
    {
        sim = GetComponent<SimulationProperties>();
        subspaces = GetComponent<SchoolSubSpacesBucket>();
        teacherPoolController = GetComponent<TeacherPool>();
        inPlaceClassrooms = new List<Classroom>(subspaces.classrooms);
        schedular = GetComponent<SchoolDaySchedular>();
    }

    private void Start()
    {
        schedular.ScheduleClasses();
        schoolDateTime = new DateTime(2020, 1, 1, 8, 00, 00);
        healthStats = FindObjectOfType<HealthStats>();
        Invoke(nameof(teacherPoolController.AllocateOrpahanedTeachers), 5.0f);
        healthParameters = FindObjectOfType<GeneralHealthParamaters>();
        PauseSim();
    }

    private void Update()
    {
        OscillateClassSessions();  
    }

    private void StartSchoolDay()
    {
        sim.SetSessionActivityMinTime(sim.sessionActivityMinTime);
        schoolDay = true;
        SetClassesInSessionStatus(true);
        StartClasses();
        healthStats.CollectAgents();
        healthStats.PopulateAgentLists();
    }

    public void OscillateClassSessions()
    {
        //day is over
        if (currentState == ClassroomState.dayIsOver) { return; } 
        //end school day
        if (currentPeriodIndex == schedular.classTimes.Count - 1)
        {
            EndSchoolDay();
            schedular.classTimes = new List<int>();
            currentState = ClassroomState.dayIsOver;
        }
        //end period
        if (currentPeriodIndex % 2 == 0)
        {
            if (schoolTime > schedular.classTimes[currentPeriodIndex])
            {
                previousState = currentState;
                currentState = ClassroomState.onBreak;
                if (currentState != previousState )
                {
                    currentPeriodIndex++;
                    SetClassesInSessionStatus(false);
                    ReplaceClassTeachers();
                    //Debug.Log("increasing current Period Index");
                    EndClasses();
                    if (classlabPairList != null)
                    {
                        SendClassesBackFromLabs();
                    }
                }
            }
        }
        //start period
        else if (currentPeriodIndex % 2 != 0)
        {
            if (schoolTime > schedular.classTimes[currentPeriodIndex])
            {
                previousState = currentState;
                currentState = ClassroomState.inSession;
                if (currentState != previousState)
                {
                    SetClassesInSessionStatus(true);
                    currentPeriodIndex++;
                    SendClassesToLabs();
                    StartClasses();
                }
            }
        }

    }

    private void EndSchoolDay()
    {
        schoolDay = false;
        SetClassesInSessionStatus(false);
        foreach (EgressPoint stairs in subspaces.staircases)
        {
            stairs.RecallClasses(sim.cooldownClassExit);
        }
    }

    private void StartClasses()
    {
        foreach (Classroom classroom in inPlaceClassrooms)
        {
            classroom.StartClass();
        }
    }

    private void EndClasses()
    {
        foreach (Classroom classroom in inPlaceClassrooms)
        {
            classroom.EndClass();
        }
    }

    void SetClassesInSessionStatus(bool status)
    {
        foreach (Classroom classroom in subspaces.classrooms)
        {
            classroom.SetClassInSessionStatus(status);
        }
        foreach (Lab lab in subspaces.labs)
        {
            lab.SetClassInSessionStatus(status);
        }
        classInSession = status;
    }

    void TimeStep()
    {
        schoolTime++;
        schoolDateTime += new TimeSpan(0, 1, 0);
    }

    void SendClassesToLabs()
    {
        Debug.Log($"sending classes to labs");
        if (schoolDay == false) { return; }
        foreach (Lab lab in subspaces.labs)
        {
            SendRandomClassToLab(lab);
        }
    }

    void SendRandomClassToLab(Lab lab)
    {
        if (lab.IsLabEmpty())
        {
            int randomIndex = UnityEngine.Random.Range(0, inPlaceClassrooms.Count);
            Classroom selectedClass = inPlaceClassrooms[randomIndex];
            //record the selceted class
            classlabPairList.Add(new ClassLabPair(selectedClass, lab));
            lab.SetCurrentOriginalClass(selectedClass);
            //have the class send the students to the lab
            selectedClass.SendClassToLab(lab);
            inPlaceClassrooms.Remove(selectedClass);
            lab.StartLab();
            Debug.Log($"Sending {selectedClass} to {lab.name}");
        }
    }

    void SendClassesBackFromLabs()
    {
        foreach (ClassLabPair classLabPair in classlabPairList.ToArray())
        {
            ReturnClassFromLab(classLabPair);
            classlabPairList.Remove(classLabPair);
        }
    }

    void ReturnClassFromLab(ClassLabPair classLabPair)
    {
        classLabPair.lab.EndLab(classLabPair.classroom);
        inPlaceClassrooms.Add(classLabPair.classroom);
        classLabPair.classroom.RecieveStudents();
    }

    void ReplaceClassTeachers()
    {
        //Debug.Log("calling replace teachers");
        teacherPoolController.ShuffleSchoolTeachers();
        teacherspool = teacherPoolController.GetSchoolTeachers();
        foreach (TeacherAI teacher in teacherspool)
        {
            teacher.SetInClassroomto(false);
            teacher.ClearClassRoom();
        }

        for (int i = 0; i < subspaces.classrooms.Length; i++)
        {
            teacherspool[i].AssignClassRoom(subspaces.classrooms[i]);
            teacherspool[i].SetInClassroomto(true);
        }

        foreach (TeacherAI teacher in teacherspool)
        {
            if (teacher.IsInClassroom())
            {
                teacher.GetComponent<TeacherNavigation>().GoToClassRoom();
            }
            else
            {
                teacher.GetComponent<TeacherNavigation>().GoToTeachersRoom();
            }
        }
    }


    public void PauseSim()
    {
        Debug.Log($"pausing Sim");
        Time.timeScale = 0f;
    }

    public void StartSim()
    {
        Time.timeScale = 1f;
        StartSchoolDay();
        sim.SetWalkingSpeed();
        SetHealthParameters();
    }

    public void SetHealthParameters()
    {
        healthParameters.InfectdSelectedStudents();
        healthParameters.InfectSelectedTeachers();
        healthParameters.SetMaskForAgents();
        healthParameters.SetAirExchangeRateForSpaces();
    }

    public void ResumeSim()
    {
        Time.timeScale = 1f;
    }

    /*===============================================
     * Debugging
     * ==============================================
     */
    /*
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
    */
}
