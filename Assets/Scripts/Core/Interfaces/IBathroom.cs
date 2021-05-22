using SES.Core;

namespace SES.Core
{
    public interface IBathroom : ISpace
    {
        Spot RequestToilet(IAI agent);
        void ReleaseToilet(Spot toilet);
    }
}