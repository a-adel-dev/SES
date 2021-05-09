using UnityEngine;
using System;
using SES.School;

namespace SES.SimProperties
{
    public class SimulationController : MonoBehaviour
    {
        SchoolDayProgressionController school;
        private void Awake()
        {
            school = FindObjectOfType<SchoolDayProgressionController>();
        }

        public void StartSim()
        {
            school.InitializeProperties();
            school.StartSchoolDay();

        }

        public void PauseSim()
        {
            school.PauseSchool();
        }

        public void ResumeSim()
        {
            school.ResumeSchool();
        }
    }
}