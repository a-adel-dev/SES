﻿using UnityEngine;


namespace SES.School
{
    public class SSimOver : SSchoolBaseState
    {
        public override void EnterState(SchoolScheduler progressionController)
        {
            progressionController.SchoolState = "Simulation is over";
        }

        public override void Update(SchoolScheduler progressionController)
        {
            
        }
    }
}