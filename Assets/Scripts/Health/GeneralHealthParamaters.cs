﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using SES.Core;

namespace SES.Health
{
    public class GeneralHealthParamaters
    {
        public static int numContagious = 0;
        public static int numInfected = 0;
        public List<ITeacherAI> teachers = new List<ITeacherAI>();
        public List<IStudentAI> students = new List<IStudentAI>();

        

        [Header("Health Settings")]
        public int numStudentInfected = 0;
        public int numStudentsContagious = 0;
        public int numTeachersInfected = 0;
        public int numTeachersContagious = 0;
        static List<float> globalAirControl = new List<float>();

        public static SpaceHealth[] Spaces { get; set; }

        public static void Initialize()
        {
            PopulateAirControlList();
            SetAirControl(SimulationDefaults.InitialAirExchangeRate);
        }

        public static void SetAirControl(int index)
        {
            foreach (SpaceHealth space in Spaces)
            {
                space.SetAirExhangeRate(globalAirControl[index]);
            }
        }

        private static void PopulateAirControlList()
        {
            globalAirControl.Add(0.12f);
            globalAirControl.Add(0.23f);
            globalAirControl.Add(0.85f);
            globalAirControl.Add(0.90f);
            globalAirControl.Add(2.16f);
            globalAirControl.Add(7.92f);
        }

        public static void InfectdSelectedStudents()
        {
            //Debug.Log($"infecting {numStudentsContagious} students");
            List<IStudentAI> studentsToInfect = ListHandler.GetRandomItemsFromList(TotalAgentsBucket.GetStudents(), SimulationParameters.initialNumStudentsContagious);
            foreach (IStudentAI student in studentsToInfect)
            {
                //Debug.Log($"infecting {student.gameObject.name}");
                student.AgentHealth.InfectAgent();
            }
        }

        public static void InfectSelectedTeachers()
        {
            //Debug.Log($"infecting {numTeachersContagious} teachers");
            List<ITeacherAI> teachersToInfect = ListHandler.GetRandomItemsFromList(TotalAgentsBucket.GetTeachers(), SimulationParameters.initialNumTeachersContagious);
            foreach (ITeacherAI teacher in teachersToInfect)
            {
                teacher.AgentHealth.InfectAgent();
                //Debug.Log($"infecting {teacher.gameObject.name}");
            }
        }

        public static void SetMaskForAgents()
        {
            foreach (IStudentAI student in TotalAgentsBucket.GetStudents())
            {
                student.AgentHealth.SetMaskFactor(SimulationParameters.studentsMaskSettings);
            }

            foreach (ITeacherAI teacher in TotalAgentsBucket.GetTeachers())
            {
                teacher.AgentHealth.SetMaskFactor(SimulationParameters.teacherMaskSettings);
            }
        }
    }
}
