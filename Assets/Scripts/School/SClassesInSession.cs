using UnityEngine;
using SES.Core;

namespace SES.School
{
    public class SClassesInSession : SSchoolBaseState 
    {

        short sessionTimer = 1;
        public short sessionLength { get; set; } = 40;
        public float timeStep { get; set; } = 0.5f;
        float timer = 0f;

        public override void EnterState(SchoolDayProgressionController progressionController)
        {
            Debug.Log($"Classes In session");
        }

        public override void Update(SchoolDayProgressionController progressionController)
        {
            PassTime();
            
            if (sessionTimer >= sessionLength)
            {
                sessionTimer = 0;
                progressionController.TransitionToState(progressionController.breakTime);
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
