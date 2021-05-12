using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SES.Core
{
    public static class SimulationParameters 
    {
        #region primary parameters
        public static int periodLength { get; set; } = 50;
        public static int breakLength { get; set; } = 5;
        public static int numPeriods { get; set; } = 2;
        public static int simLength { get; set; } = 2;
        public static float timeStep { get; set; } = 0.5f;
        public static bool activitiesEnabled { get; set; } = true;
        public static int cooldownClassExit { get; set; } = 0;

        public static int initialNumStudentsContagious { get; set; } = 0;
        public static int initialNumTeachersContagious { get; set; } = 0;

        public static MaskFactor studentsMaskSettings { get; set; } = MaskFactor.none;
        public static MaskFactor teacherMaskSettings { get; set; } = MaskFactor.none;

        public static bool schoolHalfCapacity { get; set; } = false;
        public static bool classroomHalfCapacity { get; set; } = false;
        
        internal static int airControlSettings { get; set; } = 0;
        #endregion

        #region Secondry parameters
        public static int minClassSectionNumber { get; internal set; } = 1;
        public static int maxClassSectionNumber { get; internal set; } = 8;
        public static int minClassActivityTime { get; internal set; } = 8;

        public static int numSkippedClasses { get; internal set; } = 1;

        public static int baseAutonomyChance { get; set; } = 10;
        public static int breakAutonomyChance { get; set; } = 40;
        #endregion
    }
}
