using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SES.Core;
using SES.Spaces;
using SES.Spaces.Classroom;
using SES.Health;

namespace SES.AIControl
{

    public class TeacherAI : MonoBehaviour, ITeacherAI
    {
        /*
       /// <summary>
       /// checks wheather teacher is currently assigned a classroom
       /// </summary>
       bool inClassroom = false;
       TeacherNavigation teacherNav;
       AgentHealth health;
       public TeacherMovementStyle movementStyle = TeacherMovementStyle.classroom;

       // Start is called before the first frame update
       void Start()
       {
           teacherNav = GetComponent<TeacherNavigation>();
           health = GetComponent<AgentHealth>();
           if (IsInClassroom())
           {
               StartWander(TeacherMovementStyle.restricted);
               health.SetActivityType(ActivityType.LoudTalking);
           }
       }

       // Update is called once per frame
       void Update()
       {
           if (!IsInClassroom())
           {
               StopWandering();
               health.SetActivityType(ActivityType.Talking);
           }
       }

       /// <summary>
       /// Sets the teacher to be in a classroom or out based on the status parameter
       /// </summary>
       /// <param name="status">True if teacher is in a class, false if not</param>
       public void SetInClassroomto(bool status)
       {
           inClassroom = status;
           teacherNav.ClearTeacherDesk();
       }
       /// <summary>
       /// returns true if teacher is currently in class
       /// </summary>
       public bool IsInClassroom()
       {
           return inClassroom;
       }


       /// <summary>
       /// moves the teacher about in a classroom
       /// </summary>
       /// <param name="style">classroom if the teacher is free to move in the class, restricted if the teacher is bound to his area</param>
       private void StartWander(TeacherMovementStyle style)
       {
           teacherNav.SetWandering(true);
           if (style == TeacherMovementStyle.restricted)
           {
               StartCoroutine(teacherNav.Wander());
               movementStyle = TeacherMovementStyle.restricted;
           }
           else if (style == TeacherMovementStyle.classroom)
           {
               StartCoroutine(teacherNav.Wander());
               movementStyle = TeacherMovementStyle.classroom;
           }
       }

       private void StopWandering()
       {
           teacherNav.SetWandering(false);
       }

       public void RestrictClassMovement()
       {
           teacherNav.StopWandering();
           StartWander(TeacherMovementStyle.restricted);
       }

       public void FreeClassMovement()
       {
           teacherNav.StopWandering();
           StartWander(TeacherMovementStyle.classroom);
       }

       public void AssignClassRoom(IClassroom classroomSpace)
       {
           throw new System.NotImplementedException();
       }

       public void GoToClassRoom()
       {
           teacherNav.GoToClassRoom();
       }

       public void GoToTeachersRoom()
       {
           teacherNav.GoToTeachersRoom();
       }

       public GameObject GetGameObject()
       {
           return gameObject;
       }

       public void SetNearPOI(bool status)
       {
           throw new System.NotImplementedException();
       }

       public void AssignSpot(Spot spot)
       {
           throw new System.NotImplementedException();
       }

       public void SetControlledTo(bool state)
       {
           throw new System.NotImplementedException();
       }

       public void ClearCurrentLab()
       {
           throw new System.NotImplementedException();
       }

       public void SetCurrentClass(ISpace classroom)
       {
           teacherNav.AssignClassRoom(classroom as ClassroomSpace);
       }

       public void AssignTeachersRoom(ISpace teachersroom)
       {
           teacherNav.AssignTeachersRoom(teachersroom as Teachersroom);
       }

       public void ClearClassRoom()
       {
           teacherNav.ClearClassRoom();
       }

       public bool IsInfected()
       {
           return health.healthCondition == HealthCondition.infected;
       }

       public bool IsTeacher()
       {
           return GetComponent<TeacherAI>();
       }

       public bool ISStudent()
       {
           return GetComponent<AIControl>();
       }
        */
        public void AssignClassRoom(IClassroom classroomSpace)
        {
            throw new System.NotImplementedException();
        }

        public void AssignOriginalPosition()
        {
            throw new System.NotImplementedException();
        }

        public void AssignSpot(Spot spot)
        {
            throw new System.NotImplementedException();
        }

        public void AssignTeachersRoom(ISpace teachersroom)
        {
            throw new System.NotImplementedException();
        }

        public void ClearClassRoom()
        {
            throw new System.NotImplementedException();
        }

        public GameObject GetGameObject()
        {
            throw new System.NotImplementedException();
        }

        public Transform GetTransform()
        {
            throw new System.NotImplementedException();
        }

        public void GoToClassRoom()
        {
            throw new System.NotImplementedException();
        }

        public void GoToTeachersRoom()
        {
            throw new System.NotImplementedException();
        }

        public void Idle()
        {
            throw new System.NotImplementedException();
        }

        public bool IsInClassroom()
        {
            throw new System.NotImplementedException();
        }

        public bool IsInfected()
        {
            throw new System.NotImplementedException();
        }

        public bool IsStudent()
        {
            throw new System.NotImplementedException();
        }

        public bool IsTeacher()
        {
            throw new System.NotImplementedException();
        }

        public void ResumeAgent()
        {
            throw new System.NotImplementedException();
        }

        public void SetControlledTo(bool state)
        {
            throw new System.NotImplementedException();
        }

        public void SetCurrentClass(ISpace classroom)
        {
            throw new System.NotImplementedException();
        }

        public void SetInClassroomto(bool v)
        {
            throw new System.NotImplementedException();
        }

        public void SetNearPOI(bool status)
        {
            throw new System.NotImplementedException();
        }
    }


}

