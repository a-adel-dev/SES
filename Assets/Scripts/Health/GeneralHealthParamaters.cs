using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GeneralHealthParamaters : MonoBehaviour
{
    [Header("General")]
    [Tooltip("The critical radius after which larger radius are not counted in the sim Cᵣ(μm)")] 
    public float criticalRadius = 2.6f;
    [Tooltip("the concentration of suspended pathogen to the infection rate Cᵢ(infection quanta)")]
    public float viralInfectivity = .1f;


    [Header("Breathing flow")]
    [Tooltip("breathing flow rate assumed for mouth breathing Q_b (m3/h)")]
    public float normalBreathingFlowRate  = 0.5f;
    [Tooltip("breathing flow rate assumed for talking Q_b (m3/h)")]
    public float talkingBreathingFlowRate  = 0.75f;
    [Tooltip("breathing flow rate assumed for shouting (m3/h)")]
    public float LoudtalkingBreathingFlowRate  = 1.0f;

    [Tooltip("Breathing droplet concentration (cm-3)")]
    public float avarageNaturalDropletConentration = 0.1f;
    [Tooltip("Talking droplet concentration (cm-3)")]
    public float avarageTalkingDropletConcentration = 0.3f;
    [Tooltip("Shouting droplet concentration (cm-3)")]
    public float avarageShoutingDropletConcentration = 0.9f;

    [Tooltip("concentration of virions in the droplets (copies/liquid volume)")]
    public float viralLoad = 10E11f;

    public float initialAirExchangeRate = 3f;

    private void Start()
    {
        foreach (SpaceHealth space in FindObjectsOfType<SpaceHealth>())
        {
            space.SetAirExhangeRate(initialAirExchangeRate);
        }
    }


}
