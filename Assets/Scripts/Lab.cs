using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab : MonoBehaviour
{
    //Lab Objects
    [HideInInspector]
    public GameObject board;
    List<Spot> lockers = new List<Spot>();
    List<Spot> availableLockers = new List<Spot>();
    List<Spot> boardSpots = new List<Spot>();
    List<Spot> desks = new List<Spot>();
    List<AI> labPupils = new List<AI>();
    List<AI> pupilsInLab = new List<AI>();

    //lab variables
    int labTime = 0;
    bool classesInsession = false;
    bool labEmpty = true;

    //school variables
    int periodTime;
    SchoolManager schoolManager;
    float timeStep;

    // Start is called before the first frame update
    void Start()
    {
        schoolManager = FindObjectOfType<SchoolManager>();
        periodTime = schoolManager.GetPeriodTime();
        timeStep = schoolManager.GetTimeStep();

    }

    // Update is called once per frame
    void Update()
    {
        classesInsession = schoolManager.classInSession;
        //RunLabTimer();
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

    public void SetlabEmptyTo(bool status)
    {
        labEmpty = status;
    }

    public bool IsLabEmpty()
    {
        return labEmpty;
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
            Debug.LogError("locker is not in lab!");
        }
    }

    public void AddToPupilsInLab(AI agent)
    {
        if (!pupilsInLab.Contains(agent))
        {
            pupilsInLab.Add(agent);
        }
    }

    public void RemoveFromLab(AI agent)
    {
        if (pupilsInLab.Contains(agent))
        {
            pupilsInLab.Remove(agent);
        }
    }

    public void Enterlab(AI pupil)
    {
        labPupils.Add(pupil);
        //TODO: make a function to determine wheather the pupils are in the lab 
    }

    public void AssignLabPosition(AI pupil)
    {
        //assign each student to a lab position
        foreach (Spot desk in desks)
        {
            if(desk.ISpotAvailable())
            {
                pupil.AssignLabPosition(desk.transform.position);
                desk.FillSpot(pupil);
            }
        }
    }

    /*====================================
     * Lab Main Methods
     * ===================================
     */

    public void RunLab()
    {
        //TODO: implement RunLab
        //update clearto go status

    }

    public void StartLab()
    {
        //TODO: implement StartLab
        SetlabEmptyTo(false);
        foreach (AI pupil in labPupils)
        {
            pupil.SetBusyTo(false);
        }
        /*
        while (labEmpty == false)
        {
            UpdateClearToGo();
        }
        */
    }

    public void EndLab(Classroom classroom)
    {
        //TODO: implement EndLab
        //clear desk spots
        
        foreach (Spot desk in desks)
        {
            desk.ClearSpot();
        }
        foreach (AI pupil in labPupils)
        {
            //clear students labspots
            pupil.ClearCurrentLab();
            pupil.SetStudentStatusTo(AIStatus.inClass);
            pupil.SetCurrentClass(classroom);
        }
        foreach (AI pupil in pupilsInLab)
        {
            pupil.BackToDesk();
            //add queue condition
        }
    }

    IEnumerator UpdateClearToGo()
    {
        if (pupilsInLab.Count > 0)
        {
            foreach (AI pupil in pupilsInLab)
            {
                pupil.CheckClearence();
            }
        }
        yield return new WaitForSeconds(10f * timeStep);
    }

}
