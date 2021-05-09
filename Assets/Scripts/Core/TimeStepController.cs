using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SES.Core
{
    public class TimeStepController : MonoBehaviour
    {
        public float timeStep = 2f;
        float timer = 0f;

        private void Start()
        {
            timeStep = SimulationParameters.timeStep;
        }

        void Update()
        {
            //PassTime();
        }

        private void PassTime()
        {
            timer += Time.deltaTime;
            if (timer >= timeStep)
            {
                timer -= timeStep;
                SendMessage("TimeStep");
            }
        }
    }

}
