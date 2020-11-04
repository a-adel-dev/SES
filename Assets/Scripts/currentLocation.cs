using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class currentLocation : MonoBehaviour
{
    public bool inClass { get; private set; }
    public bool inCorridor { get; private set; }
    public bool inBathroom { get; private set; }
    public bool atBoard { get; private set; }
    public bool atToilet { get; private set; }
    public bool atLocker { get; private set; }




    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Classroom"))
        {
            inClass = true;
        }
        else if (other.gameObject.CompareTag("Corridor"))
        {
            inCorridor = true;
        }
        else if (other.gameObject.CompareTag("Bathroom"))
        {
            inBathroom = true;
          
        }

        if (other.gameObject.CompareTag("Board"))
        {
            atBoard = true;
        }
        else if (other.gameObject.CompareTag("Locker"))
        {
            atLocker = true;
        }
        else if (other.gameObject.CompareTag("Toilet"))
        {
            atToilet = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Classroom"))
        {
            inClass = false;
        }
        else if (other.gameObject.CompareTag("Corridor"))
        {
            inCorridor = false;
        }
        else if (other.gameObject.CompareTag("Bathroom"))
        {
            inBathroom = false;
        }

        if (other.gameObject.CompareTag("Board"))
        {
            atBoard = false;
        }
        else if (other.gameObject.CompareTag("Locker"))
        {
            atLocker = false;
        }
        else if (other.gameObject.CompareTag("Toilet"))
        {
            atToilet = false;
        }
    }
}
