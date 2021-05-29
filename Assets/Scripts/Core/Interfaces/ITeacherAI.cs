namespace SES.Core
{
    public interface ITeacherAI : IAI
    {
        ILab currentLab { get; set; }
        IClassroom currentClass { get; set; }

        bool IsInTeacherroom();

        void GoToClassroom();

        void Rest();
        void GoToTeacherroom();
        void ClearCurrentClassroom();
        void ClassroomFree();
        void ClassroomRestricted();
    }
}
