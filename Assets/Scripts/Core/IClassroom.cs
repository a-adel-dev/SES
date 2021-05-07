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
        //void RecieveStudents();
        //void EgressClass(Vector3 position);
        
        void StartClass();
        //void SetSchoolDayState(SchoolDayState schoolDayState);
        void EndClass();
    }
}
