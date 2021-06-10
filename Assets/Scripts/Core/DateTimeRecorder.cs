using System.Collections;
using UnityEngine;
using System;
using SES.Core;

namespace SES.Core
{
    public static class DateTimeRecorder 
    {
        public static DateTime SchoolTime { get; set; }

        static int month= 1;
        static int day= 1;
        static int hour= 8;
        static int minute= 0;

        static DateTime nextDay;

        
        public static void StartSchoolDate()
        {
            SchoolTime = new DateTime(2020, month, day, hour, minute, 00);
            nextDay = SchoolTime + new TimeSpan(24, 0, 0);
        }

        public static void UpdateSchoolTime(TimeSpan timeSpan)
        {
            SchoolTime += timeSpan;
        }

        public static TimeSpan SkipToNextDay()
        {
            TimeSpan skippingTime = nextDay - SchoolTime; 
            SchoolTime += skippingTime;
            nextDay = SchoolTime + new TimeSpan(24, 0, 0);
            return skippingTime;
        }
    }
}