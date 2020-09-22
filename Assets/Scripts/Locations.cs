using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locations : MonoBehaviour
{
    [SerializeField] List<Space> bathroomLocations = new List<Space>();
    
    // Start is called before the first frame update
    void Start()
    {
        foreach (Space space in FindObjectsOfType<Space>())
        {
            if (space.CompareTag("Bathroom"))
            {
                bathroomLocations.Add(space);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Space> GetBathroomLocations()
    {
        return bathroomLocations;
    }
}
