using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SES.AIControl.FSM
{
    public class SStudentIdle : StudentBaseState
    {
        public override void EnterState(StudentBehaviorControl behaviorControl)
        {
            behaviorControl.PauseAgent();
        }

        public override void OnTriggerEnter(StudentBehaviorControl behaviorControl)
        {

        }

        public override void Update(StudentBehaviorControl behaviorControl)
        {

        }
    }
}
