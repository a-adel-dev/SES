using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SES.Core;

namespace SES.Spaces.Classroom
{
    public class ActivityGroup : MonoBehaviour, IActivity
    {
        List<IStudentAI> students = new List<IStudentAI>();
        List<Spot> spots = new List<Spot>();

        int numSpotsForGroupActivity = 4;
        float deskGroupActivityCompensationX = 0f;
        float deskGroupActivityCompensationZ = -0.5f;
        float minDistanceGroupActivity = 3f;

        public ActivityGroup(List<IStudentAI> studentsInClass, List<Spot> activitySpots)
        {
            students = ListHandler.Shuffle(studentsInClass);
            spots = ListHandler.Shuffle(activitySpots);

            numSpotsForGroupActivity = SimulationDefaults.numSpotsForGroupActivity;
            deskGroupActivityCompensationX = SimulationDefaults.deskGroupActivityCompensationX;
            deskGroupActivityCompensationZ = SimulationDefaults.deskGroupActivityCompensationZ;
            minDistanceGroupActivity = SimulationDefaults.minDistanceGroupActivity;
        }

        public void EndActivity()
        {
            foreach (IStudentAI student in students)
            {
                student.SetStoppingDistance(0.3f);
                student.BackToDesk();
            }
        }

        public void StartActivity()
        {
            List<Spot> selectedDesks = PickSpotsForGroupActivity(1000, spots);
            if (selectedDesks != null)
            {
                List<IStudentAI> pupilsAvailableforActivity = new List<IStudentAI>(students);
                foreach (Spot desk in selectedDesks)
                {
                    List<IStudentAI> closestStudents = new List<IStudentAI>();
                    int searchIndex = 1;
                    while (closestStudents.Count < students.Count / numSpotsForGroupActivity)
                    {
                        foreach (IStudentAI pupil in pupilsAvailableforActivity.ToArray())
                        {
                            if (Vector3.Distance(desk.transform.position, pupil.GetGameObject().transform.position) < searchIndex)
                            {
                                closestStudents.Add(pupil);
                                pupilsAvailableforActivity.Remove(pupil);
                                if (closestStudents.Count >= students.Count / numSpotsForGroupActivity)
                                {
                                    break;
                                }
                            }
                        }
                        searchIndex++;
                    }
                    searchIndex = 1;
                    foreach (IStudentAI pupil in closestStudents)
                    {
                        pupil.SetStoppingDistance(0.5f);
                        pupil.NavigateTo(new Vector3(desk.transform.position.x + deskGroupActivityCompensationX,
                                                            0f,
                                                            desk.transform.position.z + deskGroupActivityCompensationZ));
                    }
                }
                while (pupilsAvailableforActivity.Count > 0)
                {
                    foreach (IStudentAI remainingPupil in pupilsAvailableforActivity.ToArray())
                    {
                        float shortestDistance = Mathf.Infinity;
                        Spot nearestGroupDesk = null;
                        foreach (Spot desk in selectedDesks)
                        {
                            if (Vector3.Distance(remainingPupil.GetGameObject().transform.position, desk.transform.position) < shortestDistance)
                            {
                                nearestGroupDesk = desk;
                            }
                        }
                        remainingPupil.NavigateTo(nearestGroupDesk.transform.position);
                        pupilsAvailableforActivity.Remove(remainingPupil);
                    }
                }
            }
        }

        List<Spot> PickSpotsForGroupActivity(int numTries, List<Spot> activitySpots)
        {
           List<Spot> selectedDesks = new List<Spot>();
           for (int i = 0; i < numTries; i++)
           {
               List<Spot> availableDesks = new List<Spot>(activitySpots);

               while (selectedDesks.Count < numSpotsForGroupActivity && availableDesks.Count > 0)
               {
                   Spot randomDesk = availableDesks[Random.Range(0, availableDesks.Count)];
                   if (selectedDesks == null)
                   {
                       selectedDesks.Add(randomDesk);
                       availableDesks.Remove(randomDesk);
                   }
                   else
                   {
                       bool tooClose = CompareProximity(randomDesk, selectedDesks);
                       if (!tooClose)
                       {
                           selectedDesks.Add(randomDesk);
                           availableDesks.Remove(randomDesk);
                       }
                       else
                       {
                           availableDesks.Remove(randomDesk);
                       }
                   }
               }
               if (selectedDesks.Count >= numSpotsForGroupActivity)
               {
                   break;
               }
               else
               {
                   selectedDesks.Clear();
               }
           }
           if (selectedDesks == null)
           {
               Debug.LogError("Could not find a solution, please reduce space proximity option!");
           }
           return selectedDesks;
        }

        bool CompareProximity(Spot randomDesk, List<Spot> desks)
        //group activity submethod
        {
           bool tooClose = false;
           foreach (Spot desk in desks)
           {
               if (Vector3.Distance(randomDesk.transform.position,
                                       desk.transform.position) < minDistanceGroupActivity)
               {
                   tooClose = true;
               }
           }
           return tooClose;
        }
    }
}
