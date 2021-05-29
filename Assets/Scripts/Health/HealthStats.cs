using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SES.Core;
using System.Linq;

namespace SES.Health
{
    public class HealthStats : MonoBehaviour
    {
        public List<IAI> totalAgents;
        List<IAI> totalContagious = new List<IAI>();
        public int numContagious = 0;
        public int numInfected = 0;
        public List<ITeacherAI> teachers = new List<ITeacherAI>();
        public List<IStudentAI> students = new List<IStudentAI>();

        public void CollectAgents()
        {
            var agents = FindObjectsOfType<MonoBehaviour>().OfType<IAI>();
            foreach (IAI agent in agents)
            {
                totalAgents.Add(agent);
            }
        }

        public int GetNumAgents()
        {
            if (totalAgents == null) { return 0; }
            return totalAgents.Count;
        }

        public int GetNumContagious()
        {
            return numContagious;
        }
        private void Update()
        {
            numContagious = GeneralHealthParamaters.numContagious;
            numInfected = GeneralHealthParamaters.numInfected;
        }
        void TimeStep()
        {
            //foreach (IAI agent in totalAgents)
            //{
            //    if (agent.IsInfected() && !totalContagious.Contains(agent))
            //    {
            //        totalContagious.Add(agent);
            //    }
            //}
        }

        //public void PopulateAgentLists()
        //{
        //    foreach (IAI agent in totalAgents)
        //    {
        //        if (agent.IsTeacher())
        //        {
        //            teachers.Add(agent as ITeacherAI);
        //        }
        //        else
        //        {
        //            students.Add(agent as IStudentAI);
        //        }
        //    }
        //}

        public List<IStudentAI> GetStudents()
        {
            return students;
        }

        public List<ITeacherAI> GetTeachers()
        {
            return teachers;
        }
    }
}
