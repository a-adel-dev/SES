using UnityEngine;
using System;
using SES.Core;

namespace SES.Health
{
    public class AgentHealth : MonoBehaviour, IAgentHealth
    {
        public HealthCondition HealthCondition { get; set; } = HealthCondition.healthy;
        public ActivityType Activity { get; set; } = ActivityType.Breathing;

        [SerializeField] float maskFactor = 1f;
        float breathingFlowRate;
        float numberDensity;
        public SpaceHealth CurrentSpace { get; set; }
        float infectionQuanta = 0f;
        float shortRangeInfectionQuanta = 0f;
        DateTime contagiousTime;
        bool updatedHealthStats = false;
        float timer = 0f;
        float numberofBreathsPerMinute = 20f;

        private void Update()
        {
            PassTime();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<SpaceHealth>())
            {
                CurrentSpace = other.GetComponent<SpaceHealth>();
            }
        }

        public float Breathe()
        {
            float infectiousQuanta = 0f;
            //if healthy, breathe in , don't return infectionquanta
            if (HealthCondition == HealthCondition.healthy)
            {
                //go through all the radii up to the critical radius (integrate) with an accuracy of 0.1micron
                for (int i = 1; i < (int)SimulationDefaults.CriticalRadius * 10; i++)
                {
                    infectionQuanta = breathingFlowRate * (CurrentSpace.GetComponent<SpaceHealth>().Concentration +
                    shortRangeInfectionQuanta) * SimulationDefaults.ViralInfectivity * maskFactor * numberofBreathsPerMinute;
                }
                return 0f;
            }

            else if (HealthCondition == HealthCondition.infected)
            {
                return 0f;
            }

            else if (HealthCondition == HealthCondition.contagious)
            {
                infectiousQuanta = 0f;
                //go through all the radii up to the critical radius (integrate) with an accuracy of 0.1micron
                for (int i = 1; i < (int)SimulationDefaults.CriticalRadius * 10; i++)
                {
                    float radiusInMeters = (float)i / 10 * 1E-6f;
                    infectiousQuanta += breathingFlowRate * maskFactor * numberDensity * Mathf.PI * radiusInMeters * radiusInMeters * radiusInMeters *
                              SimulationDefaults.ViralLoad * numberofBreathsPerMinute;
                }
            }
            return infectiousQuanta;
        }

        public void SetActivityType(ActivityType type)
        {
            Activity = type;
            SetBreathingRate();
            SetNumberDensity();
        }

        private void SetNumberDensity()
        {
            if (Activity == ActivityType.Breathing)
            {
                numberDensity = SimulationDefaults.AvarageNaturalDropletConentration * 1E-6f;
            }
            else if (Activity == ActivityType.Talking)
            {
                numberDensity = SimulationDefaults.AvarageTalkingDropletConcentration * 1E-6f;
            }
            else if (Activity == ActivityType.LoudTalking)
            {
                numberDensity = SimulationDefaults.AvarageShoutingDropletConcentration * 1E-6f;
            }
            else if (Activity == ActivityType.Paused)
            {
                numberDensity = 0f;
            }
        }

        private void SetBreathingRate()
        {
            if (Activity == ActivityType.Breathing)
            {
                //Debug.Log($"Setting Flow Rate to {healthParamaters.normalBreathingFlowRate}");
                breathingFlowRate = SimulationDefaults.NormalBreathingFlowRate / 60f;
            }
            else if (Activity == ActivityType.Talking)
            {
                breathingFlowRate = SimulationDefaults.TalkingBreathingFlowRate / 60f;
            }
            else if (Activity == ActivityType.LoudTalking)
            {
                breathingFlowRate = SimulationDefaults.LoudtalkingBreathingFlowRate / 60f;
            }
            else if (Activity == ActivityType.Paused)
            {
                breathingFlowRate = 0f;
            }
        }

        public bool IsInfected()
        {
            return HealthCondition == HealthCondition.infected;
        }

        public bool IsContagious()
        {
            return HealthCondition == HealthCondition.contagious;
        }

        public void SetShortRangeInfectionQuanta(float quanta)
        {
            shortRangeInfectionQuanta = quanta;
        }

        public void ResetShortRangeInfectionQuanta()
        {
            shortRangeInfectionQuanta = 0;
        }

        public void InfectAgent()
        {
            HealthCondition = HealthCondition.contagious;
            //Debug.Log($"{gameObject.name} has become contagious!");
            transform.GetChild(0).gameObject.SetActive(true);
            GeneralHealthParamaters.NumContagious++;
        }

        public void ExposeAgent()
        {
            HealthCondition = HealthCondition.infected;
            GeneralHealthParamaters.NumInfected++;
            contagiousTime = new DateTime();
            contagiousTime = DateTimeRecorder.SchoolTime + SimulationDefaults.TimeBeforeContagious;
        }

        public float GetInfectionQuanta()
        {
            return infectionQuanta;
        }

        public void SetMaskFactor(MaskFactor _maskFactor)
        {
            maskFactor = _maskFactor switch
            {
                MaskFactor.none => 1f,
                MaskFactor.cloth => SimulationDefaults.ClothMaskValue,
                MaskFactor.surgical => SimulationDefaults.SurgicalMaskValue,
                MaskFactor.N95 => SimulationDefaults.N95MaskValue,
                _ => 1f,
            };
        }

        public void SetMaskFactor(float factor)
        {
            maskFactor = factor;
        }

        public float GetMaskFactor()
        {
            return maskFactor;
        }

        void UpdateHealth()
        {
            if (HealthCondition == HealthCondition.infected && updatedHealthStats == false && DateTimeRecorder.SchoolTime >= contagiousTime)
            {
                InfectAgent();
                GeneralHealthParamaters.NumInfected--;
                updatedHealthStats = true;
            }
        }

        private void PassTime()
        {
            timer += Time.deltaTime;
            if (timer >= SimulationParameters.TimeStep)
            {
                timer -= SimulationParameters.TimeStep;
                UpdateHealth();
            }
        }
    }
}
