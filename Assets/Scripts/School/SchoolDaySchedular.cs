using System.Collections.Generic;
using UnityEngine;

public class SchoolDaySchedular : MonoBehaviour
{
    SimulationProperties sim;
    public List<int> classTimes = new List<int>();

    void Awake()
    {
        sim = GetComponent<SimulationProperties>();
    }

    public void ScheduleClasses()
    {
        for (int i = 0; i < sim.numPeriods * 2; i++)
        {
            if (i == 0)
            {
                classTimes.Add(40);
                continue;
            }
            else if (i % 2 != 0)
            {
                classTimes.Add(classTimes[i - 1] + (60 - sim.periodLength));
            }
            else if (i % 2 == 0)
            {
                classTimes.Add(classTimes[i - 1] + sim.periodLength);
            }
        }

    }
}
