using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SES.Core;

namespace SES.Health
{
    public class SpaceHealth : MonoBehaviour
    {
        [Tooltip("The volume of Space in m^3")]
        public float spaceVolume;
        [Tooltip("Is space outdoor?")]
        public bool outdoor = false;
        [Tooltip("Viral Concentration in space quanta/m^3")]
        public float concentration;
        [Tooltip("Concentration when infected individuals leave space")]
        public float currentConcentration;
        [Tooltip("Air Exchange rate /hour")]
        public float airExchangeRate = 3f;
        [Tooltip("Air Exchange rate /minute")]
        float EffectiveAirExchangeRate;
        /// <summary>
        /// List of agents in the current space
        /// </summary>
        List<AgentHealth> agentsInSpace = new List<AgentHealth>();
        /// <summary>
        /// wheather there is infected individuals in space
        /// </summary>
        bool infectorsPresent = false;

        // Start is called before the first frame update
        void Start()
        {
            ComputeSpaceVolume();
        }

        private void ComputeSpaceVolume()
        {
            Collider volume = GetComponent<Collider>();
            Collider[] colliders = GetComponentsInChildren<Collider>();
            Bounds bounds = new Bounds(transform.position, Vector3.zero);
            foreach (Collider nextCollider in colliders)
            {
                bounds.Encapsulate(nextCollider.bounds);
            }
            spaceVolume = bounds.size[0] * bounds.size[1] * bounds.size[2];
            //Debug.Log($"{gameObject.name} space volume: {spaceVolume}");
        }

        // Update is called once per frame
        void Update()
        {
            infectorsPresent = IsInfectorsPresent();

            //Debug.Log($"concentration: {concentration}, current concentration: {currentConcentration}");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<AgentHealth>())
            {
                agentsInSpace.Add(other.GetComponent<AgentHealth>());
                other.GetComponent<AgentHealth>().SetCurrentSpace(this);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<AgentHealth>())
            {
                agentsInSpace.Remove(other.GetComponent<AgentHealth>());
            }
        }

        public void SetAirExhangeRate(float ACH)
        {
            airExchangeRate = ACH;
            EffectiveAirExchangeRate = airExchangeRate / 60f;
        }

        public float GetAirExhangeRate()
        {
            return airExchangeRate;
        }

        private void IncreaseSpaceInfectionConcentration()
        {
            if (outdoor)
            { return; }

            foreach (AgentHealth agent in agentsInSpace)
            {
                concentration += agent.Breathe() / (EffectiveAirExchangeRate * spaceVolume);
            }
        }

        private bool IsInfectorsPresent()
        {
            foreach (AgentHealth agent in agentsInSpace)
            {
                if (agent.IsInfected())
                {
                    return true;
                }
            }

            return false;
        }

        private void DissipateConcentration()
        {
            if (outdoor)
            { return; }
            if (infectorsPresent)
            {
                currentConcentration = concentration;
                return;
            }
            else
            {
                concentration = Mathf.Max(0f, concentration - (currentConcentration * EffectiveAirExchangeRate));
            }
        }

        public void TimeStep()
        {
            //Debug.Log($"TimeStep");
            IncreaseSpaceInfectionConcentration();
            DissipateConcentration();
            AttemptInfection();
        }

        void AttemptInfection()
        {
            if (Mathf.Abs(concentration) <= Mathf.Epsilon) { return; }
            foreach (AgentHealth agent in agentsInSpace)
            {
                float threshold = Random.Range(0f, 10f);//should be (0,100)
                                                        //Debug.Log($"{threshold} against {agent.GetInfectionQuanta()}");
                if (agent.healthCondition == HealthCondition.healthy && agent.GetInfectionQuanta() > threshold)
                {
                    agent.ExposeAgent();
                    Debug.Log($"{agent.gameObject.name} was exposed");
                }
            }
        }

        public int GetNumAgents()
        {
            return agentsInSpace.Count;
        }
    }
}
