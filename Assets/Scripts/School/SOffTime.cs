using UnityEngine;



namespace SES.School
{
    public class SOffTime : SSchoolBaseState
    {
        public short simLength { get; set; } = 3;
        short progressionIndex = 1;

        public override void EnterState(SchoolDayProgressionController progressionController)
        {
            Debug.Log($"offtime");
            Debug.Log($"Day {progressionIndex} is over.");
        }

        public override void Update(SchoolDayProgressionController progressionController)
        {
            progressionIndex++;
            if (progressionIndex < simLength)
            {
                progressionController.TransitionToState(progressionController.classesInSession);
            }
            else
            {
                progressionController.TransitionToState(progressionController.simOver);
            }
        }


    }
}