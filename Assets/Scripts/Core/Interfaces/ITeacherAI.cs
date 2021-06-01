namespace SES.Core
{
    public interface ITeacherAI : IAI
    {
        ILab currentLab { get; set; }
        IClassroom currentClass { get; set; }

        bool IsInTeacherroom();
        int GetClassMovementStyle();

        void GoToClassroom();

        void Rest();
        void GoToTeacherroom();
        void ClearCurrentClassroom();
        void ClassroomFree();
        void ClassroomRestricted();

    }
}
