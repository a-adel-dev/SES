
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SES.Core
{
    public interface IAI
    {
        GameObject GetGameObject();

        bool IsTeacher();
        bool IsStudent();
        void AssignOriginalPosition();
        void Idle();
        void ResumeAgent();

        /*
        void SetNearPOI(bool status);
        void AssignSpot(Spot spot);
        void SetControlledTo(bool state);

        void SetCurrentClass(ISpace classroom);
        bool IsInfected();
        */

    }
}
