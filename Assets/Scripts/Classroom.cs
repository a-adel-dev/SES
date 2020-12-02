
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classroom : MonoBehaviour
{
    //classroom objects
    public GameObject board;
    List<Spot> lockers = new List<Spot>();
    List<Spot> boardSpots = new List<Spot>();
    List<Spot> desks = new List<Spot>();
    List<AI> classroomPupils = new List<AI>();
    List<AI> pupilsInClass = new List<AI>();
    Vector3 boardDirection;

    //classroom variables
    List<int> classStructureTimes = new List<int>();
    bool activity = false;
    int classTime = 0;
    int sectionTime = 0;
    bool classInSession = false;

    //school variables
    int periodTime;
    SchoolManager schoolManager;
    float timeStep;
    int sessionActivityMinTime;


    //internal class code management
    bool spawned = false;
    float timer = 0f;
    int activeSection = 0;
    bool boardActivity = false;
    bool groupActivity = false;
    


    //exposed variables
    [SerializeField] GameObject pupilPrefab;
    [SerializeField] GameObject teacherPrefab;


    //temp variables
    public bool move = false;

    private void Awake()
    {
        schoolManager = FindObjectOfType<SchoolManager>();
        periodTime = schoolManager.GetPeriodTime();
        timeStep = schoolManager.GetTimeStep();
        sessionActivityMinTime = schoolManager.GetSessionActivityMinTime();   
    }

    private void Start()
    {

    }

    private void Update()
    {
        classInSession = schoolManager.classInSession;
        SpawnPupils();
        IncreaseClassTime();
        RunClass();

    }

    private void IncreaseClassTime()
    {
        timer += Time.deltaTime;
        if (timer >= timeStep)
        {
            timer -= timeStep;
            if (classInSession)
            {
                classTime++;
                sectionTime++;
            }
        } 
    }

    private void SpawnPupils()
    {
        int counter = 0; // to record student names
        if (spawned)
            return;
        foreach (Spot deskspot in desks)
        {
            Vector3 deskPosition = deskspot.gameObject.transform.position;
            GameObject pupil = Instantiate(pupilPrefab,
                        deskPosition,
                        Quaternion.identity) as GameObject;

            pupil.GetComponent<AI>().SetOriginalPosition(new Vector3(deskPosition.x, 0, deskPosition.z));
            classroomPupils.Add(pupil.GetComponent<AI>());
            pupilsInClass.Add(pupil.GetComponent<AI>());
            pupil.name = "pupil " + counter.ToString();
            counter++;
        }
        spawned = true;
        ShuffleClassroomPupils();        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Board"))
        {
            board = other.gameObject;
            boardDirection = (board.transform.position - transform.position );
        }
        else if (other.CompareTag("Locker"))
        {
            lockers.Add(other.GetComponent<Spot>());
        }
        else if (other.CompareTag("BoardSpot"))
        {
            boardSpots.Add(other.GetComponent<Spot>());
        }
        else if (other.CompareTag("Desk"))
        {
            desks.Add(other.GetComponent<Spot>());
        }
    }

    private void ShuffleClassroomPupils()
    {
        int listLength = pupilsInClass.Count;
        int random;
        AI temp;
        while (--listLength > 0)
        {
            random = UnityEngine.Random.Range(0, listLength);
            temp = pupilsInClass[random];
            pupilsInClass[random] = pupilsInClass[listLength];
            pupilsInClass[listLength] = temp;  
        }
    }

    private List<Spot> ShuffleSpots(List<Spot> spots)
    {
        int listLength = spots.Count;
        int random;
        Spot temp;
        while (--listLength > 0)
        {
            random = UnityEngine.Random.Range(0, listLength);
            temp = spots[random];
            spots[random] = spots[listLength];
            spots[listLength] = temp;
        }
        return spots;
    }

    public void GetPupilOutofClassroom(AI pupil)
    {
        pupilsInClass.Remove(pupil);
    }

    private List<int> StructureAClass()
    {
        int classSections = Random.Range(2, 8); // generate random class sections
        List<int> randomPartitions = new List<int>(); //a list to hold the partition numbers

        for (int i = 1; i < classSections+1; i++)
        {
            randomPartitions.Add(Random.Range(1, 10));
        }

        int totalOfRandomPartitions = 0;

        for (int i = 0; i < randomPartitions.Count; i++)
        {
            totalOfRandomPartitions += randomPartitions[i];
        }

        for (int i = 0; i < randomPartitions.Count; i++)
        {
            classStructureTimes.Add(Mathf.FloorToInt(periodTime * (randomPartitions[i] / (float) totalOfRandomPartitions)));   
        }
        return classStructureTimes;
    }

    public void ResetClassStructure()
    {
        classStructureTimes = new List<int>();
        //Debug.Log("class structure destroyed!");
    }

    public void RunClass()
    {
        if (!classInSession || classStructureTimes == null)
            return;
        if (sectionTime < classStructureTimes[activeSection])
        {
            //Debug.Log("class section no. " + (activeSection + 1).ToString());
            //there is a logic bug here as activities can not set to be on during the first section
        }
        else if (sectionTime >= classStructureTimes[activeSection])
        {
            sectionTime -= classStructureTimes[activeSection];
            activeSection += 1;
            
            SetActivityStatus();
        }
    }

    private void SetActivityStatus()
    {
        if (classStructureTimes[activeSection] < sessionActivityMinTime)
        {
            activity = false;
            foreach (AI pupil in classroomPupils)
            {
                pupil.SetBusyTo(false);
            }
        }
        else
        {
            activity = true;
            foreach (AI pupil in classroomPupils)
            {
                pupil.SetBusyTo(true);
            }
            StartCoroutine(BoardActivity());
        }
    }

    public void StartClass()
    {
        StructureAClass();
        //Debug.Log("class structured!");
    }

    public void EndClass()
    {
        if (activeSection > classStructureTimes.Count - 1)
        {
            classInSession = false;
            activeSection = 0;
            Debug.Log("class is over!");
        }
        ResetClassStructure();
        foreach (AI pupil in classroomPupils)
        {
            pupil.SetBusyTo(false);
        }
        classTime = 0;
    }


    IEnumerator BoardActivity()
    {
        boardSpots = ShuffleSpots(boardSpots);
        int randomIndex = Random.Range(1, boardSpots.Count);
        for (int i = 0; i < randomIndex; i++)
        {
            pupilsInClass[i].AssignSpot(boardSpots[i]);
            boardSpots[i].FillSpot(pupilsInClass[i]);
            pupilsInClass[i].SetDestination(boardSpots[i].transform.position);
        }
        yield return new WaitForSecondsRealtime((classStructureTimes[activeSection]-2) * timeStep);
        for (int i = 0; i < randomIndex; i++)
        {
            var spot  = pupilsInClass[i].ReleaseSpot();
            spot.ClearSpot();
            pupilsInClass[i].BackToDesk();
        }
    }




}
