namespace SES.SimProperties
{
    public static class ConfigurationUtils
    {
        static ConfigurationData configurationData;

        #region properties
        public static int SimulationLength
        {
            get { return configurationData.simulationTotalLength; }
        }

        public static int NumberOfSchoolDayPeriods
        {
            get { return configurationData.numberOfSchoolDayPeriods; }
        }

        public static int PeriodLength
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
            //reconfigure simdefaults;
        }

    }
}
