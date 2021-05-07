namespace SES.AIControl.FSM
{
    public abstract class StudentBaseState 
    {
        public abstract void EnterState(StudentBehaviorControl behaviorControl);
        public abstract void Update(StudentBehaviorControl behaviorControl);

        public abstract void OnTriggerEnter(StudentBehaviorControl behaviorControl);
    }
}