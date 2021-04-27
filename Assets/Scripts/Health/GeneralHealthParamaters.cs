using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GeneralHealthParamaters : MonoBehaviour
{
    [Header("General")]
    [Tooltip("The critical radius after which larger radius are not counted in the sim Cᵣ(μm)")] 
    public float criticalRadius = 2.6f;
    [Tooltip("the concentration of suspended pathogen to the infection rate Cᵢ(infection quanta)")]
    public float viralInfectivity = 0.1f;
    


    [Header("Breathing flow")]
    [Tooltip("Breathing flow rate assumed for mouth breathing Q_b (m3/h)")]
    public float normalBreathingFlowRate  = 0.5f;
    [Tooltip("Breathing flow rate assumed for talking Q_b (m3/h)")]
    public float talkingBreathingFlowRate  = 0.75f;
    [Tooltip("Breathing flow rate assumed for shouting (m3/h)")]
    public float LoudtalkingBreathingFlowRate  = 1.0f;

    [Header("Breathing droplet concentration")]
    [Tooltip("Breathing droplet concentration (cm-3)")]
    public float avarageNaturalDropletConentration = 0.1f;
    [Tooltip("Talking droplet concentration (cm-3)")]
    public float avarageTalkingDropletConcentration = 0.3f;
    [Tooltip("Shouting droplet concentration (cm-3)")]
    public float avarageShoutingDropletConcentration = 0.9f;

    [Header("Virion parameters")]
    [Tooltip("Concentration of virions in the droplets (copies/liquid volume)")]
    public float viralLoad = 10E11f;


    [Header("Short range infection parameters")]
    [Tooltip("The jet entrainment coefficient which is typically fall in the range for the turbulent jet 0.1 - 0.15 (α)")]
    public float jetEntrainmentCoefficient = 0.1f;
    [Tooltip("Mouth area (cm^2")]
    public float mouthArea = 2f;


    [Header("Space parameters")]
    public float initialAirExchangeRate = 3f;


    [Header("Mask Parameters")]
    public float n95MaskValue = 0.05f;
    public float surgicalMaskValue = 0.15f;
    public float clothMaskValue = 0.8f;

    [Header("Visualization Parameters")]
    [Tooltip("how fast a space is considerred to be totally contaminated")]
    public float spaceInfectionThreshold = .001f;




    private void Start()
    {
        foreach (SpaceHealth space in FindObjectsOfType<SpaceHealth>())
        {
            space.SetAirExhangeRate(initialAirExchangeRate);
        }
    }


}
