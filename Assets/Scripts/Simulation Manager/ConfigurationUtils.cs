using System;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SES.SimProperties
{
    public static class ConfigurationUtils
    {
        static ConfigurationData configurationData;

        #region properties
        public static short SimulationLength
        {
            get { return configurationData.simulationTotalLength; }
        }

        public static short NumberOfSchoolDayPeriods
        {
            get { return configurationData.numberOfSchoolDayPeriods; }
        }

        public static short PeriodLength
        {
            get { return configurationData.periodLength; }
        }

        public static float TimeStep
        {
            get { return configurationData.timeStep; }
        }
        #endregion

        public static void Initialize()
        {
            configurationData = new ConfigurationData();
        }

    }
}
