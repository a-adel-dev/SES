using UnityEngine;
namespace SES.AIControl.FSM
{
    public class SStudentDoingActivity : StudentBaseState
    {
        public override void EnterState(StudentBehaviorControl behaviorControl)
        {
            //Debug.Log($"Doing activity");
            behaviorControl.ResumeAgent();
        }

        public override void OnTriggerEnter(StudentBehaviorControl behaviorControl)
        {

        }

        public override void Update(StudentBehaviorControl behaviorControl)
        {
            Vector3 board = new Vector3(behaviorControl.mainClassroom.classroomSubSpaces.board.transform.position.x,
                                         0,
                                         behaviorControl.mainClassroom.classroomSubSpaces.board.transform.position.z);
            behaviorControl.NavigateTo(board);
        }

        public override string ToString()
        {
            return "Doing activity";
        }
    }
}