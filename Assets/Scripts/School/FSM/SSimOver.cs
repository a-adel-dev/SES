using UnityEngine;


namespace SES.School
{
    public class SSimOver : SSchoolBaseState
    {
        public override void EnterState(SchoolDayProgressionController progressionController)
        {
            progressionController.SchoolState = "Simulation is over";
        }

        public override void Update(SchoolDayProgressionController progressionController)
        {
            
        }
    }
}