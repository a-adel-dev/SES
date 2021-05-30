using UnityEngine;
using SES.Core;
using System;
using System.Collections.Generic;

namespace SES.Spaces
{
    public class Lab : MonoBehaviour, ILab
    {
        public SpotBucket SubSpaces { get; set; }

        SpaceStudentsBucket studentsBucket;

        private void Start()
        {
            studentsBucket = GetComponent<SpaceStudentsBucket>();
            if (GetComponent<SpotBucket>() == false)
            {
                Debug.LogError($" SpotBucket component does not exist on {gameObject.name}.");
            }
            else
            {
                SubSpaces = GetComponent<SpotBucket>();
            }
        }

        public void StudentExitLab(IStudentAI student)
        {
            studentsBucket.ReleaseStudent(student);
        }

        public void ReceiveStudent(IStudentAI student)
        {
            student.TransitStudent();
            student.BackToDesk();
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public Spot RequestDesk(IAI student)
        {
            return SubSpaces.GetAvailableDesk(student);
        }

        public Spot RequestLocker(IAI agent)
        {
            return SubSpaces.GetRandomLocker(agent);
        }

        public void MarkStudents(List<IStudentAI> students)
        {
            foreach (IStudentAI student in students)
            {
                student.CurrentClassroom = null;
                student.CurrentLab = this;
                student.currentDesk = RequestDesk(student);
                studentsBucket.ReceiveStudent(student);
            }
        }

        public void StartLab()
        {
            foreach (IStudentAI student in studentsBucket.GetStudentsInSpace())
            {
                student.StartClass();
            }
        }

        public void EndLab(IClassroom classroom)
        {
            throw new NotImplementedException();
        }

        public List<IStudentAI> ReleaseLabStudents()
        {
            List<IStudentAI> students = new List<IStudentAI>();
            foreach (IStudentAI student in studentsBucket.GetStudentsInSpace())
            {
                students.Add(student);
            }
            foreach (IStudentAI student in studentsBucket.GetStudentsOutOfSpace())
            {
                students.Add(student);
            }
            return students;
        }

        public List<IStudentAI> GetStudentsInLab()
        {
            return studentsBucket.GetStudentsInSpace();
        }

        public void EndLab()
        {
            studentsBucket.ResetSpace();
            SubSpaces.ResetDesks();
            SubSpaces.ResetLockers();
        }
    }
}
