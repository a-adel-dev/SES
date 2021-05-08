using System;
using System.Collections.Generic;
using UnityEngine;
using SES.School;
using SES.Core;

namespace SES.UI
{
    public class TimeReader : MonoBehaviour
    {
        DateTime schoolTime;

        void TimeStep()
        {
            schoolTime = DateTimeRecorder.schoolTime;
            //Debug.Log(string.Format("{0:r}", schoolTime));
        }
    }
}
