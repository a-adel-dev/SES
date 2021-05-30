using System.Collections.Generic;
using UnityEngine;

namespace SES.Core
{ 
    public class SpaceStudentsBucket : MonoBehaviour
    {
        List<IStudentAI> spaceOriginalStudents = new List<IStudentAI>();
        List<IStudentAI> studentsCurrentlyInSpace = new List<IStudentAI>();
        List<IStudentAI> studentsOutOfSpace = new List<IStudentAI>();


        [SerializeField] string spaceOriginalStudentsCount = "";
        [SerializeField] string studentsCurrentlyInSpaceCount = "";
        [SerializeField] string studentsOutOfSpaceCount = "";


        private void Update()
        {
            spaceOriginalStudentsCount = $"{spaceOriginalStudents.Count}";
            studentsCurrentlyInSpaceCount = $"{studentsCurrentlyInSpace.Count}";
            studentsOutOfSpaceCount = $"{studentsOutOfSpace.Count}";
        }
        public void AddToSpaceOriginalStudents(IStudentAI student)
        {
            spaceOriginalStudents.Add(student);
        }

        public void ReceiveStudent(IStudentAI student)
        {
            studentsCurrentlyInSpace.Add(student);
            if (studentsOutOfSpace.Contains(student))
            {
                studentsOutOfSpace.Remove(student);
            }
        }

        public List<IStudentAI> GetStudentsInSpace()
        {
            return studentsCurrentlyInSpace;
        }

        public bool IsInsideSpace(IStudentAI student)
        {
            return (studentsCurrentlyInSpace.Contains(student));
        }

        public void ReleaseStudent(IStudentAI student)
        {
            if (studentsCurrentlyInSpace.Contains(student))
            {
                studentsCurrentlyInSpace.Remove(student);
                studentsOutOfSpace.Add(student);
            }
        }


        public List<IStudentAI> GetStudentsOutOfSpace()
        {
            return studentsOutOfSpace;
        }

        public void ResetSpace()
        {
            spaceOriginalStudents.Clear();
            studentsCurrentlyInSpace.Clear();
            studentsOutOfSpace.Clear();
        }

    }
}
