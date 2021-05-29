using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SES.Core;

namespace SES.Spaces.Classroom
{
    public class ActivityBoard : IActivity
    {
        List<IStudentAI> students = new List<IStudentAI>();
        List<Spot> spots = new List<Spot>();
        public ActivityBoard(List<IStudentAI> studentsInClass, List<Spot> activitySpots)
        {
            students = ListHandler.Shuffle(studentsInClass);
            spots = ListHandler.Shuffle(activitySpots);
        }

        public void StartActivity()
        { 
            if (students.Count != 0)
            {
                int randomIndex = Random.Range(1, spots.Count);
                for (int i = 0; i < randomIndex; i++)
                {
                    spots[i].FillSpot(students[i]);
                    students[i].NavigateTo(spots[i].transform.position);
                }
            }
            foreach (IStudentAI student in students)
            {
                student.LookAtBoard();
            }
        }

        public void EndActivity()
        {
            foreach (Spot boardSpot in spots)
            {
                if (boardSpot.ISpotAvailable() == false)
                { 
                    IStudentAI student = boardSpot.ClearSpot() as IStudentAI;
                    student.BackToDesk();
                }

            }
        }
    }
}
