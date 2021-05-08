namespace SES.Spaces.Classroom
{
    public abstract class SClassroomBaseState
    {
        public abstract void EnterState(ClassroomPeriodSchedular schedular);
        public abstract void Update(ClassroomPeriodSchedular schedular);
    }
}
