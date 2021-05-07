using System.Collections.Generic;
using UnityEngine;

namespace SES.School
{
    public class SchoolDaySchedular : MonoBehaviour
    {
        int periodLength = 40;
        int numPeriods = 4;

        public List<int> classTimes = new List<int>();

        public void ScheduleClasses()
        {
            for (int i = 0; i < numPeriods * 2; i++)
            {
                if (i == 0)
                {
                    classTimes.Add(periodLength);
                    continue;
                }
                else if (i % 2 != 0)
                {
                    classTimes.Add(classTimes[i - 1] + (60 - periodLength));
                }
                else if (i % 2 == 0)
                {
                    classTimes.Add(classTimes[i - 1] + periodLength);
                }
            }
        }

        public void SetPeriodLength(int length)
        {
            periodLength = length;
        }

        public void SetNumPeriods(int num)
        {
            numPeriods = num;
        }
    }
}
