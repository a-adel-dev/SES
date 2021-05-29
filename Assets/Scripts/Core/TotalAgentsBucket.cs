using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SES.Core
{
    public static class TotalAgentsBucket
    {
        static List<IStudentAI> totalStudents = new List<IStudentAI>();
        static List<ITeacherAI> totalTeachers = new List<ITeacherAI>();
        static Queue<ITeacherAI> availableTeachers = new Queue<ITeacherAI>();

        public static void AddToStudents(IStudentAI student)
        {
            totalStudents.Add(student);
        }

        public static void AddToTeachers(ITeacherAI teacher)
        {
            totalTeachers.Add(teacher);
        }

        public static List<IStudentAI> GetStudents()
        {
            return totalStudents;
        }

        public static List<ITeacherAI> GetTeachers()
        {
            return totalTeachers;
        }

        public static void PauseAgents()
        {
            foreach (IStudentAI student in totalStudents)
            {
                student.PauseAgent();
            }

            foreach (ITeacherAI teacher in totalTeachers)
            {
                teacher.PauseAgent();
            }
        }

        public static void ResumeAgents()
        {
            foreach (IStudentAI student in totalStudents)
            {
                student.ResumeAgent();
            }

            foreach (ITeacherAI teacher in totalTeachers)
            {
                teacher.ResumeAgent();
            }
        }

        public static void AddToAvailableTeachers(ITeacherAI teacher)
        {
            availableTeachers.Enqueue(teacher);
            //Debug.Log($"available teachers: {availableTeachers.Count}");
        }

        public static ITeacherAI GetAvailableTeacher()
        {
            //Debug.Log($"About to take a teacher: {availableTeachers.Count}");
            return availableTeachers.Dequeue();

        }

    }
}
