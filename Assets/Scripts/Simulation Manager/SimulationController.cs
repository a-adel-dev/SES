using UnityEngine;
using System;
using SES.School;
using SES.Core;
using SES.AIControl;

namespace SES.SimManager
{
    public class SimulationController : MonoBehaviour
    {
        SchoolDayProgressionController school;
        
        IAISpawner spawner;
        private void Awake()
        {
            school = FindObjectOfType<SchoolDayProgressionController>();
            spawner = FindObjectOfType<AISpawner>();

        }

        public void StartSim()
        {
            school.InitializeProperties();
            spawner.SpawnStudents();
            spawner.SpawnTeachers();
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