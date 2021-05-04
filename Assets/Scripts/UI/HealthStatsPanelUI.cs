using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthStatsPanelUI : MonoBehaviour
{
    [SerializeField] Text numAgentsText;
    [SerializeField] Text numInfectedText;
    [SerializeField] Text numContagiousText;
    [SerializeField] Text percentInfectedText;
    [SerializeField] Text percentContagiousText;
    int numAgents;
    int numInfected;
    int numContagious;

    HealthStats healthStats;
    
    
    // Start is called before the first frame update
    void Start()
    {
        healthStats = FindObjectOfType<HealthStats>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        numAgents = healthStats.GetNumAgents();
        numInfected = GeneralHealthParamaters.numInfected;
        numContagious = GeneralHealthParamaters.numContagious;
        numAgentsText.text = numAgents.ToString();
        numContagiousText.text = numContagious.ToString();
        numInfectedText.text = numInfected.ToString();
        float percentageInfected = (float)numInfected / numAgents;
        float percentageContagious = (float)numContagious / numAgents;
        percentInfectedText.text = String.Format("{0:P2}", percentageInfected);
        percentContagiousText.text = String.Format("{0:P2}", percentageContagious);

    }
}
