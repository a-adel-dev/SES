using UnityEngine;


namespace SES.School
{
    public class SSimOver : SSchoolBaseState
    {
        public override void EnterState(SchoolDayProgressionController progressionController)
        {
            Debug.Log("SimOver");
            progressionController.timeRecorder.ResetSchoolTime();
        }

        public override void Update(SchoolDayProgressionController progressionController)
        {
            
        }
    }
}