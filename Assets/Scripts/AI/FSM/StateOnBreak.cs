﻿using UnityEngine;
namespace SES.AIControl.FSM
{
    public class StateOnBreak : StudentBaseState
    {
        public override void EnterState(StudentBehaviorControl behaviorControl)
        {
            Debug.Log($"on break");
        }

        public override void OnTriggerEnter(StudentBehaviorControl behaviorControl)
        {
            throw new System.NotImplementedException();
        }

        public override void Update(StudentBehaviorControl behaviorControl)
        {
            throw new System.NotImplementedException();
        }
    }
}