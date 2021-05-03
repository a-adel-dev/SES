using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    SimulationProperties sim;
    float timeStep;
    float timer = 0f;
    // Start is called before the first frame update
    void Awake()
    {
        sim = FindObjectOfType<SimulationProperties>();
        timeStep = sim.timeStep;
    }

    // Update is called once per frame
    void Update()
    {
        PassTime();
    }

    private void PassTime()
    {
        timer += Time.deltaTime;
        if (timer >= timeStep)
        {
            timer -= timeStep;
            SendMessage("TimeStep");
        }
    }
}
