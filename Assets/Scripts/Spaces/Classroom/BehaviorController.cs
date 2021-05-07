using System.Collections;
using UnityEngine;
using SES.Core;

namespace SES.Spaces.Classroom
{
    public class BehaviorController : MonoBehaviour
    {
        SpaceStudentsBucket studentsBucket;

        private void Start()
        {
            studentsBucket = GetComponent<SpaceStudentsBucket>();
        }
        /*
        public void FreePupilsBehavior()
        {
            foreach (IStudentAI pupil in studentsBucket.studentsCurrentlyInSpace)
            {
                pupil.SetControlledTo(false);
            }

            foreach (IStudentAI pupil in studentsBucket.studentsCurrentlyInSpace)
            {
                pupil.ResetPupil();
                pupil.IncreaseClearenceChance();
            }
        }

        public void ResetPupilBehavior()
        {
            foreach (IStudentAI pupil in studentsBucket.spaceOriginalStudents)
            {
                pupil.RestrictPupil();
                pupil.ResetClearenceChance();
            }
        }
        */



    }
}