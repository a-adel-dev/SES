using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortRangeInfector : MonoBehaviour
{
    List<Health> peopleInRange = new List<Health>();
    Health infector;
    GeneralHealthParamaters healthParameters;
    float jetEntrainmentCoefficient;
    float mouthArea;
    float sqrtMouthArea;

    SchoolManager schoolManager;
    float timeStep;
    float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        healthParameters = FindObjectOfType<GeneralHealthParamaters>();
        jetEntrainmentCoefficient = healthParameters.jetEntrainmentCoefficient;
        mouthArea = healthParameters.mouthArea;
        schoolManager = FindObjectOfType<SchoolManager>();
        timeStep = schoolManager.sim.timeStep;
        GameObject infectorParent = transform.parent.gameObject;
        infector = infectorParent.GetComponent<Health>();
        sqrtMouthArea = Mathf.Sqrt(mouthArea);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeStep)
        {
            timer -= timeStep;
            //Debug.Log(Breathe().ToString());
            IncreaseConcentrationInIndividuals();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Health>())
        {
            if (!other.GetComponent<Health>().IsInfected())
            {
                peopleInRange.Add(other.GetComponent<Health>());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Health>())
        {
            peopleInRange.Remove(other.GetComponent<Health>());
            other.GetComponent<Health>().ResetShortRangeInfectionQuanta();
        }
    }

    private void IncreaseConcentrationInIndividuals()
    {
        foreach (Health individual in peopleInRange)
        {
            float distance = Vector3.Distance(individual.transform.position, infector.transform.position)*100f;

            individual.SetShortRangeInfectionQuanta(infector.Breathe() * sqrtMouthArea / ( jetEntrainmentCoefficient * distance));
        }
    }
}
