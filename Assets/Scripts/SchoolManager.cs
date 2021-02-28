using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SchoolManager : MonoBehaviour
{
    //Sim Parameters
    /// <summary>
    /// Scale of the simulation, at 1 every second of realtime is a minute in the sim
    /// for a realtime simulation set to 60
    /// </summary>    
    [Range(0.2f, 60f)]
    public float simTimeScale= 4f;
    [SerializeField] int numPeriods = 4;
    [SerializeField] int periodLength = 40;
    [Range(5, 30)]
    [SerializeField] int sessionActivityMinTime = 8;
    /// <summary>
    /// The waiting time between the exit of classes at the end of school day
    /// </summary>
    [Tooltip("The waiting time between the exit of classes at the end of school day")]
    [SerializeField] int cooldownClassExit = 0;

    //space lists
    List<Classroom>     classrooms = new List<Classroom>(); // all school classrooms
    List<Classroom>     inPlaceClassrooms = new List<Classroom>(); //classrooms with classes in place i.e. not in a lab
    List<ClassLabPair>  classlabPairList = new List<ClassLabPair>();
    List<Bathroom>      bathrooms = new List<Bathroom>();
    List<Corridor>      corridors = new List<Corridor>();
    List<Teachersroom>  teachersrooms = new List<Teachersroom>();
    List<Lab>           labs = new List<Lab>();
    List<EgressPoint>   staircases = new List<EgressPoint>();

    //class global properties
    [HideInInspector]
    public bool classInSession { get; private set; }
    List<TeacherAI> orphandTeachers = new List<TeacherAI>();
    int teacherRoomIndex = 0; //An index to keep trak of which teacher room will be used to assign an orphand teacher to
    int schoolTime = 0; // exposed for debugging
    bool schoolDay = false;

    //Class internal Properties
    List<int> classTimes = new List<int>();
    float timer = 0f;
    int currentPeriodIndex = 0;
    

    private void Awake()
    {
        AllocateSubSpaces();
        inPlaceClassrooms = new List<Classroom>(classrooms);
        ScheduleClasses();
        //path = new NavMeshPath();
    }

    private void Start()
    {
        StartSchoolDay();
    }

    private void Update()
    {
        RunSchoolTimer();
        OssilateClassSessions();
        
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
    }

    private void OssilateClassSessions()
    {
        if (schoolDay == false) { return; }
        if (currentPeriodIndex == classTimes.Count-1)
        {
            EndSchoolDay();
            classTimes = new List<int>();
        }
        if (currentPeriodIndex % 2 == 0)
        {
            if (schoolTime > classTimes[currentPeriodIndex])
            {
                AllocateOrpahanedTeachers();
                classInSession = false;
                currentPeriodIndex++;
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
        else if (currentPeriodIndex % 2 != 0)
        {
            if (schoolTime > classTimes[currentPeriodIndex])
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
        }
        //Debug.Log(classTime);
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
        if(orphandTeachers.Count <= 0) { return; }
        foreach (TeacherAI teacher in orphandTeachers.ToArray())
        {
            if (teacherRoomIndex == teachersrooms.Count)
            {
                teacherRoomIndex = 0;
            }
            teachersrooms[teacherRoomIndex].AddToRoomTeachers(teacher);
            teachersrooms[teacherRoomIndex].AddToClassroomTeachers(teacher);
            //Debug.Log($"Assigning {teacher.gameObject.name} to {teachersrooms[teacherRoomIndex].gameObject.name}"); //
            orphandTeachers.Remove(teacher);
            teacherRoomIndex++;
        }
    }

    /*==========================================
     * School properties getters, setters
     * =========================================
     */
    public int GetPeriodTime()
    {
        return periodLength;
    }

    public int GetSessionActivityMinTime()
    {
        return sessionActivityMinTime;
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
            int randomIndex = Random.Range(0, inPlaceClassrooms.Count);
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

    /*===============================================
     * Debugging
     * ==============================================
     */

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
