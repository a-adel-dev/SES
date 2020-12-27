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
        RunLab();
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

    public void SetClassEmptyTo(bool status)
    {
        labEmpty = status;
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
        pupilsInLab.Add(pupil);
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
    }

    public void StartLab()
    {
        //TODO: implement StartLab
    }

    public void EndLab()
    {
        //TODO: implement EndLab
        //clear desk spots
        //clear students labspots
    }

}
