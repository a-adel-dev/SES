using UnityEngine;
using SES.Core;
using System;

namespace SES.Spaces
{
    public class Lab : MonoBehaviour, ILab
    {
        SpotBucket labSubSpaces { get; set; }
        SpaceStudentsBucket studentsBucket;

        public Vector3 Entrance
        {
            get
            {
                return labSubSpaces.entrance.transform.position;
            }
        }


        private void Start()
        {
            studentsBucket = GetComponent<SpaceStudentsBucket>();
            if (GetComponent<SpotBucket>() == false)
            {
                Debug.LogError($" SpotBucket component does not exist on {gameObject.name}.");
            }
            else
            {
                labSubSpaces = GetComponent<SpotBucket>();
            }
        }

        public void StudentExitLab(IStudentAI student)
        {
            studentsBucket.ReleaseStudent(student);
        }

        public void ReceiveStudent(IStudentAI student)
        {
            studentsBucket.ReceiveStudent(student);
        }

        //public void SetlabEmptyTo(bool status)
        //{
        //    labEmpty = status;
        //}

        //public bool IsLabEmpty()
        //{
        //    return labEmpty;
        //}

        //public Spot AssignLabPositionToStudent()
        //{
        //    //assign each student to a lab position
        //    Spot selectedDesk = null;
        //    for (int i = 0; i < labObjects.desks.Count; i++)
        //    {
        //        if (labObjects.desks[i].ISpotAvailable())
        //        {
        //            selectedDesk = labObjects.desks[i];

        //            break;
        //        }
        //    }
        //    if (selectedDesk == null)
        //    {
        //        //Debug.LogError($"Could not find a desk for pupil {pupil.GetGameObject().name} in the {this.gameObject.name}");
        //        return selectedDesk;
        //    }
        //    else
        //    {
        //        //pupil.AssignLabPosition(selectedDesk.transform.position);
        //        return selectedDesk;
        //    }
        //}

        //public void StartLab()
        //{
        //    SetlabEmptyTo(false);
        //    StartCoroutine(ManageControlStatus());
        //    InvokeRepeating(nameof(UpdateStarted), 10.0f * timeStep, 10f * timeStep);
        //}

        //IEnumerator ManageControlStatus()
        //{
        //    if (labEmpty == true)
        //    {
        //        yield break;
        //    }
        //    yield return new WaitForSeconds(10f * timeStep);
        //    foreach (IStudentAI student in labStudents.studentsCurrentlyInSpace)
        //    {
        //        student.SetControlledTo(false);
        //    }
        //}

        public void EndLab(IClassroom classroom)
        {
            //clear desk spots
            //foreach (Spot desk in labObjects.desks)
            //{
            //    desk.ClearSpot();
            //}
            ////for students oput of the lab
            //foreach (IStudentAI student in labStudents.studentsOutOfSpace)
            //{
            //    //clear students labspots
            //    student.ClearCurrentLab();
            //    student.SetStudentLocationTo(StudentState.inClass);
            //    student.SetCurrentClass(classroom);
            //}
            ////for students currntly in lab
            //foreach (IStudentAI student in labStudents.studentsCurrentlyInSpace.ToArray())
            //{
            //    student.SetControlledTo(true);
            //    student.BackToDesk();
            //    student.BackToOriginalClassroom();
            //    //add queue condition
            //}
            //labStudents.ClearStudentsInSpace();
            //labStudents.ClearSpaceFromStudents();
            //currentOriginalClass = null;
            //SetlabEmptyTo(true);
        }

        //void UpdateStudentsChancesToExitLab()
        //{
        //    if (started == false && labStudents.studentsCurrentlyInSpace.Count > 0)
        //    {
        //        //Debug.Log($"updating clearto go status");
        //        started = true;
        //        foreach (IStudentAI pupil in labStudents.studentsCurrentlyInSpace)
        //        {
        //            pupil.ResetPupil();
        //        }
        //        started = true;
        //    }
        //}

        //void UpdateStarted()
        //{
        //    started = false;
        //}

        //public void SetCurrentOriginalClass(ClassroomSpace classroom)
        //{
        //    currentOriginalClass = classroom;
        //}

        //public BoxCollider GetTeacherSpace()
        //{
        //    return teachersSpace;
        //}

        //public void SetTimeStep(float timeStep)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public void SetSchoolDayState(SchoolDayState state)
        //{
        //    schoolDayState = state;
        //}

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public Spot RequestDesk(IAI student)
        {
            foreach (Spot desk in labSubSpaces.desks)
            {
                if (desk.ISpotAvailable())
                {
                    desk.FillSpot(student);
                    return desk;
                }
            }
            return null;
        }

        public Spot RequestLocker(IAI agent)
        {
            foreach (Spot locker in ListHandler.Shuffle(labSubSpaces.lockers))
            {
                if (locker.ISpotAvailable())
                {
                    locker.FillSpot(agent);
                    return locker;
                }
            }
            return null;
        }
    }
}
