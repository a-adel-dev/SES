namespace SES.School
{
    public abstract class SSchoolBaseState
    {
        public bool resumed = false;
        public abstract void EnterState(SchoolScheduler progressionController);
        public abstract void Update(SchoolScheduler progressionController);

    }
}
