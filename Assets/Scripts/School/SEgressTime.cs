using UnityEngine;


namespace SES.School
{
    public class SEgressTime : SSchoolBaseState
    {
        short sessionLength = 20;
        short sessionTimer = 1;
        public float timeStep = .5f;
        float timer = 0f;
        public override void EnterState(SchoolDayProgressionController progressionController)
        {
            Debug.Log("Egress Time");
        }

        public override void Update(SchoolDayProgressionController progressionController)
        {

            PassTime();
            if (sessionTimer >= sessionLength)
            {
                sessionTimer = 0;
                progressionController.TransitionToState(progressionController.offTime);
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