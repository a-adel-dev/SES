using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BoardActivity : MonoBehaviour
{
    SchoolManager schoolManager;
    ClassroomStudentsBucket studentsBucket;
    ClassroomsObjectsBucket classroomSubSpaces;
    ClassroomPeriodSchedular classSchedular;
    float timeStep;
    ActivityPlanner planner;

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

    public IEnumerator StartBoardActivity()
    {
        //Debug.Log("board");
        if (studentsBucket.GetPupilsInClass().Count != 0)
        {

            List<Spot> boardSpots = new List<Spot>(classroomSubSpaces.ShuffleBoardSpots());
            int randomIndex = Random.Range(1, boardSpots.Count);
            for (int i = 0; i < randomIndex; i++)
            {
                studentsBucket.GetPupilsInClass()[i].AssignSpot(boardSpots[i]);// wouldn't the same pupil be picked every time? i think we need to shuffle students.
                boardSpots[i].FillSpot(studentsBucket.GetPupilsInClass()[i]);
                studentsBucket.GetPupilsInClass()[i].GuideTo(boardSpots[i].transform.position);
            }
            yield return new WaitForSecondsRealtime((classSchedular.GetClassStructureTimes()[classSchedular.activeSectionIndex] - 2) * timeStep);
            for (int i = 0; i < randomIndex; i++)
            {
                var spot = studentsBucket.GetPupilsInClass()[i].ReleaseSpot();
                spot.ClearSpot();
                studentsBucket.GetPupilsInClass()[i].BackToDesk();
            }
        }
        planner.EndActivity();
    }
}
