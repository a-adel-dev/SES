using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SES.Core
{
    public static class SimulationVariables 
    {
        public static short periodLength { get; set; } = 10;
        public static short breakLength { get; set; } = 5;
        public static short numPeriods { get; set; } = 2;
        public static short simLength { get; set; } = 2;
        public static float timeStep { get; set; } = 0.5f;
    }
}
