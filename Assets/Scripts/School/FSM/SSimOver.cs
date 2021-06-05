using SES.Core;


namespace SES.School
{
    public class SSimOver : SSchoolBaseState
    {
        public override void EnterState(SchoolDayProgressionController progressionController)
        {
            progressionController.SchoolState = "Simulation is over";
            progressionController.PauseClasses();
            TotalAgentsBucket.PauseAgents();
            foreach (IStudentAI student in TotalAgentsBucket.GetStudents())
            {
                student.IdleAgent();
            }

            foreach (ITeacherAI teacher in TotalAgentsBucket.GetTeachers())
            {
                teacher.IdleAgent();
            }
        }

        public override void Update(SchoolDayProgressionController progressionController)
        {
            
        }
    }
}