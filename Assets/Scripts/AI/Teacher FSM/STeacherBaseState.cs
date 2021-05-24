namespace SES.AIControl.FSM
{
    public abstract class STeacherBaseState
    {
        public abstract void EnterState(TeacherBehaviorControl behaviorControl);
        public abstract void Update(TeacherBehaviorControl behaviorControl);
    }
}