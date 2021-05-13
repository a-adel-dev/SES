using SES.Core;

namespace SES.Spaces.Classroom
{
    public class SClassroomIdle : SClassroomBaseState
    {
        public override void EnterState(ClassroomProgressionControl schedular)
        {
            //idle AI
            foreach (IStudentAI student in schedular.studentsBucket.studentsCurrentlyInSpace)
            {
                student.Idle();
            }
        }

        public override void Update(ClassroomProgressionControl schedular)
        {
            
        }
    }
}