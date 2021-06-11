using UnityEngine;
using SES.Core;

namespace SES.Spaces
{
    public class Bathroom : MonoBehaviour, IBathroom
    {
        public SpotBucket Toilets { get; set; }

        private void Start()
        {
            Toilets = GetComponent<SpotBucket>();
        }

        public void ReleaseToilet(Spot toilet)
        {
            Toilets.ClearDesk(toilet);
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public Spot RequestToilet(IAI agent)
        {
            return Toilets.GetAvailableDesk(agent);
        }

        public Spot RequestDesk(IAI agent)
        {
            Debug.LogError($"'Request Desk()' Not valid for {this.gameObject.name}");
            return null;
        }

        public Spot RequestLocker(IAI agent)
        {
            Debug.LogError($"'Request Locker()'Not valid for {this.gameObject.name}");
            return null;
        }
    }
}
