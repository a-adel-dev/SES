﻿using UnityEngine;
using SES.Core;
using SES.School;

namespace SES.SimManager
{
    public class SimInitializer : MonoBehaviour
    {
        SchoolScheduler school;
        AISpawner spawner;
        private void Awake()
        {
            ConfigurationUtils.Initialize();
            //read data from the csv and put in the defaults file
            DateTimeRecorder.StartSchoolDate();
        }

        private void Start()
        {
            school = FindObjectOfType<SchoolScheduler>();
            spawner = GetComponent<AISpawner>();
            
        }


        public void InitializeVariables()
        {
            school.subspaces.Initialize();
            spawner.Initialize();
        }
    }
}
