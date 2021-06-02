using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SES.Core;

namespace SES.Health
{
    public class GeneralHealthParamaters
    {
        public static int NumContagious { get; set; } = 0;
        public static int NumInfected { get; set; } = 0;
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

        public static void SetMakForStudents(MaskFactor factor)
        {
            foreach (IStudentAI student in TotalAgentsBucket.GetStudents())
            {
                student.AgentHealth.SetMaskFactor(factor);
            }
        }

        public static void SetMaskForTeachers(MaskFactor factor)
        {
            foreach (ITeacherAI teacher in TotalAgentsBucket.GetTeachers())
            {
                teacher.AgentHealth.SetMaskFactor(factor);
            }
        }

        public static void SetRandomMaksForTeachers()
        {
            foreach (var teacher in TotalAgentsBucket.GetTeachers())
            {
                int randomIndex = Random.Range(0, 100);

                if (randomIndex <= 25)
                {
                    teacher.AgentHealth.SetMaskFactor(MaskFactor.none);
                }
                else if (randomIndex > 25 && randomIndex <= 50)
                {
                    teacher.AgentHealth.SetMaskFactor(MaskFactor.cloth);
                }
                else if (randomIndex > 50 && randomIndex <= 75)
                {
                    teacher.AgentHealth.SetMaskFactor(MaskFactor.surgical);
                }
                else
                {
                    teacher.AgentHealth.SetMaskFactor(MaskFactor.N95);
                }
            }
        }

        public static void SetRandomMaksForStudents()
        {
            foreach (var teacher in TotalAgentsBucket.GetStudents())
            {
                int randomIndex = Random.Range(0, 100);

                if (randomIndex <= 25)
                {
                    teacher.AgentHealth.SetMaskFactor(MaskFactor.none);
                }
                else if (randomIndex > 25 && randomIndex <= 50)
                {
                    teacher.AgentHealth.SetMaskFactor(MaskFactor.cloth);
                }
                else if (randomIndex > 50 && randomIndex <= 75)
                {
                    teacher.AgentHealth.SetMaskFactor(MaskFactor.surgical);
                }
                else
                {
                    teacher.AgentHealth.SetMaskFactor(MaskFactor.N95);
                }
            }
        }
    }
}
