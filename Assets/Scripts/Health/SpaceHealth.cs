using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SES.Core;

namespace SES.Health
{
    public class SpaceHealth : MonoBehaviour
    {
        public float SpaceVolume { get; set; }
        [SerializeField] bool outdoor = false;
        public bool Outdoor { get => outdoor; set => outdoor = value; }
        public float Concentration { get; set; }
        /// <summary>
        /// Concentration when infected individuals leave space
        /// </summary>
        public float CurrentConcentration { get; set; }
        public float AirExchangeRate { get; set; } = 3f;
        float EffectiveAirExchangeRate;
        /// <summary>
        /// List of agents in the current space
        /// </summary>
        List<AgentHealth> agentsInSpace = new List<AgentHealth>();
        /// <summary>
        /// wheather there is infected individuals in space
        /// </summary>
        bool infectorsPresent = false;
        float timer = 0f;

        // Start is called before the first frame update
        void Start()
        {
            ComputeSpaceVolume();
            EffectiveAirExchangeRate = AirExchangeRate / 60f;

        }

        void Update()
        {
            PassTime();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<AgentHealth>())
            {
                agentsInSpace.Add(other.GetComponent<AgentHealth>());
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
            AirExchangeRate = ACH;
            EffectiveAirExchangeRate = AirExchangeRate / 60f;
        }

        public float GetAirExhangeRate()
        {
            return AirExchangeRate;
        }

        private void IncreaseSpaceInfectionConcentration()
        {
            if (Outdoor)
            { return; }

            foreach (AgentHealth agent in agentsInSpace)
            {
                Concentration += agent.Breathe() / (EffectiveAirExchangeRate * SpaceVolume);
            }
        }

        private bool IsInfectorsPresent()
        {
            foreach (AgentHealth agent in agentsInSpace)
            {
                if (agent.IsContagious())
                {
                    return true;
                }
            }

            return false;
        }

        private void DissipateConcentration()
        {
            if (Outdoor)
            { return; }
            if (infectorsPresent)
            {
                CurrentConcentration = Concentration;
                return;
            }
            else
            {
                Concentration = Mathf.Max(0f, Concentration - (CurrentConcentration * EffectiveAirExchangeRate));
            }
        }

        void AttemptInfection()
        {
            if (Mathf.Abs(Concentration) <= Mathf.Epsilon) { return; }
            foreach (AgentHealth agent in agentsInSpace)
            {
                float threshold = Random.Range(0f, 10f);//should be (0,100)
                                                        //Debug.Log($"{threshold} against {agent.GetInfectionQuanta()}");
                if (agent.HealthCondition == HealthCondition.healthy && agent.GetInfectionQuanta() > threshold)
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

        private void PassTime()
        {
            timer += Time.deltaTime;
            if (timer >= SimulationParameters.TimeStep)
            {
                timer -= SimulationParameters.TimeStep;
                infectorsPresent = IsInfectorsPresent();
                AttemptInfection();
                IncreaseSpaceInfectionConcentration();
                DissipateConcentration();

            }
        }
        private void ComputeSpaceVolume()
        {
            Collider volume = GetComponent<Collider>();
            SpaceVolume = volume.bounds.size[0] * volume.bounds.size[1] * volume.bounds.size[2];
        }
    }
}
