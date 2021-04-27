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
    Classroom currentOriginalClass;
    BoxCollider teachersSpace;

    //lab variables
    int labTime = 0;
    bool classesInSession = false;
    bool labEmpty = true;
    bool started = false; // a bool to enable the function to update cleartogo status

    //school variables
    int periodTime;
    SchoolManager schoolManager;
    float timeStep;

    // Start is called before the first frame update
    void Start()
    {
        schoolManager = FindObjectOfType<SchoolManager>();
        periodTime = schoolManager.GetPeriodTime();
        timeStep = schoolManager.simTimeScale;

    }

    // Update is called once per frame
    void Update()
    {
        classesInSession = schoolManager.classInSession;
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
        else if (other.CompareTag("TeachersArea"))
        {
            teachersSpace = other.GetComponent<BoxCollider>();
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


    public void AddToLabPupils(AI pupil)
    {
        if ( !labPupils.Contains(pupil))
        {
            labPupils.Add(pupil);
        }
    }
    public void RemoveFromLab(AI agent)
    {
        if (pupilsInLab.Contains(agent))
        {
            pupilsInLab.Remove(agent);
        }
    }


    public void AssignLabPosition(AI pupil)
    {
        //assign each student to a lab position
        Spot selectedDesk = null;
        for (int i = 0; i < desks.Count; i++)
        {
            if (desks[i].ISpotAvailable())
            {
                selectedDesk = desks[i];
                selectedDesk.FillSpot(pupil);
                break;
            }
        }

        if (selectedDesk == null)
        {
            Debug.LogError($"Could not find a desk for pupil {pupil.name} in the {this.gameObject.name}");
        }
        else
        {
            pupil.AssignLabPosition(selectedDesk.transform.position);
        }

        //search for a free desk
        //assign desk to pupil
        //return
    }

    /*====================================
     * Lab Main Methods
     * ===================================
     */

    public void RunLab()
    {

        UpdateClearToGo();
        //update clearto go status
    }

    public void StartLab()
    {

        SetlabEmptyTo(false);
        StartCoroutine(ManageBusyStatus());
        InvokeRepeating("UpdateStarted", 10.0f * timeStep, 10f * timeStep);

        /*
        foreach (AI pupil in labPupils)
        {
            pupil.SetBusyTo(false);
        }
        
        while (labEmpty == false)
        {
            UpdateClearToGo();
        }
        */
    }

    IEnumerator ManageBusyStatus()
    {
        yield return new WaitForSeconds(10f * timeStep);
        foreach (AI pupil in labPupils)
        {
            pupil.SetBusyTo(false);
        }
    }

    public void EndLab(Classroom classroom)
    {
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
        labPupils = new List<AI>();
        foreach (AI pupil in pupilsInLab.ToArray())
        {
            pupil.SetBusyTo(true);
            pupil.BackToDesk();
            pupil.EnterClass(currentOriginalClass);
            
            //add queue condition
        }
        pupilsInLab = new List<AI>();
        currentOriginalClass = null;
        SetlabEmptyTo(true);
    }

    void UpdateClearToGo()
    {
        if (started == false && pupilsInLab.Count > 0 )
        {
            
            //Debug.Log($"updating clearto go status");
            started = true;
            foreach (AI pupil in pupilsInLab)
            {
                pupil.ResetPupil();
            }
            started = true;
        }
    }

    void UpdateStarted()
    {
        started = false;
    }



    public void SetCurrentOriginalClass(Classroom classroom)
    {
        currentOriginalClass = classroom;
    }

    public BoxCollider GetTeacherSpace()
    {
        return teachersSpace;
    }

}
