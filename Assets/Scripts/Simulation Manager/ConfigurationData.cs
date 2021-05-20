using UnityEngine;
using System.IO;
using System;

namespace SES.SimManager
{
    public class ConfigurationData
    {
        #region properties
        const string ConfigurationDataFileName = "ConfigurationData.csv";

        //config data goes here
        public float timeStep { get; set; } = 4f;
        public int simulationTotalLength { get; set; } = 14;
        public int numberOfSchoolDayPeriods { get; set; } = 4;
        public int periodLength { get; set; } = 40;
        public int breakLength { get; set; } = 10;
        public bool activitiesEnabled { get; set; } = true;
        public bool relocationEnabled { get; set; } = true;
        public int initialNumStudentsContagious { get; set; } = 0;
        public int initialNumTeachersContagious { get; set; } = 0;
        public int cooldownClassExit { get; set; } = 0;
        public int studentsMaskSettings { get; set; } = 0;
        public int teacherMaskSettings { get; set; } = 0;
        public bool schoolHalfCapacity { get; set; } = false;
        public bool classroomHalfCapacity { get; set; } = false;
        public int airControlSettings { get; set; } = 0;

        public int minClassSectionNumber { get; set; } = 1;
        public int maxClassSectionNumber { get; set; } = 8;
        public int minClassActivityTime { get; set; } = 8;

        public float adultWalkingSpeed { get; set; } = 1.5f;
        public float childrenWalkingSpeed { get; set; } = 0.6f;
        public int baseAutonomyChance { get; set; } = 10;
        public int breakAutonomyChance { get; set; } = 20;

        public int numSpotsForGroupActivity { get; set; } = 4;
        public float deskGroupActivityCompensationX { get; set; } = 0f;
        public float deskGroupActivityCompensationZ { get; set; } = -0.5f;
        public float minDistanceGroupActivity { get; set; } = 3f;




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
            }
        }

        private void SetConfigurationDataFields(string csvValues)
        {
            string[] values = csvValues.Split(',');

            timeStep = float.Parse(values[0]);
            simulationTotalLength = int.Parse(values[1]);
            numberOfSchoolDayPeriods = int.Parse(values[2]);
            periodLength = int.Parse(values[3]);
            breakLength = int.Parse(values[4]);
            activitiesEnabled = int.Parse(values[5]) == 1;
            relocationEnabled = int.Parse(values[6]) == 1;
            initialNumStudentsContagious = int.Parse(values[7]);
            initialNumTeachersContagious = int.Parse(values[8]);
            cooldownClassExit = int.Parse(values[9]);
            studentsMaskSettings = int.Parse(values[10]);
            teacherMaskSettings = int.Parse(values[11]);
            schoolHalfCapacity = int.Parse(values[12]) == 1;
            classroomHalfCapacity = int.Parse(values[13]) == 1;
            airControlSettings = int.Parse(values[14]);

            minClassSectionNumber = int.Parse(values[15]);
            maxClassSectionNumber = int.Parse(values[16]);
            minClassActivityTime = int.Parse(values[17]);
            adultWalkingSpeed = float.Parse(values[18]);
            childrenWalkingSpeed = float.Parse(values[19]);
            baseAutonomyChance = int.Parse(values[20]);
            breakAutonomyChance = int.Parse(values[21]);
            numSpotsForGroupActivity = int.Parse(values[22]);
            deskGroupActivityCompensationX = float.Parse(values[23]);
            deskGroupActivityCompensationZ = float.Parse(values[24]);
            minDistanceGroupActivity = float.Parse(values[25]);

        }
    }
}
