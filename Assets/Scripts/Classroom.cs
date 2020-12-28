
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classroom : MonoBehaviour
{
    //classroom objects
    [HideInInspector]
    public GameObject board;
    List<Spot> lockers = new List<Spot>();
    List<Spot> availableLockers = new List<Spot>();
    List<Spot> boardSpots = new List<Spot>();
    List<Spot> desks = new List<Spot>();
    List<AI> classroomPupils = new List<AI>();
    List<AI> pupilsInClass = new List<AI>();

    //classroom variables
    List<int> classStructureTimes = new List<int>();
    bool activity = false;
    int classTime = 0;
    int sectionTime = 0;
    bool classInSession = false;
    [SerializeField] int numSpotsForGroupActivity = 4;
    [SerializeField] float minDistanceGroupActivity = 3f;
    [SerializeField] float deskGroupActivityCompensationX = 0f;
    [SerializeField] float deskGroupActivityCompensationZ = 0f;
    bool studentsInFile = false; //determines if students will move in a file
    bool classEmpty = false; //indicates if the class is empty

    //school variables
    int periodTime;
    SchoolManager schoolManager;
    float timeStep;
    int sessionActivityMinTime;


    //internal class code management
    bool spawned = false;
    float timer = 0f;
    int activeSectionIndex = 0;
    bool boardActivity = false;
    bool groupActivity = false;
    
    //exposed variables
    [SerializeField] GameObject pupilPrefab;
    [SerializeField] GameObject teacherPrefab;
    


    //temp variables
    public bool move = false;
    public GameObject indicator;
    public bool DebuggingClassTimes;

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
        RunClassroomTimer();
        RunClass();
    }


    /*=================================
     * utility Methods
     *=================================
     */
    private void RunClassroomTimer()
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
            AI pupilAI = pupil.GetComponent<AI>();
            pupilAI.SetStudentStatusTo(AIStatus.inClass);
            pupil.transform.parent = transform;
            pupilAI.SetOriginalPosition(new Vector3(deskPosition.x, 0, deskPosition.z));
            pupilAI.SetMainClassroom(this);
            pupilAI.SetCurrentClass(this);
            classroomPupils.Add(pupilAI);
            pupilsInClass.Add(pupilAI);
            pupil.name = "pupil " + counter.ToString();
            counter++;
        }
        spawned = true;
        ShuffleClassroomPupils();
    }

    private void ShuffleClassroomPupils()
    {
        int listLength = pupilsInClass.Count;
        int random;
        AI temp;
        while (--listLength > 0)
        {
            random = Random.Range(0, listLength);
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

    

    /*==================================
     * Collecting subspaces
     * =================================
     */
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Board"))
        {
            board = other.gameObject;
        }
        else if (other.CompareTag("Locker"))
        {
            lockers.Add(other.GetComponent<Spot>());
            availableLockers.Add(other.GetComponent<Spot>());
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

    /*==================================
     * Class IO
     * =================================
     */
    public void SetInQueueTo(bool status)
    {
        studentsInFile = status;
    }

    public void SetClassEmptyTo(bool status)
    {
        classEmpty = status;
    }

    public Spot GetLocker()
    {
        if (availableLockers.Count <= 0)
        {
            return null;
        }
        else
        {
            Spot randomLocker;
            int randomIndex = Random.Range(0, availableLockers.Count);
            randomLocker = availableLockers[randomIndex];
            availableLockers.Remove(randomLocker);
            return randomLocker;
        }
    }

    public void ReturnLocker(Spot locker)
    {
        if (!availableLockers.Contains(locker) && lockers.Contains(locker))
        {
            availableLockers.Add(locker);
        }
        else
        {
            Debug.LogError("locker is not in class!");
        }
    }

    public bool IsInsideClass(AI pupil)
    {
        return (pupilsInClass.Contains(pupil));
    }

    public void AddToPupilsInClass(AI agent)
    {
        if (!pupilsInClass.Contains(agent))
        {
            pupilsInClass.Add(agent);
        }
    }

    public void RemoveFromClass(AI agent)
    {
        if (pupilsInClass.Contains(agent))
        {
            pupilsInClass.Remove(agent);
        }
    }

    /*====================================
     * Classroom Main Methods
     * ===================================
     */

    private List<int> StructureAClass()
    {
        if (DebuggingClassTimes)
        {
            classStructureTimes.Add(1);
            classStructureTimes.Add(8);
            classStructureTimes.Add(6);
            classStructureTimes.Add(6);
            classStructureTimes.Add(19);
        }
        else
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
        if (!classInSession || classStructureTimes.Count <= 0 || classEmpty)
            return;
        if (sectionTime < classStructureTimes[activeSectionIndex])
        {
            //Debug.Log("class section no. " + (activeSection + 1).ToString());
            //there is a logic bug here as activities can not set to be on during the first section
        }
        else if (sectionTime >= classStructureTimes[activeSectionIndex])
        {
            sectionTime -= classStructureTimes[activeSectionIndex];
            if (activeSectionIndex < classStructureTimes.Count - 1)
            {
                activeSectionIndex++;
                SetActivityStatus();
            }
        }
    }

    private void SetActivityStatus()
    {
        if (classEmpty) { return; }
        if (classStructureTimes[activeSectionIndex] < sessionActivityMinTime)
        {
            activity = false;
            foreach (AI pupil in pupilsInClass)
            {
                pupil.SetBusyTo(false);
            }
        }
        else
        {
            activity = true;
            foreach (AI pupil in pupilsInClass)
            {
                pupil.SetBusyTo(true);
            }

            PickActivity();
        }
    }

    private void PickActivity()
    {
        if (classEmpty) { return; }
        if (Random.Range(0f, 1) < .5f)
        {
            StartCoroutine(BoardActivity());
        }
        else
        {
            StartCoroutine(GroupActivity());
        }
    }

    public void StartClass()
    {
        if (classEmpty) { return; }
        StructureAClass();
        foreach (AI pupil in classroomPupils)
        {
            pupil.RestrictPupil();
            pupil.ResetClearenceChance();
        }
        //Debug.Log("class structured!");
    }

    public void EndClass()
    {
        if (classEmpty) { return; }

        classInSession = false;
        activeSectionIndex = 0;
        Debug.Log("class is over!");

        ResetClassStructure();
        foreach (AI pupil in classroomPupils)
        {
            pupil.SetBusyTo(false);
            pupil.ResetPupil();
        }
        classTime = 0;
        foreach (AI pupil in classroomPupils)
        {
            pupil.ResetPupil();
            pupil.IncreaseClearenceChance();
        }
    }
    
    /*====================================
     * Classroom Activities
     * ===================================
     */
    IEnumerator BoardActivity()
    {
        Debug.Log("board");
        if (pupilsInClass.Count != 0)
        {
            boardSpots = ShuffleSpots(boardSpots);
            int randomIndex = Random.Range(1, boardSpots.Count);
            for (int i = 0; i < randomIndex; i++)
            {
                pupilsInClass[i].AssignSpot(boardSpots[i]);
                boardSpots[i].FillSpot(pupilsInClass[i]);
                pupilsInClass[i].GuideTo(boardSpots[i].transform.position);
            }
            yield return new WaitForSecondsRealtime((classStructureTimes[activeSectionIndex] - 2) * timeStep);
            for (int i = 0; i < randomIndex; i++)
            {
                var spot = pupilsInClass[i].ReleaseSpot();
                spot.ClearSpot();
                pupilsInClass[i].BackToDesk();
            }
        }
        EndActivity();
    }

    private void EndActivity()
    {
        foreach (AI pupil in pupilsInClass)
        {
            pupil.ResetPupil();
        }
    }

    IEnumerator GroupActivity()
    {
        Debug.Log("Group");
        if (pupilsInClass.Count != 0)
        {
            List<Spot> selectedDesks = PickSotsForGroupActivity(1000);
            if (!(selectedDesks == null))
            {
                List<AI> pupilsAvailableforActivity = new List<AI>(pupilsInClass);
                foreach (Spot desk in selectedDesks)
                {
                    //Debug.Log(desk.name);
                    //Instantiate(indicator, desk.transform.position, Quaternion.identity);

                    List<AI> closestStudents = new List<AI>();
                    int searchIndex = 1;
                    while (closestStudents.Count < pupilsInClass.Count / numSpotsForGroupActivity)
                    {
                        foreach (AI pupil in pupilsAvailableforActivity.ToArray())
                        {
                            if (Vector3.Distance(desk.transform.position, pupil.transform.position) < searchIndex)
                            {
                                closestStudents.Add(pupil);
                                pupilsAvailableforActivity.Remove(pupil);
                                if (closestStudents.Count >= pupilsInClass.Count / numSpotsForGroupActivity )
                                {
                                    break;
                                }
                            }
                        }
                        searchIndex++;
                    }
                    searchIndex = 1;
                    foreach (AI pupil in closestStudents)
                    {
                        pupil.setStoppingDistance(.5f);
                        pupil.GuideTo(new Vector3(desk.transform.position.x + deskGroupActivityCompensationX,
                                                            0f,
                                                            desk.transform.position.z + deskGroupActivityCompensationZ));
                    }
                }
                while (pupilsAvailableforActivity.Count > 0)
                {
                    foreach (AI remainingPupil in pupilsAvailableforActivity.ToArray())
                    {
                        float shortestDistance = Mathf.Infinity;
                        Spot nearestGroupDesk = null;
                        foreach (Spot desk in selectedDesks)
                        {
                            if (Vector3.Distance(remainingPupil.transform.position, desk.transform.position) < shortestDistance)
                            {
                                nearestGroupDesk = desk;
                            }
                        }
                        remainingPupil.GuideTo(nearestGroupDesk.transform.position);
                        pupilsAvailableforActivity.Remove(remainingPupil);
                    }
                }

            }



            yield return new WaitForSecondsRealtime((classStructureTimes[activeSectionIndex] - 2) * timeStep);
            foreach (AI pupil in pupilsInClass)
            {
                pupil.setStoppingDistance(0.3f);
                pupil.BackToDesk();
            }
        }
        EndActivity();
    }

    List<Spot> PickSotsForGroupActivity(int numTries)
    {
        List<Spot> selectedDesks = new List<Spot>();
        for (int i = 0; i < numTries; i++)
        {
            List<Spot> availableDesks = new List<Spot>(desks);

            while (selectedDesks.Count < numSpotsForGroupActivity && availableDesks.Count > 0)
            {
                Spot randomDesk = availableDesks[Random.Range(0, availableDesks.Count)];
                bool tooClose = false;
                if (selectedDesks == null)
                {
                    selectedDesks.Add(randomDesk);
                    availableDesks.Remove(randomDesk);
                }
                else
                {
                    tooClose = CompareProximity(randomDesk, selectedDesks);
                    if (!tooClose)
                    {
                        selectedDesks.Add(randomDesk);
                        availableDesks.Remove(randomDesk);
                    }
                    else
                    {
                        availableDesks.Remove(randomDesk);
                    }
                }
            }

            if (selectedDesks.Count >= numSpotsForGroupActivity)
            {
                break;
            }
            else
            {
                selectedDesks.Clear();
            }
            
        }
        if (selectedDesks == null)
        {
            Debug.LogError("Could not find a solution, please reduce space proximity option!");
        }
        return selectedDesks;
    }

    bool CompareProximity(Spot randomDesk, List<Spot> desks)
    //group activity submethod
    {
        bool tooClose = false;
        foreach (Spot desk in desks)
        {
            if (Vector3.Distance(randomDesk.transform.position,
                                    desk.transform.position) < minDistanceGroupActivity)
            {
                tooClose = true;
            }
        }
        return tooClose;
    }


    public void SendClassToLab(Lab lab)
    {
        //foreach of the students 
        foreach (AI pupil in classroomPupils)
        {
            //assign all students to a lab
            pupil.SetCurrentLab(lab);
            //set students status to inLab
            pupil.SetStudentStatusTo(AIStatus.inLab);
        }
        //if inqueue
        //move students in queue to lab
        ////move first student to the lab
        ////set next studetnt to follow first student 
        //// when eachone arrives
        //// have the lab assign him a position
        ////enterLab
        //else
        //move them randomly to the lab
        foreach (AI pupil in pupilsInClass.ToArray())
        {
            pupil.SetBusyTo(true);
            pupil.EnterLab(lab);
            lab.AssignLabPosition(pupil);
            pupil.GoToLab();
            pupilsInClass.Remove(pupil);
        }
        ////guide class students to the lab
        ////when eachone arrives
        ////have the lab assign him a position
        ////enterLab
        //set classEmpty to true
        classEmpty = true;
        //TODO: check the status of out of class pupils

    }

    public void RecieveStudents()
    {
        //setclassEmpty to false
    }

}
