using System.Collections;
using UnityEngine;
using SES.Core;

namespace SES.AIControl
{
    public class StudentAI : MonoBehaviour, IStudentAI
    {
        StudentBehaviorControl behavior;

        private void Start()
        {
            behavior = GetComponent<StudentBehaviorControl>();
        }

        public void SetBehavior(StudentState state)
        {
            behavior.SetStudentState(state);
        }







        #region General methods
        public Transform GetTransform()
        {
            return transform;
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public bool IsStudent()
        {
            return (GetComponent<StudentAI>());
        }

        public bool IsTeacher()
        {
            return (GetComponent<TeacherAI>());
        }

        #endregion

        

        #region interface contested methods

        public void AssignLab(ISpace lab)
        {
            throw new System.NotImplementedException();
        }

        public void AssignLabPosition(Vector3 position)
        {
            throw new System.NotImplementedException();
        }

        public void AssignSpot(Spot spot)
        {
            throw new System.NotImplementedException();
        }

        public void BackToDesk()
        {
            throw new System.NotImplementedException();
        }

        public void BackToOriginalClassroom()
        {
            throw new System.NotImplementedException();
        }

        public void ClearCurrentLab()
        {
            throw new System.NotImplementedException();
        }

        public void Enterlab(ISpace lab)
        {
            throw new System.NotImplementedException();
        }



        public void GetLabPosition(ISpace lab)
        {
            throw new System.NotImplementedException();
        }

        

        public void GoToLab()
        {
            throw new System.NotImplementedException();
        }

        public void GuideTo(Vector3 vector3)
        {
            throw new System.NotImplementedException();
        }

        public void IncreaseClearenceChance()
        {
            throw new System.NotImplementedException();
        }

        public bool IsInfected()
        {
            throw new System.NotImplementedException();
        }



        public void MoveTo(Vector3 exit)
        {
            throw new System.NotImplementedException();
        }

        public Spot ReleaseSpot()
        {
            throw new System.NotImplementedException();
        }

        public void ResetClearenceChance()
        {
            throw new System.NotImplementedException();
        }

        public void ResetPupil()
        {
            throw new System.NotImplementedException();
        }

        public void RestrictPupil()
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

        public void SetCurrentLab(ISpace lab)
        {
            throw new System.NotImplementedException();
        }

        public void SetNearPOI(bool status)
        {
            throw new System.NotImplementedException();
        }

        public void setStoppingDistance(float v)
        {
            throw new System.NotImplementedException();
        }

        public void SetStudentLocationTo(StudentState location)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}