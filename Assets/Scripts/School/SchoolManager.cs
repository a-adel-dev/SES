using System.Collections.Generic;
using UnityEngine;
using SES.Core;

namespace SES.School
{

    public class SchoolManager : MonoBehaviour
    {
        //List<IClassroom> inPlaceClassrooms = new List<IClassroom>(); //classrooms with classes in place i.e. not in a lab
        ////List<ClassLabPair> classlabPairList = new List<ClassLabPair>();
        ////List<ITeacherAI> teacherspool;

        //public SchoolSubSpacesBucket subspaces;
        //public TeacherPool teacherPoolController;
        //public SchoolDaySchedular schedular;

        //public SchoolDayState schoolDayState;
        //ClassroomsOcsillatingState currentState = ClassroomsOcsillatingState.inSession;
        //ClassroomsOcsillatingState previousState = ClassroomsOcsillatingState.onBreak;

        //public int schoolTime = 0;
        //int currentPeriodIndex = 0;

        //private void Start()
        //{
        //    subspaces = GetComponent<SchoolSubSpacesBucket>();
        //    //teacherPoolController = GetComponent<TeacherPool>();
        //    schedular = GetComponent<SchoolDaySchedular>();
        //    schedular.ScheduleClasses();
        //    inPlaceClassrooms = new List<IClassroom>(subspaces.classrooms);
        //    StartSchoolDay();
        //}

        //private void Update()
        //{
        //    OscillateClassSessions();
        //}

        //public void StartSchoolDay()
        //{
        //    SetSchoolDayState(SchoolDayState.classesInSession);
        //    StartClasses();
        //}

        //private void StartClasses()
        //{
        //    SetSchoolDayState(SchoolDayState.classesInSession);
        //    Debug.Log($"starting All classes");
        //    foreach (IClassroom classroom in inPlaceClassrooms)
        //    {
        //        classroom.StartClass();
        //    }
        //}

        //public void OscillateClassSessions()
        //{
        //    //TODO: Sim is over
        //    //TODO: day is over
        //    //end school day
        //    if (currentPeriodIndex == schedular.classTimes.Count - 1)
        //    {
        //        EndSchoolDay();
        //         // needs to adjust for number of sim days
        //    }
        //    //end period
        //    if (currentPeriodIndex % 2 == 0)
        //    {
        //        if (schoolTime >= schedular.classTimes[currentPeriodIndex])
        //        {
        //            previousState = currentState;
        //            currentState = ClassroomsOcsillatingState.onBreak;
        //            if (currentState != previousState)
        //            {
        //                currentPeriodIndex++;
        //                //ReplaceClassTeachers();
        //                //Debug.Log("increasing current Period Index");
        //                EndClasses();
        //                /*
        //                if (classlabPairList != null)
        //                {
        //                    SendClassesBackFromLabs();
        //                }
        //                */
        //            }
        //        }
        //    }
        //    //start period
        //    else if (currentPeriodIndex % 2 != 0)
        //    {
        //        if (currentPeriodIndex > schedular.classTimes.Count - 1) { return; }
        //        if (schoolTime >= schedular.classTimes[currentPeriodIndex])
        //        {
        //            previousState = currentState;
        //            currentState = ClassroomsOcsillatingState.inSession;
        //            if (currentState != previousState)
        //            {
        //                currentPeriodIndex++;
        //                //SendClassesToLabs();
        //                StartClasses();
        //            }
        //        }
        //    }

        //}
        //private void EndClasses()
        //{
        //    Debug.Log($"Ending Classes");
        //    SetSchoolDayState(SchoolDayState.breakTime);
        //    foreach (IClassroom classroom in inPlaceClassrooms)
        //    {
        //        classroom.EndClass();
        //    }
        //}

        //private void EndSchoolDay()
        //{
        //    schedular.classTimes = new List<int>();
        //    SetSchoolDayState(SchoolDayState.egressTime);
        //    Debug.Log($"Ending School day");
        //    //TODO: Fix Egress
        //    //EgressStudents(); 
        //    schoolDayState = SchoolDayState.offTime;
        //}

        //void SetSchoolDayState(SchoolDayState state)
        //{
        //    schoolDayState = state;
        //}

        //void TimeStep()
        //{
        //    schoolTime++;
        //    //Debug.Log($"adding 1 to school time");
        //}

        ////void SendClassesToLabs()
        ////{
        ////    Debug.Log($"sending classes to labs");
        ////    //if (schoolDay == false) { return; }
        ////    foreach (Lab lab in subspaces.labs)
        ////    {
        ////        SendRandomClassToLab(lab);
        ////    }
        ////}

        ////void SendRandomClassToLab(Lab lab)
        ////{
        ////    if (lab.IsLabEmpty())
        ////    {
        ////        int randomIndex = UnityEngine.Random.Range(0, inPlaceClassrooms.Count);
        ////        ClassroomSpace selectedClass = inPlaceClassrooms[randomIndex] as ClassroomSpace;
        ////        //record the selceted class
        ////        classlabPairList.Add(new ClassLabPair(selectedClass, lab));
        ////        lab.SetCurrentOriginalClass(selectedClass);
        ////        //have the class send the students to the lab
        ////        selectedClass.SendClassToLab(lab);
        ////        inPlaceClassrooms.Remove(selectedClass);
        ////        lab.StartLab();
        ////        Debug.Log($"Sending {selectedClass} to {lab.name}");
        ////    }
        ////}

        ////void SendClassesBackFromLabs()
        ////{
        ////    foreach (ClassLabPair classLabPair in classlabPairList.ToArray())
        ////    {
        ////        ReturnClassFromLab(classLabPair);
        ////        classlabPairList.Remove(classLabPair);
        ////    }
        ////}

        ////void ReturnClassFromLab(ClassLabPair classLabPair)
        ////{
        ////    classLabPair.lab.EndLab(classLabPair.classroom);
        ////    inPlaceClassrooms.Add(classLabPair.classroom);
        ////    classLabPair.classroom.RecieveStudents();
        ////}
        ///*
        //void ReplaceClassTeachers()
        //{
        //    //Debug.Log("calling replace teachers");
        //    teacherPoolController.ShuffleSchoolTeachers();
        //    teacherspool = teacherPoolController.GetSchoolTeachers();
        //    foreach (ITeacherAI teacher in teacherspool)
        //    {
        //        teacher.SetInClassroomto(false);
        //        teacher.ClearClassRoom();
        //    }

        //    for (int i = 0; i < subspaces.classrooms.Length; i++)
        //    {
        //        teacherspool[i].AssignClassRoom(subspaces.classrooms[i]);
        //        teacherspool[i].SetInClassroomto(true);
        //    }

        //    foreach (ITeacherAI teacher in teacherspool)
        //    {
        //        if (teacher.IsInClassroom())
        //        {
        //            teacher.GoToClassRoom();
        //        }
        //        else
        //        {
        //            teacher.GoToTeachersRoom();
        //        }
        //    }
        //}
        //*/


    }
}
