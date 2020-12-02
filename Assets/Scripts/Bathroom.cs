using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bathroom : MonoBehaviour
{
    bool classesInsession = false;
    SchoolManager schoolManager;
    List<Spot> toilets = new List<Spot>();

    // Start is called before the first frame update
    void Start()
    {
        schoolManager = FindObjectOfType<SchoolManager>();
    }

    // Update is called once per frame
    void Update()
    {
        classesInsession = schoolManager.classInSession;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Toilet"))
        {
            toilets.Add(other.GetComponent<Spot>());
        }
    }
}
