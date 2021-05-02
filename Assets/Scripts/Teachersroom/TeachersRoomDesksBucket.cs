using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeachersRoomDesksBucket : MonoBehaviour
{
    List<Spot> desks;
    
    // Start is called before the first frame update
    void Start()
    {  
        desks = GetComponent<TeacherSpawner>().GetDesks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Spot GetAvailableDesk()
    {
        List<Spot> availableDesks = new List<Spot>(); 
        foreach (Spot desk in desks)
        {
            if (desk.ISpotAvailable())
            {
                availableDesks.Add(desk);
            }
        }

        Spot selectedDesk = availableDesks[Random.Range(0, availableDesks.Count)];
        return selectedDesk;
    }


}
