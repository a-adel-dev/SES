using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SchoolManager : MonoBehaviour
{
    public float timeStep= 4f;
    [SerializeField] int sessionActivityMinTime = 8;
    [SerializeField] float childWalkingSpeed = 0.6f;
    [SerializeField] float adultWalkingSpeed = 1.5f;
    public bool halfCapacity = false;
    public bool classroomHalfCapacity = false;
    public int cooldownClassExit = 0;

    List<Classroom>     inPlaceClassrooms = new List<Classroom>(); //classrooms with classes in place i.e. not in a lab
    List<ClassLabPair>  classlabPairList = new List<ClassLabPair>();

    public SchoolSubSpacesBucket subspaces;
    public TeacherPool teacherPoolController;
    public SimulationProperties sim;

    //classroom global properties
    [HideInInspector]
    public bool classInSession { get; private set; }
    public int schoolTime = 0;
    public DateTime dateTime { get; private set; }
    bool schoolDay = false;

    //Class internal Properties
    List<int> classTimes = new List<int>();
    int currentPeriodIndex = 0;
    List<TeacherAI> teacherspool;
    ClassroomState currentState = ClassroomState.inSession;
    ClassroomState previousState = ClassroomState.onBreak;
    HealthStats healthStats;
    GeneralHealthParamaters healthParameters;

    private void Awake()
    {
        subspaces = GetComponent<SchoolSubSpacesBucket>();
        teacherPoolController = GetComponent<TeacherPool>();
        sim = GetComponent<SimulationProperties>();
        inPlaceClassrooms = new List<Classroom>(subspaces.classrooms);
        ScheduleClasses();
    }

    private void Start()
    {
        dateTime = new DateTime(2020, 1, 1, 8, 00, 00);
        healthStats = FindObjectOfType<HealthStats>();
        Invoke(nameof(teacherPoolController.AllocateOrpahanedTeachers), 5.0f);
        healthParameters = FindObjectOfType<GeneralHealthParamaters>();
        PauseSim();
    }

    private void Update()
    {
        OscillateClassSessions();  
    }

    /*==========================================
     * School Main Methods
     * =========================================
     */
    private void StartSchoolDay()
    {
        SetSessionActivityMinTime(sessionActivityMinTime);
        schoolDay = true;
        SetClassesInSessionStatus(true);
        foreach (Classroom classroom in inPlaceClassrooms)
        {
            classroom.StartClass();
        }
        healthStats.CollectAgents();
        healthStats.PopulateAgentLists();
    }

    private void OscillateClassSessions()
    {
        if (currentState == ClassroomState.dayIsOver) { return; }
        if (currentPeriodIndex == classTimes.Count - 1)
        {
            EndSchoolDay();
            classTimes = new List<int>();
            currentState = ClassroomState.dayIsOver;
        }
        if (currentPeriodIndex % 2 == 0)
        {
            if (schoolTime > classTimes[currentPeriodIndex])
            {
                previousState = currentState;
                currentState = ClassroomState.onBreak;
                if (currentState != previousState )
                {
                    currentPeriodIndex++;
                    SetClassesInSessionStatus(false);
                    ReplaceClassTeachers(); 
                    //Debug.Log("increasing current Period Index");
                    foreach (Classroom classroom in inPlaceClassrooms)
                    {
                        classroom.EndClass();
                    }
                    if (classlabPairList != null)
                    {
                        SendClassesBackFromLabs();
                    }
                }
            }
        }
        else if (currentPeriodIndex % 2 != 0)
        {
            if (schoolTime > classTimes[currentPeriodIndex])
            {
                previousState = currentState;
                currentState = ClassroomState.inSession;
                if (currentState != previousState)
                {
                    SetClassesInSessionStatus(true);
                    currentPeriodIndex++;
                    SendClassesToLabs();
                    foreach (Classroom classroom in inPlaceClassrooms)
                    {
                        classroom.StartClass();
                    }
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
            stairs.RecallClasses(cooldownClassExit);
        }

        //Time.timeScale = 0;
    }

    void TimeStep()
    {
        schoolTime++;
        dateTime += new TimeSpan(0, 1, 0);
    }

    private void ScheduleClasses()
    {
        for (int i = 0; i < sim.numPeriods * 2; i++)
        {
            if (i == 0)
            {
                classTimes.Add(40);
                continue;
            }
            else if (i % 2 != 0)
            {
                classTimes.Add(classTimes[i - 1] + (60 - sim.periodLength));
            }
            else if (i % 2 == 0)
            {
                classTimes.Add(classTimes[i - 1] + sim.periodLength);
            }
        }

    }

    /*==========================================
     * School properties getters, setters
     * =========================================
     */
    public void SetSessionActivityMinTime(int time)
    {
        foreach (Classroom classroom in subspaces.classrooms)
        {
            classroom.activityPlanner.SetActivityMinTime(time);
        }
    }

    /*=============================================
     * Classroom Management
     * ============================================
     */

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

    /*===============================================
     * General
     * ==============================================
     */

    public void PauseSim()
    {
        Time.timeScale = 0f;
    }

    public void StartSim()
    {
        Time.timeScale = 1f;
        StartSchoolDay();
        SetWalkingSpeed();
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

    private void SetWalkingSpeed()
    {
        var agents = FindObjectsOfType<Health>();
        foreach (Health agent in agents)
        {
            if (agent.GetComponent<AI>())
            {
                agent.GetComponent<NavMeshAgent>().speed = childWalkingSpeed * (60f / timeStep);
            }
            else
            {
                agent.GetComponent<NavMeshAgent>().speed = adultWalkingSpeed* (60f / timeStep);
            }
        }
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



}
