using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SES.Core;

namespace SES.Spaces
{
    public class POI : MonoBehaviour, ISpace
    {
        //    List<IAI> pupilsNearPOI = new List<IAI>();
        //    public float timeStep;
        //    [SerializeField] float pOIStoppingDistance = 3f;
        //    [SerializeField] float maximumWaitingTime = 5f;

        //    IEnumerator StopPupils()
        //    {
        //        foreach (IAI student in pupilsNearPOI)
        //        {
        //            Transform studentLocation = student.GetGameObject().transform;
        //            NavMeshAgent pupilNavMeshAgent = student.GetGameObject().GetComponent<NavMeshAgent>();
        //            if (Vector3.Distance(studentLocation.position, transform.position) < pOIStoppingDistance)
        //            {
        //                pupilNavMeshAgent.isStopped = true;
        //                yield return new WaitForSeconds(Random.Range(1f, maximumWaitingTime) * timeStep);
        //                pupilNavMeshAgent.isStopped = false;
        //                yield return new WaitForSeconds(maximumWaitingTime);
        //            }
        //        }
        //        yield return new WaitForSeconds(maximumWaitingTime * timeStep);
        //    }

        //    private void OnTriggerEnter(Collider other)
        //    {
        //        if (other.CompareTag("Pupil"))
        //        {
        //            pupilsNearPOI.Add(other.GetComponent<IAI>());
        //            other.GetComponent<IAI>().SetNearPOI(true);
        //            other.GetComponent<IAI>().AssignSpot(GetComponent<Spot>());
        //        }
        //    }

        //    private void OnTriggerExit(Collider other)
        //    {
        //        if (other.CompareTag("Pupil"))
        //        {
        //            pupilsNearPOI.Remove(other.GetComponent<IAI>());
        //            other.GetComponent<IAI>().SetNearPOI(false);
        //        }
        //    }

        //    public void SetTimeStep(float _timeStep)
        //    {
        //        timeStep = _timeStep;
        //    }
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
