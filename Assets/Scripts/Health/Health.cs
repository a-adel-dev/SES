using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActivityType { Breathing, Talking, LoudTalking}
public enum MaskFactor { none, cloth, surgical , N95}

public class Health : MonoBehaviour
{

    public bool infected  = false;
    public ActivityType activity { get; private set; } = ActivityType.Breathing;
    float maskFactor = 1f;
    float breathingFlowRate;
    float numberDensity;
    float dropletVolume;
    float timeStep;
    float timer = 0f;
    SpaceHealth currentSpace;
    float infectionQuanta = 0f;
    float infectiuosQuanta = 0f;
    float shortRangeInfectionQuanta = 0f;

    ShortRangeInfector shortRangeInfector;
    SchoolManager schoolManager;
    GeneralHealthParamaters healthParamaters;

    void Start()
    {
        healthParamaters = FindObjectOfType<GeneralHealthParamaters>();
        float criticalRadiusInM = healthParamaters.criticalRadius * 1E-6f;
        dropletVolume = Mathf.PI * Mathf.Pow(criticalRadiusInM, 3f);
        schoolManager = FindObjectOfType<SchoolManager>();
        timeStep = schoolManager.simTimeScale;
        SetBreathingRate();
        SetNumberDensity();
        shortRangeInfector = transform.GetChild(0).GetComponent<ShortRangeInfector>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeStep)
        {
            timer -= timeStep;
            //Debug.Log(Breathe().ToString());
        }
        
    }

    public float Breathe()
    {
        if (!infected)
        {
            infectionQuanta = breathingFlowRate  * (currentSpace.GetComponent<SpaceHealth>().concentration +
                shortRangeInfectionQuanta) * healthParamaters.viralInfectivity * maskFactor;
            return 0f;
        }
        infectiuosQuanta = (breathingFlowRate * maskFactor * numberDensity * dropletVolume * healthParamaters.viralLoad) * 3.48E+14f / 60f;
        return  infectiuosQuanta ;
    }

    public void SetActivityType (ActivityType type)
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
        return infected;
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
        infected = true;
        shortRangeInfector.gameObject.SetActive(true);
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


    
}
