using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    List<Health> agentsInSpace = new List<Health>();


    float timer = 0f;
    SchoolManager schoolManager;
    float timeStep;
    float spaceTime = 0f;

    /// <summary>
    /// wheather there is infected individuals in space
    /// </summary>
    bool infectorsPresent = false;
    



    // Start is called before the first frame update
    void Start()
    {
        ComputeSpaceVolume();
        schoolManager = FindObjectOfType<SchoolManager>();
        timeStep = schoolManager.simTimeScale;
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
        RunTime();
        infectorsPresent = IsInfectorsPresent();
        
        //Debug.Log($"concentration: {concentration}, current concentration: {currentConcentration}");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Health>())
        {
            agentsInSpace.Add(other.GetComponent<Health>());
            other.GetComponent<Health>().SetCurrentSpace(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Health>())
        {
            agentsInSpace.Remove(other.GetComponent<Health>());
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

    private void RunTime()
    {
        timer += Time.deltaTime;
        if (timer >= timeStep)
        {
            timer -= timeStep;
            spaceTime += 1f;
            SendMessage("TimeStep");
            
        }
    }

    private void IncreaseSpaceInfectionConcentration()
    {
        if (outdoor)
        { return; }

        foreach (Health agent in agentsInSpace)
        {
            concentration += agent.Breathe() / (EffectiveAirExchangeRate * spaceVolume);
        }
    }

    private bool IsInfectorsPresent()
    {
        foreach (Health agent in agentsInSpace)
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
        if(outdoor)
        { return; }
        if (infectorsPresent)
        {
            currentConcentration = concentration;
            return;
        }
        else
        {
            concentration = Mathf.Max(0f, concentration -( currentConcentration * EffectiveAirExchangeRate));
        }
    }

    void TimeStep()
    {
        //Debug.Log($"TimeStep");
        IncreaseSpaceInfectionConcentration();
        DissipateConcentration();
        AttemptInfection();
    }

    void AttemptInfection()
    {
        if (Mathf.Abs(concentration) <= Mathf.Epsilon) { return; }
        foreach (Health agent in agentsInSpace )
        {
            float threshold = Random.Range(0f, 100f);
            Debug.Log($"{threshold} against {agent.GetInfectionQuanta()}");
            if (!agent.IsInfected() && agent.GetInfectionQuanta() > threshold)
            {
                agent.InfectAgent();
            }
        }
    }

    public int GetNumAgents()
    {
        return agentsInSpace.Count;
    }
}
