using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    SchoolManager schoolManager;
    float timeStep;
    float timer = 0f;
    // Start is called before the first frame update
    void Awake()
    {
        schoolManager = FindObjectOfType<SchoolManager>();
        timeStep = schoolManager.timeStep;
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
