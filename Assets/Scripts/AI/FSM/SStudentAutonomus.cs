using UnityEngine;
using SES.Core;
using Panda;


namespace SES.AIControl.FSM
{
    public class SStudentAutonomus : StudentBaseState
    {
        PandaBehaviour bT;
        public override void EnterState(StudentBehaviorControl behaviorControl)
        {
            bT = behaviorControl.GetComponent<PandaBehaviour>();
            //behaviorControl.ResumeAgent();
            Debug.Log($"Autonomus");
            //activiate panda
            bT.enabled = true;
        }

        public override void OnTriggerEnter(StudentBehaviorControl behaviorControl)
        {

        }

        public override void Update(StudentBehaviorControl behaviorControl)
        {

        }

        public override string ToString()
        {
            return "Autonomus";
        }

    }
}