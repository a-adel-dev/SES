using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ClassroomState { inSession, onBreak, dayIsOver}



public class SchoolManager : MonoBehaviour
{
    //Sim Parameters
    /// <summary>
    /// Scale of the simulation, at 1 every second of realtime is a minute in the sim
    /// for a realtime simulation set to 60
    /// </summary>    
    [Range(0.2f, 60f)]
    public float simTimeScale= 4f;
    int simLength = 14;
    [SerializeField] int numPeriods = 4;
    [SerializeField] int periodLength = 40;
    [SerializeField] bool activitiesEnabled = true;
    [Range(5, 30)]
    [SerializeField] int sessionActivityMinTime = 8;
    [SerializeField] float timeMultiplier = 1f;
    [SerializeField] float childWalkingSpeed = 0.6f;
    [SerializeField] float adultWalkingSpeed = 1.5f;
    float TimeScale;
    public bool halfCapacity = false;
    public bool classroomHalfCapacity = false;
    /// <summary>
    /// The waiting time between the exit of classes at the end of school day
    /// </summary>
    [Tooltip("The waiting time between the exit of classes at the end of school day")]
    public int cooldownClassExit = 0;

    //space lists
    List<Classroom>     classrooms = new List<Classroom>(); // all school classrooms
    List<Classroom>     inPlaceClassrooms = new List<Classroom>(); //classrooms with classes in place i.e. not in a lab
    List<ClassLabPair>  classlabPairList = new List<ClassLabPair>();
    List<Bathroom>      bathrooms = new List<Bathroom>();
    List<Corridor>      corridors = new List<Corridor>();
    List<Teachersroom>  teachersrooms = new List<Teachersroom>();
    List<Lab>           labs = new List<Lab>();
    List<EgressPoint>   staircases = new List<EgressPoint>();
    

    //classroom global properties
    [HideInInspector]
    public bool classInSession { get; private set; }
    List<TeacherAI> orphandTeachers = new List<TeacherAI>();
    int teacherRoomIndex = 0; //An index to keep trak of which teacher room will be used to assign an orphand teacher to


    public int schoolTime = 0;
    public DateTime dateTime { get; private set; }
    
    bool schoolDay = false;

    //Class internal Properties
    List<int> classTimes = new List<int>();
    float timer = 0f;
    int currentPeriodIndex = 0;
    TeacherPool teacherPoolController;
    List<TeacherAI> teacherspool;
    ClassroomState currentState = ClassroomState.inSession;
    ClassroomState previousState = ClassroomState.onBreak;
    HealthStats healthStats;
    GeneralHealthParamaters healthParameters;


    private void Awake()
    {
        AllocateSubSpaces();
        inPlaceClassrooms = new List<Classroom>(classrooms);
        ScheduleClasses();
        teacherPoolController = gameObject.GetComponent(typeof(TeacherPool)) as TeacherPool;
        //path = new NavMeshPath();
        dateTime = new DateTime(2020, 1, 1, 8, 00, 00);
    }

    private void Start()
    {
        healthStats = FindObjectOfType<HealthStats>();
        Invoke("AllocateOrpahanedTeachers", 5.0f);
        healthParameters = FindObjectOfType<GeneralHealthParamaters>();
        PauseSim();

    }

    private void Update()
    {
        //Time.timeScale = timeMultiplier;
        RunSchoolTimer();
        OscillateClassSessions();
        
    }

    /*==========================================
     * School Main Methods
     * =========================================
     */
    private void StartSchoolDay()
    {
        schoolDay = true;
        classInSession = true;
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
                    
                    classInSession = false;
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
                    classInSession = true;
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
        classInSession = false;
        foreach (EgressPoint stairs in staircases)
        {
            stairs.RecallClasses(cooldownClassExit);
        }

        //Time.timeScale = 0;
    }

    private void RunSchoolTimer()
    {
        timer += Time.deltaTime;
        if (timer >= simTimeScale)
        {
            timer -= simTimeScale;
            schoolTime++;
            dateTime += new TimeSpan(0, 1, 0);
            SendMessage("TimeStep");
        }

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
    /*==========================================
     * Collection of subspaces
     * =========================================
     */
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

        var stairsArray = FindObjectsOfType<EgressPoint>();
        foreach (var stairs in stairsArray)
        {
            staircases.Add(stairs);
        }
    }

