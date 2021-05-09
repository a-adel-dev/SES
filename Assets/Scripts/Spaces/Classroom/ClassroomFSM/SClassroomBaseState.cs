namespace SES.Spaces.Classroom
{
    public abstract class SClassroomBaseState
    {

        public bool resumed = false;
        public abstract void EnterState(ClassroomPeriodSchedular schedular);
        public abstract void Update(ClassroomPeriodSchedular schedular);
    }
}
