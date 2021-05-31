using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SES.Core;

namespace SES.Health
{
    public class ShortRangeInfector : MonoBehaviour
    {
        List<AgentHealth> peopleInRange = new List<AgentHealth>();
        AgentHealth infector;
        float sqrtMouthArea;
        float timer = 0f;

        void Start()
        {
            GameObject infectorParent = transform.parent.gameObject;
            infector = infectorParent.GetComponent<AgentHealth>();
            sqrtMouthArea = Mathf.Sqrt(SimulationDefaults.MouthArea);
        }

        private void Update()
        {
            PassTime();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<AgentHealth>())
            {
                if (other.GetComponent<AgentHealth>().HealthCondition == HealthCondition.healthy) 
                {
                    peopleInRange.Add(other.GetComponent<AgentHealth>());
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<AgentHealth>())
            {
                peopleInRange.Remove(other.GetComponent<AgentHealth>());
                other.GetComponent<AgentHealth>().ResetShortRangeInfectionQuanta();
            }
        }

        private void IncreaseConcentrationInIndividuals()
        {
            foreach (AgentHealth individual in peopleInRange)
            {
                if (individual.CurrentSpace == infector.CurrentSpace)
                {
                    float distance = Vector3.Distance(individual.transform.position, infector.transform.position) * 100f;
                    individual.SetShortRangeInfectionQuanta(infector.Breathe() * sqrtMouthArea / (SimulationDefaults.JetEntrainmentCoefficient * distance));
                }
            }
        }

        private void PassTime()
        {
            timer += Time.deltaTime;
            if (timer >= SimulationParameters.TimeStep)
            {
                timer -= SimulationParameters.TimeStep;
                IncreaseConcentrationInIndividuals();
            }
        }
    }
}
