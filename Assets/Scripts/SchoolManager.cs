using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public struct ClassLabPair
{
    public Classroom classroom;
    public Lab lab;

    public ClassLabPair(Classroom _classroom, Lab _lab)
    {
        classroom = _classroom;
        lab = _lab;
    }
}

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
    List<Classroom>     classrooms = new List<Classroom>(); // all school classrooms
    List<Classroom>     inPlaceClassrooms = new List<Classroom>(); //classrooms with classes in place i.e. not in a lab
    List<ClassLabPair>  classlabPairList = new List<ClassLabPair>();
    List<Bathroom>      bathrooms = new List<Bathroom>();
    List<Corridor>      corridors = new List<Corridor>();
    List<Teachersroom>  teachersrooms = new List<Teachersroom>();
    List<Lab>           labs = new List<Lab>();

    //class global properties
    [HideInInspector]
    public bool classInSession { get; private set; }
    int schoolTime = 0; // exposed for debugging
    bool schoolDay = false;

    //Class internal Properties
    List<int> classTimes = new List<int>();
    float timer = 0f;
    int currentPeriodIndex = 0;
    //NavMeshPath path;
    //pickedClass :class to be moved to physics lab
    

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
        Time.timeScale = 0;
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
    }
    
    /*==========================================
     * School properties getters, setters
     * =========================================
     */
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
    /*=============================================
     * Classroom Management
     * ============================================
     */

    void SendClassesToLabs()
    {
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
            inPlaceClassrooms.Remove(selectedClass);
            //record the selceted class
            classlabPairList.Add(new ClassLabPair(selectedClass, lab));
            lab.SetCurrentOriginalClass(selectedClass);
            //have the class send the students to the lab
            classrooms[randomIndex].SendClassToLab(lab);
            Debug.Log($"Sending {classrooms[randomIndex].name } to {lab.name}");
            lab.StartLab();
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
