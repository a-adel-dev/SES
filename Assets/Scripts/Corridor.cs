using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Corridor : MonoBehaviour
{
    bool classesInsession = false;
    SchoolManager schoolManager;
    List<Spot> pointsOfInterest = new List<Spot>();
    List<AI> pupilsInCorridors = new List<AI>();
    
    // Start is called before the first frame update
    void Start()
    {
        schoolManager = FindObjectOfType<SchoolManager>();
        foreach (Transform child in transform)
        {
            pointsOfInterest.Add(child.GetComponent<Spot>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        classesInsession = schoolManager.classInSession;

        
    }





    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pupil"))
        {
            pupilsInCorridors.Add(other.GetComponent<AI>()) ;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pupil"))
        {
            pupilsInCorridors.Remove(other.GetComponent<AI>());
        }
    }
}
