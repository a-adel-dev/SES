using UnityEngine;
using System;
using SES.Core;

namespace SES.Health
{
    public class AgentHealth : MonoBehaviour
    {


        public HealthCondition healthCondition = HealthCondition.healthy;
        public ActivityType activity { get; private set; } = ActivityType.Breathing;
        public DateTime schoolDate = new DateTime(); 

        float maskFactor = 1f;
        float breathingFlowRate;
        float numberDensity;
        float dropletVolume;
        SpaceHealth currentSpace;
        float infectionQuanta = 0f;
        float infectiuosQuanta = 0f;
        float shortRangeInfectionQuanta = 0f;
        DateTime contagiousTime;
        bool updatedHealthStats = false;


        ShortRangeInfector shortRangeInfector;
        GeneralHealthParamaters healthParamaters;


        void Start()
        {
            healthParamaters = FindObjectOfType<GeneralHealthParamaters>();
            float criticalRadiusInM = healthParamaters.criticalRadius * 1E-6f;
            dropletVolume = Mathf.PI * Mathf.Pow(criticalRadiusInM, 3f);
            SetBreathingRate();
            SetNumberDensity();
            shortRangeInfector = transform.GetChild(0).GetComponent<ShortRangeInfector>();
        }


        public void SetSchoolDate(DateTime dt)
        {
            schoolDate = dt;
        }

        public float Breathe()
        {
            //if healthy, breathe in , don't return infectionquanta
            if (healthCondition == HealthCondition.healthy || healthCondition == HealthCondition.infected)
            {
                infectionQuanta = breathingFlowRate * (currentSpace.GetComponent<SpaceHealth>().concentration +
                    shortRangeInfectionQuanta) * healthParamaters.viralInfectivity * maskFactor;
                return 0f;
            }

            infectiuosQuanta = (breathingFlowRate * maskFactor * numberDensity * dropletVolume * healthParamaters.viralLoad) * 3.48E+14f / 60f;
            return infectiuosQuanta;
        }

        public void SetActivityType(ActivityType type)
        {
            activity = type;
            SetBreathingRate();
            SetNumberDensity();
        }

        private void SetNumberDensity()
        {
            if (activity == ActivityType.Breathing)
            {
                numberDensity = healthParamaters.avarageNaturalDropletConentration * 1E-6f;
            }
            else if (activity == ActivityType.Talking)
            {
                numberDensity = healthParamaters.avarageTalkingDropletConcentration * 1E-6f;
            }
            else if (activity == ActivityType.LoudTalking)
            {
                numberDensity = healthParamaters.avarageShoutingDropletConcentration * 1E-6f;
            }
        }

        private void SetBreathingRate()
        {
            if (activity == ActivityType.Breathing)
            {
                //Debug.Log($"Setting Flow Rate to {healthParamaters.normalBreathingFlowRate}");
                breathingFlowRate = healthParamaters.normalBreathingFlowRate / 60f;
            }
            else if (activity == ActivityType.Talking)
            {
                breathingFlowRate = healthParamaters.talkingBreathingFlowRate / 60f;
            }
            else if (activity == ActivityType.LoudTalking)
            {
                breathingFlowRate = healthParamaters.LoudtalkingBreathingFlowRate / 60f;
            }
        }

        /*
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<SpaceHealth>())
            {
                currentSpace = other.GetComponent<SpaceHealth>();
            }
        }
        */
        public void SetCurrentSpace(SpaceHealth space)
        {
            currentSpace = space;
        }

        public bool IsInfected()
        {
            return healthCondition == HealthCondition.infected;
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
            Debug.Log($"adding to contagious");
            healthCondition = HealthCondition.contagious;
            shortRangeInfector.gameObject.SetActive(true);
            GeneralHealthParamaters.numContagious++;
        }

        public void ExposeAgent()
        {
            Debug.Log($"adding to infected");
            healthCondition = HealthCondition.infected;
            GeneralHealthParamaters.numInfected++;
            contagiousTime = new DateTime();
            contagiousTime = schoolDate + healthParamaters.timeBeforeContagious;
        }

        public float GetInfectionQuanta()
        {
            return infectionQuanta;
        }

        public void SetMaskFactor(MaskFactor _maskFactor)
        {
            switch (_maskFactor)
            {
                case MaskFactor.none:
                    maskFactor = 1f;
                    break;
                case MaskFactor.cloth:
                    maskFactor = healthParamaters.clothMaskValue;
                    break;
                case MaskFactor.surgical:
                    maskFactor = healthParamaters.surgicalMaskValue;
                    break;
                case MaskFactor.N95:
                    maskFactor = healthParamaters.n95MaskValue;
                    break;
                default:
                    maskFactor = 1f;
                    break;
            }
        }

        public void SetMaskFactor(float factor)
        {
            maskFactor = factor;
        }

        public float GetMaskFactor()
        {
            return maskFactor;
        }

        void TimeStep()
        {
            if (healthCondition == HealthCondition.infected && updatedHealthStats == false && schoolDate >= contagiousTime)
            {
                healthCondition = HealthCondition.contagious;
                Debug.Log(String.Format("{0:hh mm ss} contagious time, {1:hh mm ss} schooldate", contagiousTime, schoolDate));
                GeneralHealthParamaters.numContagious++;
                GeneralHealthParamaters.numInfected--;
                updatedHealthStats = true;
            }
        }
    }
}
