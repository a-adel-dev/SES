using UnityEngine;
using SES.Core;
using System;
using System.Collections.Generic;

namespace SES.Spaces
{
    public class Lab : MonoBehaviour, ILab
    {
        public SpotBucket labSubSpaces { get; set; }
        public SpaceStudentsBucket labStudents;
        public string message = "";

        private void Start()
        {
            labStudents = GetComponent<SpaceStudentsBucket>();
            if (GetComponent<SpotBucket>() == false)
            {
                Debug.LogError($" SpotBucket component does not exist on {gameObject.name}.");
            }
            else
            {
                labSubSpaces = GetComponent<SpotBucket>();
            }
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

        public List<IStudentAI> EndLab()
        {
            List<IStudentAI> students = new List<IStudentAI>();
            //clear desk spots
            foreach (Spot desk in labSubSpaces.desks)
            {
                desk.ClearSpot();
            }
            //for students oput of the lab
            foreach (IStudentAI student in labStudents.studentsCurrentlyInSpace)
            {
                students.Add(student);
                //clear students labspots
                student.ClearCurrentLab();
                student.currentDesk = null;
            }
            //for students currntly in lab
            labStudents.ClearStudentsInSpace();
            labStudents.ClearSpaceFromStudents();

            return students;
        }


        private void Update()
        {
            //message = ($"{gameObject.name} has {labStudents.studentsCurrentlyInSpace.Count} students");
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

        internal void ReceiveStudent(IStudentAI student)
        {
            student.currentDesk = RequestDesk(student);
            labStudents.ReceiveStudent(student);
        }
    }
}
