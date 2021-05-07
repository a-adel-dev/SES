using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SES.School;
using SES.Health;
using SES.Spaces.Classroom;
using SES.Core;
using SES.AIControl;
/*
namespace SES.SimProperties
{
    public class SimulationProperties : MonoBehaviour
    {
        public DateTime schoolDate { get; private set; }
        TimeSpan stepTime = new TimeSpan(0, 1, 0);

        SchoolManager schoolManager;
        HealthStats healthStats;
        GeneralHealthParamaters healthParameters;
        public int simLength { get; private set; } = 14;
        public int numPeriods { get; private set; } = 4;
        public int periodLength { get; private set; } = 40;
        public bool activitiesEnabled { get; private set; } = true;
        float timeStep = 4f;
        public float childWalkingSpeed = 0.6f;
        public float adultWalkingSpeed = 1.5f;
        public int sessionActivityMinTime = 8;
        public bool halfCapacity = false;
        public bool classroomHalfCapacity = false;
        public int cooldownClassExit = 0;


        // Use this for initialization
        void Awake()
        {
            healthParameters = GetComponent<GeneralHealthParamaters>();
            schoolManager = FindObjectOfType<SchoolManager>();
            healthStats = GetComponent<HealthStats>();
            healthStats.CollectAgents();
            healthStats.PopulateAgentLists();
            // should change to start when the simulation starts
            schoolDate = new DateTime(2020, 1, 1, 8, 00, 00);
            
            //PauseSim();

        }

        void TimeStep()
        {
            schoolDate += stepTime;
        }

        public void SetSimLength(int time)
        {
            simLength = time;
        }

        public void SetNumPeriods(int num)
        {
            numPeriods = num;
        }

        public void SetPeriodLength(int length)
        {
            periodLength = length;
        }

        public void EnableActivities(bool state)
        {
            activitiesEnabled = state;
            if (state)
            {
                foreach (ClassroomSpace classroom in schoolManager.subspaces.classrooms)
                {
                    classroom.planner.EnableActivities();
                }
            }
        }
        public void SetWalkingSpeed()
        {
            List<IAI> agentList = new List<IAI>(healthStats.totalAgents);
            foreach (IAI agent in agentList)
            {
                if (agent.ISStudent())
                {
                    var _agent = agent as AI;
                    _agent.GetComponent<NavMeshAgent>().speed = childWalkingSpeed * (60f / timeStep);
                }
                else
                {
                    var _agent = agent as TeacherAI;
                    _agent.GetComponent<NavMeshAgent>().speed = adultWalkingSpeed * (60f / timeStep);
                }
            }
        }

        internal void SetTimeStep(float value)
        {
            timeStep = value;
        }

        public void SetSessionActivityMinTime(int time)
        {
            foreach (ClassroomSpace classroom in schoolManager.subspaces.classrooms)
            {
                classroom.planner.SetActivityMinTime(time);
            }
        }

        public void PauseSim()
        {
            //Debug.Log($"pausing Sim");
            Time.timeScale = 0f;
        }

        public void StartSim()
        {
            Time.timeScale = 1f;
            schoolManager.StartSchoolDay();
            SetWalkingSpeed();
            SetHealthParameters();
        }

        private void EgressStudents()
        {
            //foreach (EgressPoint stairs in schoolManager.subspaces.staircases)
            //{
            //    stairs.RecallClasses(sim.cooldownClassExit);
            //}
            //TODO: have EgressController recall classes
        }

        public void SetHealthParameters()
        {
            healthParameters.InfectdSelectedStudents();
            healthParameters.InfectSelectedTeachers();
            healthParameters.SetMaskForAgents();
            healthParameters.SetAirExchangeRateForSpaces();
        }

        public void ResumeSim()
        {
            Time.timeScale = 1f;
        }

    }
}
*/
