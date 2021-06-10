using System.Collections;
using UnityEngine;
using System;
using SES.Core;

namespace SES.Core
{
    public static class DateTimeRecorder 
    {
        public static DateTime schoolTime { get; set; }

        static int Month { get; set; } = 1;
        static int Day { get; set; } = 1;
        static int Hour { get; set; } = 8;
        static int Minute { get; set; } = 0;

        static DateTime nextDay;

        
        public static void StartSchoolDate()
        {
            schoolTime = new DateTime(2020, Month, Day, Hour, Minute, 00);
            nextDay = schoolTime + new TimeSpan(24, 0, 0);
        }

        public static void UpdateSchoolTime(TimeSpan timeSpan)
        {
            schoolTime += timeSpan;
        }

        public static TimeSpan SkipToNextDay()
        {
            TimeSpan skippingTime = nextDay - schoolTime; 
            schoolTime += skippingTime;
            nextDay = schoolTime + new TimeSpan(24, 0, 0);
            return skippingTime;
        }
    }
}