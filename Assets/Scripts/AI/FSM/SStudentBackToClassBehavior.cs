using SES.Core;
using UnityEngine;

namespace SES.AIControl.FSM
{
    public class SStudentBackToClassBehavior : StudentBaseState
    {
        float timer = 0;
        bool stopForPOI = false;
        int POIChance = 1;
        public override void EnterState(StudentBehaviorControl behaviorControl)
        {
            behaviorControl.NavigateTo(behaviorControl.currentClassroom.classroomSubSpaces.entrance.transform.position);
        }

        public override void Update(StudentBehaviorControl behaviorControl)
        {
            PassTime();
            if (behaviorControl.nearPOI && behaviorControl.inCorridor && behaviorControl.visitedPOI == false && stopForPOI)
            {
                behaviorControl.StopForPOI();
            }

            if (behaviorControl.nav.remainingDistance <= behaviorControl.nav.stoppingDistance &&
                behaviorControl.nav.pathPending == false)
            {
                behaviorControl.currentClassroom.studentsBucket.ReceiveStudent(behaviorControl);
                behaviorControl.NavigateTo(behaviorControl.currentDesk.transform.position);
                behaviorControl.currentClassroom.classScheduler.RequestStatus(behaviorControl);
            }
        }

        public override string ToString()
        {
            return "Going Back To Class";
        }

        private void PassTime()
        {
            timer += Time.deltaTime;
            if (timer >= SimulationParameters.timeStep)
            {
                timer -= SimulationParameters.timeStep;
                stopForPOI = Random.Range(1, 10) <= POIChance;
            }
        }
    }
}