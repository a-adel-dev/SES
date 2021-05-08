using UnityEngine;

namespace SES.Spaces.Classroom
{
    public class SClassroomOnBreak : SClassroomBaseState
    {
        public override void EnterState(ClassroomPeriodSchedular schedular)
        {
            Debug.Log("Class is on a break");
            //Control AI
        }

        public override void Update(ClassroomPeriodSchedular schedular)
        {

        }
    }
}