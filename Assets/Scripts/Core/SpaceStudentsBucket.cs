using System.Collections.Generic;
using UnityEngine;

namespace SES.Core
{ 
    public class SpaceStudentsBucket : MonoBehaviour
    {
        List<IStudentAI> spaceOriginalStudents = new List<IStudentAI>();
        List<IStudentAI> studentsCurrentlyInSpace = new List<IStudentAI>();
        List<IStudentAI> studentsOutOfSpace = new List<IStudentAI>();

        public void AddToSpaceOriginalStudents(IStudentAI student)
        {
            spaceOriginalStudents.Add(student);
        }

        public void ReceiveStudent(IStudentAI student)
        {
            studentsCurrentlyInSpace.Add(student);
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
        }

    }
}
