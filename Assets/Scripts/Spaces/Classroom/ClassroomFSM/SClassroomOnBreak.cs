using UnityEngine;
using SES.Core;

namespace SES.Spaces.Classroom
{
    public class SClassroomOnBreak : SClassroomBaseState
    {
        public override void EnterState(ClassroomProgressionControl schedular)
        {
            //Debug.Log("Class is on a break");
            //Control AI
            foreach (IStudentAI student in schedular.studentsBucket.studentsCurrentlyInSpace)
            {
                student.BreakTime();
            }
        }

        public override void Update(ClassroomProgressionControl schedular)
        {

        }
    }
}