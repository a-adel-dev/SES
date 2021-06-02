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
        float dropletVolume;
        public SpaceHealth CurrentSpace { get; set; }
        float infectionQuanta = 0f;
        float infectiuosQuanta = 0f;
        float shortRangeInfectionQuanta = 0f;
        DateTime contagiousTime;
        bool updatedHealthStats = false;
        float timer = 0f;


        void Start()
        {
            float criticalRadiusInM = SimulationDefaults.CriticalRadius * 1E-6f;
            dropletVolume = Mathf.PI * Mathf.Pow(criticalRadiusInM, 3f);
        }

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
            //if healthy, breathe in , don't return infectionquanta
            if (HealthCondition == HealthCondition.healthy || HealthCondition == HealthCondition.infected)
            {
                //Debug.Log($"{CurrentSpace.GetComponent<SpaceHealth>()}");
                infectionQuanta = breathingFlowRate * (CurrentSpace.GetComponent<SpaceHealth>().Concentration +
                    shortRangeInfectionQuanta) * SimulationDefaults.ViralInfectivity * maskFactor;
                return 0f;
            }

            infectiuosQuanta = (breathingFlowRate * maskFactor * numberDensity * dropletVolume * SimulationDefaults.ViralLoad) * 3.48E+14f / 60f;
            return infectiuosQuanta;
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
            Debug.Log($"{gameObject.name} has become contagious!");
            transform.GetChild(0).gameObject.SetActive(true);
            GeneralHealthParamaters.NumContagious++;
        }

        public void ExposeAgent()
        {
            HealthCondition = HealthCondition.infected;
            GeneralHealthParamaters.NumInfected++;
            contagiousTime = new DateTime();
            contagiousTime = DateTimeRecorder.schoolTime + SimulationDefaults.TimeBeforeContagious;
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
            if (HealthCondition == HealthCondition.infected && updatedHealthStats == false && DateTimeRecorder.schoolTime >= contagiousTime)
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
