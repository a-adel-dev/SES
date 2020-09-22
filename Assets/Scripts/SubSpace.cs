using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubSpace : Space
{
    bool occupied = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pupil"))
        {
            occupied = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pupil"))
        {
            occupied = false;
        }
    }


    public bool isOccupied()
    {
        return occupied;
    }
}
