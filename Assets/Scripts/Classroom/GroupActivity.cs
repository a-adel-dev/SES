using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupActivity : MonoBehaviour
{
    SchoolManager schoolManager;
    ClassroomStudentsBucket studentsBucket;
    ClassroomsObjectsBucket classroomSubSpaces;
    ClassroomPeriodSchedular classSchedular;
    float timeStep;
    ActivityPlanner planner;
    [SerializeField] int numSpotsForGroupActivity = 4;
    [SerializeField] float deskGroupActivityCompensationX = 0f;
    [SerializeField] float deskGroupActivityCompensationZ = -0.5f;
    [SerializeField] float minDistanceGroupActivity = 3f;

    // Use this for initialization
    void Awake()
    {
        studentsBucket = GetComponent<ClassroomStudentsBucket>();
        classroomSubSpaces = GetComponent<ClassroomsObjectsBucket>();
        classSchedular = GetComponent<ClassroomPeriodSchedular>();
        schoolManager = FindObjectOfType<SchoolManager>();
        planner = GetComponent<ActivityPlanner>();
    }

    private void Start()
    {
        timeStep = schoolManager.timeStep;
    }

    public IEnumerator StartGroupActivity()
    {
        //Debug.Log("Group");
        if (studentsBucket.GetPupilsInClass().Count != 0)
        {
            List<Spot> selectedDesks = PickSpotsForGroupActivity(1000);
            if (!(selectedDesks == null))
            {
                List<AI> pupilsAvailableforActivity = new List<AI>(studentsBucket.GetPupilsInClass());
                foreach (Spot desk in selectedDesks)
                {
                    List<AI> closestStudents = new List<AI>();
                    int searchIndex = 1;
                    while (closestStudents.Count < studentsBucket.GetPupilsInClass().Count / numSpotsForGroupActivity)
                    {
                        foreach (AI pupil in pupilsAvailableforActivity.ToArray())
                        {
                            if (Vector3.Distance(desk.transform.position, pupil.transform.position) < searchIndex)
                            {
                                closestStudents.Add(pupil);
                                pupilsAvailableforActivity.Remove(pupil);
                                if (closestStudents.Count >= studentsBucket.GetPupilsInClass().Count / numSpotsForGroupActivity)
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
            yield return new WaitForSecondsRealtime((classSchedular.GetClassStructureTimes()[classSchedular.activeSectionIndex] - 2) * timeStep);
            foreach (AI pupil in studentsBucket.GetPupilsInClass())
            {
                pupil.setStoppingDistance(0.3f);
                pupil.BackToDesk();
            }
        }
        planner.EndActivity();
    }

    List<Spot> PickSpotsForGroupActivity(int numTries)
    {
        List<Spot> selectedDesks = new List<Spot>();
        for (int i = 0; i < numTries; i++)
        {
            List<Spot> availableDesks = new List<Spot>(classroomSubSpaces.GetClassroomDesks());

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


}
