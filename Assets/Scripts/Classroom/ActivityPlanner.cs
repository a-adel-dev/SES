using System.Collections;
using UnityEngine;
using System.Collections.Generic;


public class ActivityPlanner : MonoBehaviour
{
    bool activitiesEnabled = false;
    ClassroomPeriodSchedular classSchedular;
    int sessionActivityMinTime;
    ClassroomStudentsBucket studentsBucket;
    BoardActivity boardActivity;
    GroupActivity groupActivity;

    void Awake()
    {
        classSchedular = GetComponent<ClassroomPeriodSchedular>();
        studentsBucket = GetComponent<ClassroomStudentsBucket>();
        boardActivity = GetComponent<BoardActivity>();
        groupActivity = GetComponent<GroupActivity>();
    }

    public void SetActivityStatus()
    {
        if (!activitiesEnabled) { return; }
        if (classSchedular.GetClassStructureTimes()[classSchedular.activeSectionIndex] < sessionActivityMinTime)
        {
            foreach (AI pupil in studentsBucket.GetPupilsInClass())
            {
                pupil.SetBusyTo(false);
            }
        }
        else
        {
            foreach (AI pupil in studentsBucket.GetPupilsInClass())
            {
                pupil.SetBusyTo(true);
            }

            StartActivity();
        }
    }

    private void StartActivity()
    {
        if (Random.Range(0f, 1) < 0.5f)
        {
            StartCoroutine(boardActivity.StartBoardActivity());
        }
        else
        {
            StartCoroutine(groupActivity.StartGroupActivity());
        }
    }

    public void EndActivity()
    {
        foreach (AI pupil in studentsBucket.GetPupilsInClass())
        {
            pupil.ResetPupil();
        }
    }
    
    public void EnableActivities()
    {
        activitiesEnabled = true;
    }

    public void SetActivityMinTime(int time)
    {
        sessionActivityMinTime = time;
    }
}
