﻿using UnityEngine;
using SES.Core;

namespace SES.AIControl.FSM
{
    public class SStudentBathroomBehavior : StudentBaseState
    {
        public override void EnterState(StudentBehaviorControl behaviorControl)
        {
            if (behaviorControl.CurrentClassroom != null)
            {
                behaviorControl.CurrentClassroom.StudentExitClassroom(behaviorControl);
            }
            else
            {
                behaviorControl.CurrentLab.StudentExitLab(behaviorControl);
            }
            behaviorControl.bathroomToVisit = behaviorControl.school.RequestBathroom(behaviorControl);
            behaviorControl.NavigateTo(behaviorControl.bathroomToVisit.GetGameObject().transform.position);
            behaviorControl.AgentHealth.SetActivityType(ActivityType.Breathing);
        }

        public override void Update(StudentBehaviorControl behaviorControl)
        {
            if (behaviorControl.nav.remainingDistance <= behaviorControl.nav.stoppingDistance &&
                behaviorControl.nav.pathPending == false)
            {
                behaviorControl.VisitToilet();
            }
        }

        public override string ToString()
        {
            return "Going To Bathroom";
        }
    }
}