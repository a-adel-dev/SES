using UnityEngine;
using SES.Core;

namespace SES.SimManager
{
    public class SimInitializer : MonoBehaviour
    {
        private void Awake()
        {
            ConfigurationUtils.Initialize();
            //read data from the csv and put in the defaults file
            DateTimeRecorder.StartSchoolDate();
        }
    }
}
