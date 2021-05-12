using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SES.Core;

namespace SES.Spaces
{
    public class EgressPoint : MonoBehaviour, ISpace
    {
        /// <summary>
        /// List of classroom whose staircase exit is this object
        /// </summary>
        public GameObject GetGameObject()
        {
            throw new System.NotImplementedException();
        }

        public Spot RequestDesk(IAI student)
        {
            throw new System.NotImplementedException();
        }
    }
}