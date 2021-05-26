namespace SES.Spaces.Classroom
{
    public class SClassroomEmpty : SClassroomBaseState
    {
        public override void EnterState(ClassroomProgressionControl schedular)
        {
            schedular.ReleaseTeacher();
        }

        public override void Update(ClassroomProgressionControl schedular)
        {

        }
    }
}