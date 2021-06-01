
namespace SES.Core
{
    public static class SimulationParameters 
    {
        #region primary parameters
        public static int periodLength { get; set; } = 50;
        public static int breakLength { get; set; } = 5;
        public static int numPeriods { get; set; } = 2;
        public static int simLength { get; set; } = 2;
        public static float TimeStep { get; set; } = 0.5f;
        public static bool activitiesEnabled { get; set; } = true;
        public static bool RelocationEnabled { get; set; } = true;
        public static int CooldownClassExit { get; set; } = 0;

        public static int initialNumStudentsContagious { get; set; } = 0;
        public static int initialNumTeachersContagious { get; set; } = 0;

        public static MaskFactor studentsMaskSettings { get; set; } = MaskFactor.none;
        public static MaskFactor teacherMaskSettings { get; set; } = MaskFactor.none;

        public static bool schoolHalfCapacity { get; set; } = false;
        public static bool classroomHalfCapacity { get; set; } = false;
        
        public static int airControlSettings { get; set; } = 0;
        #endregion
    }
}
