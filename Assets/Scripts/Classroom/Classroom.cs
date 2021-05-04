using UnityEngine;

public class Classroom : MonoBehaviour
{
    public ClassroomPeriodSchedular classScheduler;
    public ActivityPlanner activityPlanner;
    public ClassroomStudentsBucket studentsBucket;
    public ClassroomsObjectsBucket classroomSubSpaces;
    public classRoomSpawner spawner;
    public ClassroomPupilController pupilController;
    bool classInSession = false;
    bool classEmpty = false;
    bool spawned = false;

    private void Awake()
    {
        classScheduler = GetComponent<ClassroomPeriodSchedular>();
        activityPlanner = GetComponent<ActivityPlanner>();
        studentsBucket = GetComponent<ClassroomStudentsBucket>();
        classroomSubSpaces = GetComponent<ClassroomsObjectsBucket>();
        pupilController = GetComponent<ClassroomPupilController>();
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

    public void EndClass()
    {
        if (classEmpty) { return; }

        classInSession = false;
        //Debug.Log("class is over!");

        classScheduler.ResetClassStructure();
        pupilController.FreePupilsBehavior();

    }

    public bool IsClassEmpty()
    {
        return classEmpty;
    }

    public void StartClass()
    {
        //Debug.Log($"starting class");
        if (classEmpty || !classInSession) { return; } //check if class is Empty to stop doing anything
        classScheduler.StructureAClass(); // structure a new class
        pupilController.ResetPupilBehavior();
        
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

    public void EgressClass(Vector3 exit)
    {
        pupilController.EgressClass(exit);
    }

    public void SetClassInSessionStatus(bool status)
    {
        classInSession = status;
        classScheduler.SetClassInSession(status);
    }
}
