using SES.Core;

namespace SES.SimManager
{
    public static class ConfigurationUtils
    {
        static ConfigurationData configurationData;

        public static void Initialize()
        {
            configurationData = new ConfigurationData();
            SimulationDefaults.timeStep = configurationData.TimeStep;
            SimulationDefaults.simLength = configurationData.SimulationTotalLength;
            SimulationDefaults.numPeriods = configurationData.NumberOfSchoolDayPeriods;
            SimulationDefaults.periodLength = configurationData.PeriodLength;
            SimulationDefaults.breakLength = configurationData.BreakLength;
            SimulationDefaults.activitiesEnabled = configurationData.ActivitiesEnabled;
            SimulationDefaults.relocationEnabled = configurationData.RelocationEnabled;
            SimulationDefaults.initialNumStudentsContagious = configurationData.InitialNumStudentsContagious;
            SimulationDefaults.initialNumTeachersContagious = configurationData.InitialNumTeachersContagious;
            SimulationDefaults.cooldownClassExit = configurationData.CooldownClassExit;
            GetStudentMaskSettings(configurationData.StudentsMaskSettings);
            GetTeacherMaskSettings(configurationData.TeacherMaskSettings);
            SimulationDefaults.halfCapacity = configurationData.SchoolHalfCapacity;
            SimulationDefaults.classroomHalfCapacity = configurationData.ClassroomHalfCapacity;
            SimulationDefaults.airControlSettings = configurationData.AirControlSettings;

            SimulationDefaults.minClassSectionNumber = configurationData.MinClassSectionNumber;
            SimulationDefaults.maxClassSectionNumber = configurationData.MaxClassSectionNumber;
            SimulationDefaults.minClassActivityTime = configurationData.MinClassActivityTime;
            SimulationDefaults.childrenWalkingSpeed = configurationData.ChildrenWalkingSpeed;
            SimulationDefaults.adultWalkingSpeed = configurationData.AdultWalkingSpeed;
            SimulationDefaults.numSpotsForGroupActivity = configurationData.NumSpotsForGroupActivity;
            SimulationDefaults.deskGroupActivityCompensationX = configurationData.DeskGroupActivityCompensationX;
            SimulationDefaults.deskGroupActivityCompensationZ = configurationData.DeskGroupActivityCompensationZ;
            SimulationDefaults.minDistanceGroupActivity = configurationData.MinDistanceGroupActivity;
            SimulationDefaults.bathroomChance = configurationData.BathroomChance;
            SimulationDefaults.lockerWaitingTime = configurationData.LockerWaitingTime;

            SimulationDefaults.CriticalRadius = configurationData.CriticalRadius;
            SimulationDefaults.ViralInfectivity = configurationData.ViralInfectivity;
            SimulationDefaults.NormalBreathingFlowRate = configurationData.NormalBreathingFlowRate;
            SimulationDefaults.TalkingBreathingFlowRate = configurationData.TalkingBreathingFlowRate;
            SimulationDefaults.LoudtalkingBreathingFlowRate = configurationData.LoudtalkingBreathingFlowRate;
            SimulationDefaults.AvarageNaturalDropletConentration = configurationData.AvarageNaturalDropletConentration;
            SimulationDefaults.AvarageTalkingDropletConcentration = configurationData.AvarageTalkingDropletConcentration;
            SimulationDefaults.AvarageShoutingDropletConcentration = configurationData.AvarageShoutingDropletConcentration;
            SimulationDefaults.ViralLoad = configurationData.ViralLoad;
            SimulationDefaults.JetEntrainmentCoefficient = configurationData.JetEntrainmentCoefficient;
            SimulationDefaults.MouthArea = configurationData.MouthArea;
            SimulationDefaults.InitialAirExchangeRate = configurationData.InitialAirExchangeRate;
            SimulationDefaults.N95MaskValue = configurationData.N95MaskValue;
            SimulationDefaults.SurgicalMaskValue = configurationData.SurgicalMaskValue;
            SimulationDefaults.ClothMaskValue = configurationData.ClothMaskValue;
            SimulationDefaults.SpaceInfectionThreshold = configurationData.SpaceInfectionThreshold;
            SimulationDefaults.TimeBeforeContagious = configurationData.TimeBeforeContagious;

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
