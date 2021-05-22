
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
        void IdleAgent();
        void PauseAgent();
        void ResumeAgent();
        void AssignDesk(Spot spot);
        void NavigateTo(Vector3 location);
        void AssignSpot(Spot spot);
        void ClearSpot();

        /*
        void SetNearPOI(bool status);
        
        void SetControlledTo(bool state);

        void SetCurrentClass(ISpace classroom);
        bool IsInfected();
        */

    }
}
