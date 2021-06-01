using System;

namespace SES.Core
{
    public static class SimulationDefaults
    {
        #region primary parameters
        public static float timeStep { get; set; } = 0.5f;
        public static int simLength { get; set; } = 2;
        public static int numPeriods { get; set; } = 2;
        public static int periodLength { get; set; } = 50;
        public static int breakLength { get; set; } = 5;
        public static bool activitiesEnabled { get; set; } = true;
        public static bool relocationEnabled { get; set; } = true;
        public static int initialNumStudentsContagious { get; set; } = 0;
        public static int initialNumTeachersContagious { get; set; } = 0;
        public static int cooldownClassExit { get; set; } = 0;

        public static MaskFactor studentsMaskSettings { get; set; } = MaskFactor.none;
        public static MaskFactor teacherMaskSettings { get; set; } = MaskFactor.none;

        public static bool halfCapacity { get; set; } = false;
        public static bool classroomHalfCapacity { get; set; } = false;

        internal static int airControlSettings { get; set; } = 0;
        #endregion

        #region Secondry parameters
        public static int minClassSectionNumber { get; internal set; } = 1;
        public static int maxClassSectionNumber { get; internal set; } = 8;
        public static int minClassActivityTime { get; internal set; } = 8;

        public static float adultWalkingSpeed { get; set; } = 1.5f;
        public static float childrenWalkingSpeed { get; set; } = 0.6f;

        public static int baseAutonomyChance { get; set; } = 10;
        public static int breakAutonomyChance { get; set; } = 20;

        public static int numSpotsForGroupActivity { get; set; } = 4;
        public static float deskGroupActivityCompensationX { get; set; } = 0f;
        public static float deskGroupActivityCompensationZ { get; set; } = -0.5f;
        public static float minDistanceGroupActivity { get; set; } = 3f;
        public static int bathroomChance { get; set; } = 2;
        public static float lockerWaitingTime { get; set; } = 2f;

        public static float CriticalRadius { get; set; } = 2.6f;
        public static float ViralInfectivity { get; set; } = 0.1f;
        public static float NormalBreathingFlowRate { get; set; } = 0.5f;
        public static float TalkingBreathingFlowRate { get; set; } = 0.75f;
        public static float LoudtalkingBreathingFlowRate { get; set; } = 1.0f;
        public static float AvarageNaturalDropletConentration { get; set; } = 0.1f;
        public static float AvarageTalkingDropletConcentration { get; set; } = 0.3f;
        public static float AvarageShoutingDropletConcentration { get; set; } = 0.9f; 
        public static float ViralLoad { get; set; } = 10E11f;
        public static float JetEntrainmentCoefficient { get; set; } = 0.1f;
        public static float MouthArea { get; set; } = 2f;
        public static int InitialAirExchangeRate { get; set; } = 3;
        public static float N95MaskValue { get; set; } = 0.05f;
        public static float SurgicalMaskValue { get; set; } = 0.15f;
        public static float ClothMaskValue { get; set; } = 0.8f;
        public static float SpaceInfectionThreshold { get; set; } = .001f;
        public static TimeSpan TimeBeforeContagious { get; set; } = new TimeSpan(0, 10, 0);
        #endregion
    }
}
