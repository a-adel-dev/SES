using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class POI : MonoBehaviour
{
    List<AI> pupilsNearPOI = new List<AI>();
    SchoolManager schoolManager;
    float timeStep;
    // Start is called before the first frame update
    void Start()
    {
        schoolManager = FindObjectOfType<SchoolManager>();
    }

    // Update is called once per frame
    void Update()
    {
        timeStep = schoolManager.GetTimeStep();
        StartCoroutine(StopPupils());
    }

    IEnumerator StopPupils()
    {
        foreach (AI pupil in pupilsNearPOI)
        {
            if (Vector3.Distance(pupil.transform.position, transform.position) < 3f)
            {
                pupil.GetComponent<NavMeshAgent>().isStopped = true;
                yield return new WaitForSeconds(2f * timeStep);
                pupil.GetComponent<NavMeshAgent>().isStopped = false;
                yield return new WaitForSeconds(3f);
            }
        }
        yield return new WaitForSeconds(2f * timeStep);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pupil"))
        {
            pupilsNearPOI.Add(other.GetComponent<AI>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pupil"))
        {
            pupilsNearPOI.Remove(other.GetComponent<AI>());
        }
    }
}
