using SES.Core;

namespace SES.Spaces.Classroom
{
    public class SClassroomEmpty : SClassroomBaseState
    {
        public override void EnterState(ClassroomProgressionControl schedular)
        {
            //set classroom status to empty
            schedular.ReleaseTeacher();
            schedular.Subspaces.ResetDesks();
            schedular.Subspaces.ResetLockers();
        }

        public override void Update(ClassroomProgressionControl schedular)
        {

        }
    }
}