using UnityEngine;
using System.Collections.Generic;

namespace SES.Core
{
    public interface ILab : ISpace
    {
        List<IStudentAI> GetStudentsInLab();
        SpotBucket SubSpaces { get; set; }
        void ReceiveStudent(IStudentAI student);
        void StudentExitLab(IStudentAI student);
        void MarkStudents(List<IStudentAI> students);

        void StartLab();
        List<IStudentAI> ReleaseLabStudents();

        void EndLab();
    }
}
