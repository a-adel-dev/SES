using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SES.Core;

namespace SES.SimProperties
{
    public class ClassroomEgressController : MonoBehaviour
    {
        [Tooltip("List of classroom whose staircase exit is this object")]
        [SerializeField] List<IClassroom> associatedClassrooms;


        public void RecallClasses(int waitingTime)
        {
            Debug.Log($"{this.name} recalling classes");
            if (waitingTime == 0)
            {
                MoveAllClasses();
            }
            else
            {
                StartCoroutine(MoveClasses(waitingTime));
            }
        }


        /// <summary>
        /// Prompt classes to send their pupils to this exit point with cooldown timer
        /// </summary>
        /// <param name="classroom">target classroom</param>
        /// <param name="waitingTime">cooldown time</param>
        IEnumerator MoveClasses(int waitingTime)
        {
            foreach (IClassroom classroom in associatedClassrooms)
            {
                //classroom.EgressClass(this.transform.position);
                //yield return new WaitForSeconds(waitingTime * timeStep.value);
                yield return null;
            }
        }
        /// <summary>
        /// Prompt classes to send their pupils to this exit point all at once
        /// </summary>
        public void MoveAllClasses()
        {
            //foreach (IClassroom classroom in associatedClassrooms)
            //{
            //    classroom.EgressClass(this.transform.position);
            //}
        }

    }
}