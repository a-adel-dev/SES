using UnityEngine;


namespace SES.School
{
    public class SBreakTime : SSchoolBaseState
    {

        short sessionTimer = 1;
        public short sessionLength { get; set; } = 20;
        short periodIndex = 0;
        public short numPeriods { get; set; } = 1;
        public float timeStep = .5f;
        float timer = 0f;
        public override void EnterState(SchoolDayProgressionController progressionController)
        {
            Debug.Log($"BreakTime");
        }

        public override void Update(SchoolDayProgressionController progressionController)
        {
            PassTime();
            if (sessionTimer >= sessionLength)
            {
                sessionTimer = 0;
                periodIndex++;
                if (periodIndex == numPeriods)
                {
                    sessionTimer = 0;
                    periodIndex = 0;
                    progressionController.TransitionToState(progressionController.egressTime);
                }
                else
                {
                    progressionController.TransitionToState(progressionController.classesInSession);
                }
            }
        }
        private void PassTime()
        {
            timer += Time.deltaTime;
            if (timer >= timeStep)
            {
                timer -= timeStep;
                Debug.Log(sessionTimer);
                sessionTimer++;
            }
        }
    }
}
