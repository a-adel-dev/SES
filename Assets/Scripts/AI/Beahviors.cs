using System.Collections;
using UnityEngine;
using Panda;
using SES.Core;
using SES.School;

namespace SES.AIControl
{
    public class Beahviors : MonoBehaviour
    {
        PandaBehaviour bT;
        StudentBehaviorControl control;
        Spot lockerToVisit;
        IBathroom bathroomToVisit;
        Spot toiletToVisit;

        ISchool school;

        private void Start()
        {
            bT = GetComponent<PandaBehaviour>();
            control = GetComponent<StudentBehaviorControl>();
            school = FindObjectOfType<SchoolDayProgressionController>();
        }

        [Task]
        void CheckReach()
        {
            if (Task.isInspected)
            {
                Task.current.debugInfo = string.Format("t = {0:0.00}", Time.time);
            }

            if (control.nav.remainingDistance <= control.nav.stoppingDistance && !control.nav.pathPending)
            {
                Task.current.Succeed();
            }
        }

        [Task]
        void RequestStatus()
        {
            control.currentClassroom.classScheduler.RequestStatus(control);
            bT.enabled = false;
            Task.current.Succeed();
        }

        [Task]
        void PickLocker()
        {
            lockerToVisit = control.currentClassroom.RequestLocker(control);
            if (lockerToVisit != null)
            {
                Task.current.Succeed();
            }
            else
            {
                Task.current.Fail();
            }
        }

        [Task]
        void GoToLocker()
        {
            control.NavigateTo(lockerToVisit.transform.position);
            Task.current.Succeed();
        }

        [Task]
        void ReleaseLocker()
        {
            lockerToVisit.ClearSpot();
            lockerToVisit = null;
            Task.current.Succeed();
        }

        [Task]
        void BackToDesk()
        {
            control.NavigateTo(control.currentDesk.transform.position);
            Task.current.Succeed();
        }

        [Task]
        void ExitClassroom()
        {
            control.currentClassroom.ExitClassroom(control);
            Task.current.Succeed();
        }

        [Task]
        void RequestBathroom()
        {
            bathroomToVisit = school.RequestBathroom(control);
            Task.current.Succeed();
        }

        [Task]
        void GoToBathroom()
        {
            control.NavigateTo(bathroomToVisit.GetGameObject().transform.position);
            Task.current.Succeed();
        }

        [Task]
        void RequestToilet()
        {
            toiletToVisit =  bathroomToVisit.RequestToilet(control);
            if (toiletToVisit != null)
            {
                Task.current.Succeed();
            }
            else
            {
                Task.current.Fail();
            }
            
        }

        [Task]
        void GoToToilet()
        {
            control.NavigateTo(toiletToVisit.transform.position);
            Task.current.Succeed();
        }

        [Task]
        void ReleaseToilet()
        {
            bathroomToVisit.ReleaseToilet(toiletToVisit);
            toiletToVisit = null;
            Task.current.Succeed();
        }

        [Task]
        void ExitBathroom()
        {
            bathroomToVisit = null;
            Task.current.Succeed();
        }

        [Task]
        void GoToClass()
        {
            control.NavigateTo(control.currentClassroom.transform.position);
            Task.current.Succeed();
        }

        [Task]
        void EnterClass()
        {
            control.currentClassroom.studentsBucket.ReceiveStudent(control);
            Task.current.Succeed();
        }
    }
}