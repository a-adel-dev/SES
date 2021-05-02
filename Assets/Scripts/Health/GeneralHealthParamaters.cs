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


    [Header("Health Settings")]
    public int numStudentInfected = 0;
    public int numTeachersInfected = 0;
    public MaskFactor studentsMasks = MaskFactor.none;
    public MaskFactor teachersMasks = MaskFactor.none;
    public List<float> globalAirControl = new List<float>();
    public int airControlSettings = 3;

    HealthStats healthstats;
    SpaceHealth[] spaces;




    private void Start()
    {
        healthstats = FindObjectOfType<HealthStats>();
        foreach (SpaceHealth space in FindObjectsOfType<SpaceHealth>())
        {
            space.SetAirExhangeRate(initialAirExchangeRate);
        }
        PopulateAirControlList();
        spaces = FindObjectsOfType<SpaceHealth>();

        //InfectdSelectedStudents();
        //InfectSelectedTeachers();
    }

    private void PopulateAirControlList()
    {
        globalAirControl.Add(0.12f);
        globalAirControl.Add(0.23f);
        globalAirControl.Add(0.85f);
        globalAirControl.Add(0.90f);
        globalAirControl.Add(2.16f);
        globalAirControl.Add(7.92f);
    }

    public void SetAirExchangeRateForSpaces()
    {
        foreach (SpaceHealth space in spaces)
        {
            space.airExchangeRate = globalAirControl[airControlSettings];
        }
    }

    public void InfectdSelectedStudents()
    {
        Debug.Log($"infecting {numStudentInfected} students");
        var studentsToInfect = GetRandomItemsFromList(healthstats.GetStudents(), numStudentInfected);
        foreach (Health student in studentsToInfect)
        {
            Debug.Log($"infecting {student.gameObject.name}");
            student.InfectAgent();
        }
    }

    public void InfectSelectedTeachers()
    {
        Debug.Log($"infecting {numTeachersInfected} teachers");
        var teachersToInfect = GetRandomItemsFromList(healthstats.GetTeachers(), numTeachersInfected);
        foreach (Health teacher in teachersToInfect)
        {
            teacher.InfectAgent();
            Debug.Log($"infecting {teacher.gameObject.name}");
        }
    }

    public void SetMaskForAgents()
    {
        foreach (Health student in healthstats.GetStudents())
        {
            student.SetMaskFactor(studentsMasks);
        }

        foreach (Health teacher in healthstats.GetTeachers())
        {
            teacher.SetMaskFactor(teachersMasks);
        }
    }

    public void SetAirExchangeRate()
    {

    }
    public static List<T> GetRandomItemsFromList<T>(List<T> list, int number)
    {
        // this is the list we're going to remove picked items from
        List<T> tmpList = new List<T>(list);
        // this is the list we're going to move items to
        List<T> newList = new List<T>();

        // make sure tmpList isn't already empty
        while (newList.Count < number && tmpList.Count > 0)
        {
            int index = Random.Range(0, tmpList.Count);
            newList.Add(tmpList[index]);
            tmpList.RemoveAt(index);
        }

        return newList;
    }
}
