using System.Collections;
using UnityEngine;

namespace SES.School
{
    public class SSchoolPaused : SSchoolBaseState
    {
        public override void EnterState(SchoolDayProgressionController progressionController)
        {
            //pause classes and everything
            progressionController.PauseClasses();
            progressionController.SchoolState = "Paused";
        }

        public override void Update(SchoolDayProgressionController progressionController)
        {

        }
    }
}