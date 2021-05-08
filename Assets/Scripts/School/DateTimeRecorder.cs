using System.Collections;
using UnityEngine;
using System;
using SES.Core;

namespace SES.School
{
    public class DateTimeRecorder : MonoBehaviour
    {
        public DateTime schoolTime { get; set; }

        public int month { get; set; } = 1;
        public int day { get; set; } = 1;
        public int hour { get; set; } = 8;
        public int minute { get; set; } = 0;

        private void Start()
        {
            schoolTime = new DateTime(2020, month, day, hour, minute, 00);
        }

        public void UpdateSchoolTime(TimeSpan timeSpan)
        {
            schoolTime += timeSpan;
        }

        public void ResetSchoolTime()
        {
            schoolTime = new DateTime(2020, month, day, hour, minute, 00);
        }

        public TimeSpan SkipToNextDay()
        {
            TimeSpan skippingTime = new DateTime(2020,month, day + 1, 8, 00, 00) - schoolTime;
            schoolTime += skippingTime;
            return skippingTime;
        }

    }
}