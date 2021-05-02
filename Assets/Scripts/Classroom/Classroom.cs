using UnityEngine;

public class Classroom : MonoBehaviour
{
    ClassroomPeriodSchedular classScheduler;
    ActivityPlanner activityPlanner;
    ClassroomStudentsBucket studentsBucket;
    ClassroomsObjectsBucket classroomSubSpaces;
    classRoomSpawner spawner;
    bool classInSession = false;
    bool classEmpty = false;
    bool spawned = false;

    private void Awake()
    {
        classScheduler = GetComponent<ClassroomPeriodSchedular>();
        activityPlanner = GetComponent<ActivityPlanner>();
        studentsBucket = GetComponent<ClassroomStudentsBucket>();
        classroomSubSpaces = GetComponent<ClassroomsObjectsBucket>();
        spawner = GetComponent<classRoomSpawner>();
        
    }

    private void Start()
    {
        SpawnAgents();
    }


    public void SpawnAgents()
    {
        if(spawned) { return; }
        spawner.SpawnAgents();
        spawned = true;
    }

    public void SetActivityMinTime(int time)
    {
        activityPlanner.SetActivityMinTime(time);
    }

    public GameObject GetClassBoard()
    {
        return classroomSubSpaces.GetClassBoard();
    }

    public void AddToPupilsInClass(AI pupil)
    {
        studentsBucket.AddToPupilsInClass(pupil);
    }

    public void RemoveFromClass(AI pupil)
    {
        studentsBucket.RemoveFromClass(pupil);
    }

    public Spot GetLocker()
    {
        return classroomSubSpaces.GetLocker();
    }

    public void ReturnLocker(Spot locker)
    {
        classroomSubSpaces.ReturnLocker(locker);
    }

    public BoxCollider GetTeacherSpace()
    {
        return classroomSubSpaces.GetTeacherSpace();
    }

    public Transform GetTeacherSpawnerPos()
    {
        return spawner.GetTeacherSpawnerPos();
    }

    public bool IsClassEmpty()
    {
        return classEmpty;
    }

    public void StartClass()
    {
        if (classEmpty || !classInSession) { return; } //check if class is Empty to stop doing anything
        classScheduler.StructureAClass(); // structure a new class
        foreach (AI pupil in studentsBucket.GetClassroomStudents())
        {
            pupil.RestrictPupil();
            pupil.ResetClearenceChance();
        }
    }

    public void EndClass()
    {
        if (classEmpty) { return; }

        classInSession = false;
        //Debug.Log("class is over!");

        classScheduler.ResetClassStructure();
        foreach (AI pupil in studentsBucket.GetPupilsInClass())
        {
            pupil.SetBusyTo(false);
        }
        foreach (AI pupil in studentsBucket.GetClassroomStudents())
        {
            pupil.ResetPupil();
            pupil.IncreaseClearenceChance();
        }
    }
    
    public void EnableActivities()
    {
        activityPlanner.EnableActivities();
    }

    public void SendClassToLab(Lab lab)
    {
        //foreach of the students 
        foreach (AI pupil in studentsBucket.GetClassroomStudents())
        {
            //assign all students to a lab
            pupil.SetCurrentLab(lab);
            //set students status to inLab
            pupil.SetStudentStatusTo(AIStatus.inLab);
            pupil.AssignLab(lab);
            lab.AssignLabPosition(pupil);
        }
        foreach (AI pupil in studentsBucket.GetPupilsInClass())
        {
            pupil.SetBusyTo(true);
            pupil.GoToLab();
            pupil.Enterlab(lab);
        }
        studentsBucket.ClearStudentsInClass();
        classEmpty = true;
        //TODO: check the status of out of class pupils
    }

    public void RecieveStudents()
    {
        classEmpty = false;
    }

    public void SendClassOutOfFloor(Vector3 exit)
    {
        //Debug.Log($"{this.name} is exiting the building");
        foreach (AI pupil in studentsBucket.GetClassroomStudents())
        {
            pupil.SetBusyTo(true);
            pupil.MoveTo(exit);
        }
    }

    public void SetClassInSessionStatus(bool status)
    {
        classInSession = status;
        classScheduler.SetClassInSession(status);
    }
}
