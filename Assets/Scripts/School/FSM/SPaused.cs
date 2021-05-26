using System.Collections;
using UnityEngine;

namespace SES.School
{
    public class SPaused : SSchoolBaseState
    {
        public override void EnterState(SchoolScheduler progressionController)
        {
            //pause classes and everything
            progressionController.PauseClasses();
            progressionController.SchoolState = "Paused";
        }

        public override void Update(SchoolScheduler progressionController)
        {

        }
    }
}