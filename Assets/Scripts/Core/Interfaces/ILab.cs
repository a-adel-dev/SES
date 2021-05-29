using UnityEngine;

namespace SES.Core
{
    public interface ILab : ISpace
    {
        void EndLab(IClassroom classroom);
        Vector3 Entrance { get; }
        void ReceiveStudent(IStudentAI student);
        void StudentExitLab(IStudentAI student);
    }
}
