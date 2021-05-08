using System;
using System.Collections.Generic;
using UnityEngine;
using SES.School;

namespace SES.UI
{

    
    public class TimeReader : MonoBehaviour
    {
        DateTimeRecorder timeRecorder;
        DateTime schoolTime;

        // Start is called before the first frame update
        void Start()
        {
            timeRecorder = FindObjectOfType<DateTimeRecorder>();
        }

        void TimeStep()
        {
            schoolTime = new DateTime();
            schoolTime = timeRecorder.schoolTime;
            Debug.Log(string.Format("{0:r}", schoolTime));
        }
    }
}
