using System;
using System.Collections.Generic;
using UnityEngine;
using SES.Core;

public class Tester : MonoBehaviour
{
    [SerializeField] IntVariable VMonth;
    [SerializeField] IntVariable VDay;
    [SerializeField] IntVariable VHour;
    [SerializeField] IntVariable VMinute;

    int month;
    int day;
    int hour;
    int minute;

    DateTime schoolTime;


    private void Update()
    {
        month = VMonth.value;
        day = VDay.value;
        hour = VHour.value;
        minute = VMinute.value;

        schoolTime = new DateTime(2020, month, day, hour, minute, 0);
        schoolTime = new DateTime(2020, month, day, hour, minute, 0);
    }
    
}
