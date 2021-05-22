using SES.Spaces;

namespace SES.Core
{
    public interface ISchool
    {
        Bathroom RequestBathroom(IAI agent);
    }
}