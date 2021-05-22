using System.Collections.Generic;
using UnityEngine;

namespace SES.Core
{ 
    public class SpaceStudentsBucket : MonoBehaviour
    {
        public List<IStudentAI> spaceOriginalStudents = new List<IStudentAI>();
        public List<IStudentAI> studentsCurrentlyInSpace = new List<IStudentAI>();
        public List<IStudentAI> studentsOutOfSpace = new List<IStudentAI>();

        public void AddToSpaceOriginalStudents(IStudentAI student)
        {
            spaceOriginalStudents.Add(student);
        }

        public void ReceiveStudent(IStudentAI student)
        {
            studentsCurrentlyInSpace.Add(student);
        }

        public void ShuffleClassroomPupils()
        {
            studentsCurrentlyInSpace = ListHandler.Shuffle(studentsCurrentlyInSpace);
        }

        public bool IsInsideSpace(IStudentAI student)
        {
            return (studentsCurrentlyInSpace.Contains(student));
        }

        public void RemoveFromClass(IStudentAI student)
        {
            if (studentsCurrentlyInSpace.Contains(student))
            {
                studentsCurrentlyInSpace.Remove(student);
                studentsOutOfSpace.Add(student);
            }
        }

        public void ClearSpaceFromStudents()
        {
            spaceOriginalStudents = new List<IStudentAI>();
        }

        public void ClearStudentsInSpace()
        {
            studentsCurrentlyInSpace = new List<IStudentAI>();
        }

        public void ReceiveStudents(List<IStudentAI> students)
        {
            foreach (IStudentAI student in students)
            {
                AddToSpaceOriginalStudents(student);
            }
        }
    }
}
