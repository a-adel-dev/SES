using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActivityType { Breathing, Talking, LoudTalking}

public class Health : MonoBehaviour
{

    public bool infected  = false;
    public ActivityType activity { get; private set; } = ActivityType.Breathing;
    float maskFactor = 1f;
    float breathingFlowRate;
    float numberDensity;
    float dropletVolume;
    Droplet droplet;
    SchoolManager schoolManager;
    float timeStep;
    float timer = 0f;
    SpaceHealth currentSpace;

    GeneralHealthParamaters healthParamaters;

    void Start()
    {
        healthParamaters = FindObjectOfType<GeneralHealthParamaters>();
        float criticalRadiusInmL = healthParamaters.criticalRadius / 1000;
        dropletVolume = Mathf.PI * Mathf.Pow(criticalRadiusInmL, 3f);
        schoolManager = FindObjectOfType<SchoolManager>();
        timeStep = schoolManager.simTimeScale;
        SetBreathingRate();
        SetNumberDensity();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeStep)
        {
            timer -= timeStep;
            Debug.Log(Breathe().ToString());
        }
    }

    public float Breathe()
    {
        if (!infected)
        {
            return 0f;
        }

        return breathingFlowRate * maskFactor * numberDensity * dropletVolume * healthParamaters.viralLoad;
        
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
            numberDensity = healthParamaters.avarageNaturalDropletConentration;
        }
        else if (activity == ActivityType.Talking)
        {
            numberDensity = healthParamaters.avarageTalkingDropletConcentration;
        }
        else if (activity == ActivityType.LoudTalking)
        {
            numberDensity = healthParamaters.avarageShoutingDropletConcentration;
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

    public void SetMaskFactor(float factor)
    {
        maskFactor = factor;
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
}