    void AllocateOrpahanedTeachers()
    {
        
        if(orphandTeachers.Count <= 0) 
        {
            return;
        }
        
        foreach (TeacherAI teacher in orphandTeachers.ToArray())
        {
            if (teacherRoomIndex == teachersrooms.Count)
            {
                teacherRoomIndex = 0;
            }
            teachersrooms[teacherRoomIndex].AddToRoomTeachers(teacher);
            teachersrooms[teacherRoomIndex].AddToClassroomTeachers(teacher);
            teacher.AssignTeachersRoom(teachersrooms[teacherRoomIndex]);
            //Debug.Log($"Assigning {teacher.gameObject.name} to {teachersrooms[teacherRoomIndex].gameObject.name}"); //
            orphandTeachers.Remove(teacher);
            teacherRoomIndex++;
        }
    }

    /*==========================================
     * School properties getters, setters
     * =========================================
     */
    public int GetSimLength()
    {
        return simLength;
    }

    public void SetSimLength(int time)
    {
        simLength = time;
    }

    public int GetNumPeriods()
    {
        return numPeriods;
    }

    public void SetNumPeriods(int num)
    {
        numPeriods = num;
    }

    public int GetNumClasses()
    {
        return classrooms.Count;
    }

    public int GetNumlabs()
    {
        return labs.Count;
    }

    public int GetPeriodLength()
    {
        return periodLength;
    }

    public void SetPeriodLength(int length)
    {
        periodLength = length;
    }

    public int GetSessionActivityMinTime()
    {
        return sessionActivityMinTime;
    }

    public void EnableActivities(bool state)
    {
        activitiesEnabled = state;
    }

    public bool IsActivitiesEnabled()
    {
        return activitiesEnabled;
    }

    public Bathroom GetNearestBathroom(AI pupil)
    {
        Bathroom nearestBathroom = null;
        float distance = Mathf.Infinity;
        Vector3 pupilPos = pupil.transform.position;
        //NavMeshPath path = new NavMeshPath();
        foreach (Bathroom bathroom in bathrooms)
        {
            if (Vector3.Distance(bathroom.transform.position, pupil.transform.position) < distance)
            {
                distance = Vector3.Distance(bathroom.transform.position, pupilPos);
                nearestBathroom = bathroom;
            }
            /*
            //Debug.Log(NavMesh.CalculatePath(pupilPos, bathroom.transform.position, NavMesh.AllAreas, path));
            Vector3 bathroomPos = bathroom.transform.position;
            NavMesh.CalculatePath(pupilPos, bathroomPos, NavMesh.AllAreas, path);
            
            while (!(path.status == NavMeshPathStatus.PathComplete))
            {
                NavMeshHit hit;
                NavMesh.SamplePosition(bathroomPos, out hit, 1, NavMesh.AllAreas);
                bathroomPos = hit.position;
                NavMesh.CalculatePath(pupilPos, bathroomPos, NavMesh.AllAreas, path);
            }
            
            if (PathLength(path) < distance)
            {
                nearestBathroom = bathroom;
                distance = PathLength(path);
                Debug.Log(distance);
            }
            */
        }
        return nearestBathroom;
    }

    float PathLength(NavMeshPath path)
    {
        if (path.corners.Length < 2)
            return 0;

        Vector3 previousCorner = path.corners[0];
        float lengthSoFar = 0.0F;
        int i = 1;
        while (i < path.corners.Length)
        {
            Vector3 currentCorner = path.corners[i];
            lengthSoFar += Vector3.Distance(previousCorner, currentCorner);
            previousCorner = currentCorner;
            i++;
        }
        return lengthSoFar;
    }

    public void AddOrphandTeacher(TeacherAI teacher)
    {
        orphandTeachers.Add(teacher);
    }

    /*=============================================
     * Classroom Management
     * ============================================
     */

    void SendClassesToLabs()
    {
        Debug.Log($"sending classes to labs");
        if (schoolDay == false) { return; }
        foreach (Lab lab in labs)
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

        for (int i = 0; i < classrooms.Count; i++)
        {
            teacherspool[i].AssignClassRoom(classrooms[i]);
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
                agent.GetComponent<NavMeshAgent>().speed = childWalkingSpeed * (60f / simTimeScale);
            }
            else
            {
                agent.GetComponent<NavMeshAgent>().speed = adultWalkingSpeed* (60f / simTimeScale);
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





}
