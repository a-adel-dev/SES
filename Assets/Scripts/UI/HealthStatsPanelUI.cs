using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthStatsPanelUI : MonoBehaviour
{
    [SerializeField] Text numAgentsText;
    [SerializeField] Text numInfectedText;
    [SerializeField] Text percentInfectedText;
    int numAgents;
    int numInfected;

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
        numInfected = healthStats.GetNumInfected();
        numAgentsText.text = numAgents.ToString();
        numInfectedText.text = numInfected.ToString();
        float percentageInfected = (float)numInfected / numAgents;
        percentInfectedText.text = String.Format("{0:P2}", percentageInfected);

    }
}
