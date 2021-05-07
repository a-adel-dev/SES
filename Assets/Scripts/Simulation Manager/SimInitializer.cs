using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SES.SimProperties
{
    public class SimInitializer : MonoBehaviour
    {
        private void Awake()
        {
            ConfigurationUtils.Initialize();
        }
    }
}
