using UnityEngine;
using SES.Core;

namespace SES.Spaces.Classroom
{
    public class SClassroomOnBreak : SClassroomBaseState
    {
        public override void EnterState(ClassroomProgressionControl schedular)
        {
            Debug.Log($"{schedular.gameObject.name} is going on a break");
            Debug.Log($"{schedular.studentsBucket}"); //.studentsCurrentlyInSpace
            if (schedular.studentsBucket.studentsCurrentlyInSpace != null)
            {
                foreach (IStudentAI student in schedular.studentsBucket.studentsCurrentlyInSpace)
                {
                    student.BreakTime();
                }
            } 
        }

        public override void Update(ClassroomProgressionControl schedular)
        {

        }
    }
}