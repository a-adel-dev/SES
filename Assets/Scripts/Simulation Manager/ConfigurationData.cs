using UnityEngine;
using System.IO;
using System;

namespace SES.SimProperties
{
    public class ConfigurationData
    {
        #region properties
        const string ConfigurationDataFileName = "ConfigurationData.csv";

        //config data goes here
        public int simulationTotalLength { get; set; } = 14;
        public int numberOfSchoolDayPeriods { get; set; } = 4;
        public int periodLength { get; set; } = 40;

        public float timeStep { get; set; } = 4f;

        #endregion

        public ConfigurationData()
        {
            StreamReader input = null;
            try
            {
                input = File.OpenText(Path.Combine(
                    Application.streamingAssetsPath, ConfigurationDataFileName));

                string names = input.ReadLine();
                string values = input.ReadLine();

                SetConfigurationDataFields(values);
            }
            catch (Exception e)
            {

            }
            finally
            {
                if (input != null)
                {
                    input.Close();
                }
                else
                {
                }
            }
        }

        private void SetConfigurationDataFields(string csvValues)
        {
            string[] values = csvValues.Split(',');

            simulationTotalLength = int.Parse(values[0]);
            numberOfSchoolDayPeriods = int.Parse(values[1]);
            periodLength = int.Parse(values[2]);
            timeStep = float.Parse(values[3]);
        }
    }
}
