using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SES.Core
{
    public interface IClassroom : ISpace
    {
        void RequestStatus(IStudentAI student);
        void ReceiveStudent(IStudentAI student);
        void StartClass();
        void EndClass();
        void PauseClass();
        void ResumeClass();
        List<IStudentAI> ReleaseAllClassStudents();
        bool IsClassEmpty();
        void StudentExitClassroom(IStudentAI agent);
        SpotBucket classroomSubSpaces { get; set; }

        Vector3 entrance { get; }

        ITeacherAI Teacher { get; set; }

    }
}
