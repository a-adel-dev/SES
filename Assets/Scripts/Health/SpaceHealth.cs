using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceHealth : MonoBehaviour
{
    float spaceVolume;
    public bool outdoor = false;
    public float Concentration;
    public float airExchangeRate = 3f;
    float EffectiveAirExchangeRate;
    List<Health> agentsInSpace = new List<Health>();
    float timer = 0f;
    SchoolManager schoolManager;
    float timeStep;
    float spaceTime = 0f;



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

    private void RunTime()
    {
        timer += Time.deltaTime;
        if (timer >= timeStep)
        {
            timer -= timeStep;
            spaceTime += 1f;
            foreach (Health agent  in agentsInSpace)
            {
                Concentration +=  agent.Breathe()/(EffectiveAirExchangeRate * spaceVolume);
            }
        }
    }
}
