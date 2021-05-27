namespace SES.School
{
    public abstract class SSchoolBaseState
    {
        public bool resumed = false;
        public abstract void EnterState(SchoolDayProgressionController progressionController);
        public abstract void Update(SchoolDayProgressionController progressionController);

    }
}
