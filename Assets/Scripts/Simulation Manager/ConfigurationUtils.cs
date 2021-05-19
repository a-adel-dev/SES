using SES.Core;
using System;

namespace SES.SimManager
{
    public static class ConfigurationUtils
    {
        static ConfigurationData configurationData;

        public static void Initialize()
        {
            configurationData = new ConfigurationData();
            SimulationDefaults.timeStep = configurationData.timeStep;
            SimulationDefaults.simLength = configurationData.simulationTotalLength;
            SimulationDefaults.numPeriods = configurationData.numberOfSchoolDayPeriods;
            SimulationDefaults.periodLength = configurationData.periodLength;
            SimulationDefaults.breakLength = configurationData.breakLength;
            SimulationDefaults.activitiesEnabled = configurationData.activitiesEnabled;
            SimulationDefaults.relocationEnabled = configurationData.relocationEnabled;
            SimulationDefaults.initialNumStudentsContagious = configurationData.initialNumStudentsContagious;
            SimulationDefaults.initialNumTeachersContagious = configurationData.initialNumTeachersContagious;
            SimulationDefaults.cooldownClassExit = configurationData.cooldownClassExit;
            GetStudentMaskSettings(configurationData.studentsMaskSettings);
            GetTeacherMaskSettings(configurationData.teacherMaskSettings);
            SimulationDefaults.halfCapacity = configurationData.schoolHalfCapacity;
            SimulationDefaults.classroomHalfCapacity = configurationData.classroomHalfCapacity;
            SimulationDefaults.airControlSettings = configurationData.airControlSettings;

            SimulationDefaults.minClassSectionNumber = configurationData.minClassSectionNumber;
            SimulationDefaults.maxClassSectionNumber = configurationData.maxClassSectionNumber;
            SimulationDefaults.minClassActivityTime = configurationData.minClassActivityTime;
            SimulationDefaults.childrenWalkingSpeed = configurationData.childrenWalkingSpeed;
            SimulationDefaults.adultWalkingSpeed = configurationData.adultWalkingSpeed;

        }



        private static void GetStudentMaskSettings(int maskSettings)
        {
            switch (maskSettings)
            {
                case 0:
                    SimulationDefaults.studentsMaskSettings = MaskFactor.none;
                    break;
                case 1:
                    SimulationDefaults.studentsMaskSettings = MaskFactor.cloth;
                    break;
                case 2:
                    SimulationDefaults.studentsMaskSettings = MaskFactor.surgical;
                    break;
                case 3:
                    SimulationDefaults.studentsMaskSettings = MaskFactor.N95;
                    break;
            }
        }

        private static void GetTeacherMaskSettings(int maskSettings)
        {
            switch (maskSettings)
            {
                case 0:
                    SimulationDefaults.teacherMaskSettings = MaskFactor.none;
                    break;
                case 1:
                    SimulationDefaults.teacherMaskSettings = MaskFactor.cloth;
                    break;
                case 2:
                    SimulationDefaults.teacherMaskSettings = MaskFactor.surgical;
                    break;
                case 3:
                    SimulationDefaults.teacherMaskSettings = MaskFactor.N95;
                    break;
            }
        }
    }
}
