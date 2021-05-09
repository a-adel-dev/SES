using System.Collections;
using UnityEngine;
using System;
using SES.Core;

namespace SES.Core
{
    public static class DateTimeRecorder 
    {
        public static DateTime schoolTime { get; set; }

        static int month { get; set; } = 1;
        static int day { get; set; } = 1;
        static int hour { get; set; } = 8;
        static int minute { get; set; } = 0;

        
        public static void StartSchoolDate()
        {
            schoolTime = new DateTime(2020, month, day, hour, minute, 00);
        }

        public static void UpdateSchoolTime(TimeSpan timeSpan)
        {
            schoolTime += timeSpan;
        }


        public static TimeSpan SkipToNextDay()
        {
            TimeSpan skippingTime = new DateTime(2020,month, day + 1, 8, 00, 00) - schoolTime;
            schoolTime += skippingTime;
            return skippingTime;
        }
    }
}