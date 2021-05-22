using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SES.Core;
using SES.AIControl;
using UnityEngine.Events;

namespace SES.Spaces
{
    public class EgressPoint : MonoBehaviour, ISpace
    {
        [SerializeField] GameObject HealthyCollector;
        [SerializeField] GameObject InfectedCollector;
        StudentEgressEvent egressEvent = new StudentEgressEvent();
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

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<StudentBehaviorControl>())
            {
                //if healthy
                //transfer student to healthy collector space
                other.GetComponent<StudentBehaviorControl>().IdleAgent();
                other.gameObject.GetComponent<MeshRenderer>().enabled = false;
                other.gameObject.transform.position = HealthyCollector.transform.position;
                //invoke event
                egressEvent.Invoke();

                //if not healthy
                //transfer student to infected collector space
                //invoke event
            }
        }

        public void AddStudentEgressListener(UnityAction listener)
        {
            egressEvent.AddListener(listener);
        }
    }
}