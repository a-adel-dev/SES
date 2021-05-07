using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SES.Core;
/*
namespace SES.Spaces.Classroom
{
    public class ActivityBoard : MonoBehaviour , IActivity
    {
        [SerializeField] FloatVariable timeStepVariable;
        [SerializeField] float timeStep;
        public bool activityInProgress = false;
 
        // Use this for initialization


        private void update()
        {
            timeStep = timeStepVariable.value;
        }

        public IEnumerator StartBoardActivity(List<IStudentAI> studentsInClass, List<Spot> activitySpots, int index)
        {
            activityInProgress = true;
            if (Mathf.Abs(timeStep) <= Mathf.Epsilon)
            {
                Debug.LogError($"timeStep is not set in boardActivity");
            }
            //Debug.Log("board");
            if (studentsInClass.Count != 0)
            {

                List<Spot> boardSpots = new List<Spot>(activitySpots);//TODO: Shuffle spots before passing them in
                int randomIndex = Random.Range(1, boardSpots.Count);
                for (int i = 0; i < randomIndex; i++)
                {
                    studentsInClass[i].AssignSpot(boardSpots[i]);// wouldn't the same pupil be picked every time? i think we need to shuffle students.
                    boardSpots[i].FillSpot(studentsInClass[i]);
                    studentsInClass[i].GuideTo(boardSpots[i].transform.position);
                }
                yield return new WaitForSecondsRealtime((index - 2) * timeStep);
                for (int i = 0; i < randomIndex; i++)
                {
                    var spot = studentsInClass[i].ReleaseSpot();
                    spot.ClearSpot();
                    studentsInClass[i].BackToDesk();
                }
            }
            activityInProgress = false;
            //planner.EndActivity();
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