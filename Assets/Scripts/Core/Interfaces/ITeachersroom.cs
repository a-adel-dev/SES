using System.Collections.Generic;

namespace SES.Core
{
    public interface ITeachersroom : ISpace
    {
        SpotBucket subspaces { get; set; }
        List<ITeacherAI> teachers { get; set; }
        

        void AddToTeachersInRoom(ITeacherAI teacher);
        void ExitTeacherroom(ITeacherAI teacher);

        
    }
}