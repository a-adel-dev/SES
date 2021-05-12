using UnityEngine;

namespace SES.Core
{
    public interface ISpace
    {
        GameObject GetGameObject();
        Spot RequestDesk(IAI agent);
    }
}
