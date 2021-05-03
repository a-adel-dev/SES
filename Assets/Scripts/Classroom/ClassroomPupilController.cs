using System.Collections;
using UnityEngine;


public class ClassroomPupilController : MonoBehaviour
{
    Classroom classroom;
    void Start()
    {
        classroom = GetComponent<Classroom>();
    }

    public void FreePupilsBehavior()
    {
        foreach (AI pupil in classroom.studentsBucket.GetPupilsInClass())
        {
            pupil.SetBusyTo(false);
        }

        foreach (AI pupil in classroom.studentsBucket.GetClassroomStudents())
        {
            pupil.ResetPupil();
            pupil.IncreaseClearenceChance();
        }
    }

    public void ResetPupilBehavior()
    {
        foreach (AI pupil in classroom.studentsBucket.GetClassroomStudents())
        {
            pupil.RestrictPupil();
            pupil.ResetClearenceChance();
        }
    }

    public void EgressClass(Vector3 exit)
    {
        foreach (AI pupil in classroom.studentsBucket.GetClassroomStudents())
        {
            pupil.SetBusyTo(true);
            pupil.MoveTo(exit);
        }
    }

}
