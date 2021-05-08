using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SES.Core
{
    public static class SimulationParameters 
    {
        #region primary parameters
        public static short periodLength { get; set; } = 50;
        public static short breakLength { get; set; } = 5;
        public static short numPeriods { get; set; } = 2;
        public static short simLength { get; set; } = 2;
        public static float timeStep { get; set; } = 0.5f;

        #endregion

        #region Secondry parameters
        public static int minClassSectionNumber { get; internal set; } = 1;
        public static int maxClassSectionNumber { get; internal set; } = 8;
        public static int minClassActivityTime { get; internal set; } = 8;
        #endregion
    }
}
