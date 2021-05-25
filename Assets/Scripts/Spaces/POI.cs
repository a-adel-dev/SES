using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SES.Core;

namespace SES.Spaces
{
    public class POI : MonoBehaviour, ISpace
    {
        public GameObject GetGameObject()
        {
            return gameObject;
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
