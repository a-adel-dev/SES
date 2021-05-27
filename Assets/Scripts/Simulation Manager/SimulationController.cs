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
            spawner.SpawnStudents();
            spawner.SpawnTeachers();
            school.InitializeProperties();
            school.StartSchoolDay();
        }

        public void PauseSim()
        {
            school.PauseSchool();
            TotalAgentsBucket.PauseAgents();
        }

        public void ResumeSim()
        {
            school.ResumeSchool();
            TotalAgentsBucket.ResumeAgents();
        }
    }
}