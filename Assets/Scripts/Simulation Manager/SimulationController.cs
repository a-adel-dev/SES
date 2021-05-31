using UnityEngine;
using System;
using SES.School;
using SES.Core;
using SES.Health;


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
            SetHealthConditions();
            school.StartSchoolDay();
        }

        private void SetHealthConditions()
        {
            //infect students
            GeneralHealthParamaters.InfectdSelectedStudents();
            //infect teachers
            GeneralHealthParamaters.InfectSelectedTeachers();
            //set students masks

            //set teacher masks
            GeneralHealthParamaters.SetMaskForAgents();
            //set space Air control
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