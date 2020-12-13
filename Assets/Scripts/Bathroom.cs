using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bathroom : MonoBehaviour
{
    bool classesInsession = false;
    SchoolManager schoolManager;
    List<Spot> toilets = new List<Spot>();
    List<Spot> availableToilets = new List<Spot>();

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
            availableToilets.Add(other.GetComponent<Spot>());

        }
    }

    public Spot GetAToilet(AI agent)
    {
        if (availableToilets.Count <= 0) { return null; }
        Spot randomToilet = availableToilets[Random.Range(0, availableToilets.Count)];
        Debug.Log("assigning Toilet: " + randomToilet.name + " to: " + agent.gameObject.name);
        randomToilet.FillSpot(agent);
        availableToilets.Remove(randomToilet);
        return randomToilet;
    }

    public void ReleaseToilet(Spot toilet)
    {
        toilet.ClearSpot();
        availableToilets.Add(toilet);
    }
}
