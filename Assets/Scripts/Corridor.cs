using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Corridor : MonoBehaviour
{

    List<Spot> pointsOfInterest = new List<Spot>();
    List<AI> pupilsInCorridors = new List<AI>();
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            pointsOfInterest.Add(child.GetComponent<Spot>());
        }
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
