﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SES.Core
{
    public interface IClassroom : ISpace
    {

        //void RecieveStudents();
        //void EgressClass(Vector3 position);
        SpotBucket GetClassroomSubspaces();
        SpaceStudentsBucket GetClassStudents();
        void StartClass();
        //void SetSchoolDayState(SchoolDayState schoolDayState);
        void EndClass();
        void PauseClass();
        void ResumeClass();
        void SetActivities(bool activitiesEnabled);
        List<IStudentAI> ReleaseClass();
        bool IsClassEmpty();
    }
}
