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
        GeneralHealthParamaters healthParameters;
        float jetEntrainmentCoefficient;
        float mouthArea;
        float sqrtMouthArea;

        // Start is called before the first frame update
        void Start()
        {
            healthParameters = FindObjectOfType<GeneralHealthParamaters>();
            jetEntrainmentCoefficient = healthParameters.jetEntrainmentCoefficient;
            mouthArea = healthParameters.mouthArea;
            GameObject infectorParent = transform.parent.gameObject;
            infector = infectorParent.GetComponent<AgentHealth>();
            sqrtMouthArea = Mathf.Sqrt(mouthArea);
        }

        void TimeStep()
        {
            IncreaseConcentrationInIndividuals();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<AgentHealth>())
            {
                if (!other.GetComponent<AgentHealth>().IsInfected())
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
                float distance = Vector3.Distance(individual.transform.position, infector.transform.position) * 100f;

                individual.SetShortRangeInfectionQuanta(infector.Breathe() * sqrtMouthArea / (jetEntrainmentCoefficient * distance));
            }
        }
    }
}
