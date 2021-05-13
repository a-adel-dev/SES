namespace SES.Spaces.Classroom
{
    public abstract class SClassroomBaseState
    {

        public bool resumed = false;
        public abstract void EnterState(ClassroomProgressionControl schedular);
        public abstract void Update(ClassroomProgressionControl schedular);
    }
}
