namespace SES.Core
{
    public interface ITeacherAI : IAI
    {
        void SetInClassroomto(bool v);
        void ClearClassRoom();
        void AssignClassRoom(IClassroom classroomSpace);
        bool IsInClassroom();

        void GoToClassRoom();
        void GoToTeachersRoom();
        void AssignTeachersRoom(ISpace teachersroom);
    }
}
