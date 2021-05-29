using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SES.Core
{
    public interface IStudentAI : IAI
    {
        void StartClass();
        void StartActivity();
        void SetStoppingDistance(float distance);
        void BreakTime();
        void TransitStudent();
        void ResetDay();
        void BackToDesk();
        void LookAtBoard();
        void SetSpawnLocation();
        bool IsFree();
    }
}
