using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SES.Core;

namespace SES.Spaces
{
    public class EgressPoint : MonoBehaviour, ISpace
    {
        private void Update()
        {
            //check for collision with students
            //separate the healthy from the unhealthy
        }

        /// <summary>
        /// List of classroom whose staircase exit is this object
        /// </summary>
        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public Spot RequestDesk(IAI student)
        {
            throw new System.NotImplementedException();
        }
    }
}