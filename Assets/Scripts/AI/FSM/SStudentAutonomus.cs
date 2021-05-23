


namespace SES.AIControl.FSM
{
    public class SStudentAutonomus : StudentBaseState
    {
        int toiletChance = 2;
        public override void EnterState(StudentBehaviorControl behaviorControl)
        {
            //behaviorControl.BehaviorGoToLocker();
            behaviorControl.BehaviorGoToBathroom();
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