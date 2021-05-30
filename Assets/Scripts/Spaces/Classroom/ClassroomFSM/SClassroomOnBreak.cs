using UnityEngine;
using SES.Core;

namespace SES.Spaces.Classroom
{
    public class SClassroomOnBreak : SClassroomBaseState
    {
        public override void EnterState(ClassroomProgressionControl schedular)
        {
            schedular.ReleaseTeacher();
            
            foreach (IStudentAI student in schedular.studentsBucket.GetStudentsInSpace())
            {
                student.BreakTime();
            }
        }

        public override void Update(ClassroomProgressionControl schedular)
        {

        }
    }
}