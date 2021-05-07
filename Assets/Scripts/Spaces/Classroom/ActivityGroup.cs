using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SES.Core;
/*
namespace SES.Spaces.Classroom
{
    public class ActivityGroup : MonoBehaviour, IActivity
    {
        [SerializeField] FloatVariable timeStepVariable;
        float timeStep;

        [SerializeField] int numSpotsForGroupActivity = 4;
        [SerializeField] float deskGroupActivityCompensationX = 0f;
        [SerializeField] float deskGroupActivityCompensationZ = -0.5f;
        [SerializeField] float minDistanceGroupActivity = 3f;
        public bool activityInProgress = false;

        // Use this for initialization
        private void update()
        {
            timeStep = timeStepVariable.value;
        }


        public IEnumerator StartGroupActivity(List<IStudentAI> studentsInClass, List<Spot> activitySpots, int index)
        {
            
            if (Mathf.Abs(timeStep)<= Mathf.Epsilon)
            {
                Debug.LogError($"timeStep is not set in groupActivity");
            }
            activityInProgress = true;
            //Debug.Log("Group");
            if (studentsInClass.Count != 0)
            {
                List<Spot> selectedDesks = PickSpotsForGroupActivity(1000, activitySpots);
                if (!(selectedDesks == null))
                {
                    List<IStudentAI> pupilsAvailableforActivity = new List<IStudentAI>(studentsInClass);
                    foreach (Spot desk in selectedDesks)
                    {
                        List<IStudentAI> closestStudents = new List<IStudentAI>();
                        int searchIndex = 1;
                        while (closestStudents.Count < studentsInClass.Count / numSpotsForGroupActivity)
                        {
                            foreach (IStudentAI pupil in pupilsAvailableforActivity.ToArray())
                            {
                                if (Vector3.Distance(desk.transform.position, pupil.GetTransform().position) < searchIndex)
                                {
                                    closestStudents.Add(pupil);
                                    pupilsAvailableforActivity.Remove(pupil);
                                    if (closestStudents.Count >= studentsInClass.Count / numSpotsForGroupActivity)
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
                            pupil.setStoppingDistance(.5f);
                            pupil.GuideTo(new Vector3(desk.transform.position.x + deskGroupActivityCompensationX,
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
                                if (Vector3.Distance(remainingPupil.GetTransform().position, desk.transform.position) < shortestDistance)
                                {
                                    nearestGroupDesk = desk;
                                }
                            }
                            remainingPupil.GuideTo(nearestGroupDesk.transform.position);
                            pupilsAvailableforActivity.Remove(remainingPupil);
                        }
                    }
                }
                yield return new WaitForSecondsRealtime((index - 2) * timeStep);
                foreach (IStudentAI pupil in studentsInClass)
                {
                    pupil.setStoppingDistance(0.3f);
                    pupil.BackToDesk();
                }
            }
            //planner.EndActivity();
            activityInProgress = false;
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
                    bool tooClose = false;
                    if (selectedDesks == null)
                    {
                        selectedDesks.Add(randomDesk);
                        availableDesks.Remove(randomDesk);
                    }
                    else
                    {
                        tooClose = CompareProximity(randomDesk, selectedDesks);
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

        public void SetTimeStep(float _timeStep)
        {
            timeStep = _timeStep;
        }

        public bool GetActivityInProgressState()
        {
            return activityInProgress;
        }


    }
}
*/