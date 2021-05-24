using UnityEngine;
using SES.Core;

namespace SES.AIControl.FSM
{
    class STeacherGoingToTeacherroom: STeacherBaseState
    {
        float timer = 0;
        bool stopForPOI = false;
        int POIChance = 1;

        public override void EnterState(TeacherBehaviorControl behaviorControl)
        {
            behaviorControl.NavigateTo(behaviorControl.teacherroom.GetGameObject().transform.position);
        }

        public override void Update(TeacherBehaviorControl behaviorControl)
        {
            PassTime();
            if (behaviorControl.nearPOI && behaviorControl.inCorridor && behaviorControl.visitedPOI == false && stopForPOI)
            {
                behaviorControl.StopForPOI();
            }

            if (behaviorControl.nav.remainingDistance <= behaviorControl.nav.stoppingDistance &&
                behaviorControl.nav.pathPending == false)
            {
                behaviorControl.Rest();
            }
        }


        public override string ToString()
        {
            return "Going to rest";
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
