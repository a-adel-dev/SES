using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigator : MonoBehaviour
{
    float timer = 0;
    Settings settings;
    int simulationStep;
    Pupil[] pupils;
    int bathroomBreakChance;

    
    
    // Start is called before the first frame update
    void Start()
    {
        settings = FindObjectOfType<Settings>();
        simulationStep = settings.GetSimulationStep();
        pupils = settings.GetAllPupils();
        bathroomBreakChance = settings.GetBathroomBreakChacne();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        timer += Time.deltaTime;
        if (timer > simulationStep)
        {
            foreach (Pupil pupil in pupils)
            {
                var agentController = pupil.GetComponent<AgentController>();
                if (agentController.GetInClassroom())
                {
                    var chance = Random.Range(0, 100);
                    if (chance < bathroomBreakChance)
                    {
                        Space closestBathroom = FindNearestBathroom(pupil);

                        Debug.Log(pupil + " is moving , chance is: " + chance);
                        agentController.SetDestination(closestBathroom.transform.position);
                    }
                }

                //check if pupil in Space (bathroom) , then distribute to a subspace(toilet)
                
            }
            timer -= simulationStep;
        }

        foreach (Pupil pupil in pupils)
        {
            var agentController = pupil.GetComponent<AgentController>();
            if (agentController.GetInBathroom() && !agentController.GetInToilet())
            {
                Space closestBathroom = FindNearestBathroom(pupil);
                agentController.SetDestination(closestBathroom.GetAvailableSubspace().position);
                break;
                
            }
        }
    }

    private Space FindNearestBathroom(Pupil pupil)
    {
        var bathroomLocations = GetComponent<Locations>().GetBathroomLocations();
        var closestBathroom = bathroomLocations[0];
        var prevBathroom = bathroomLocations[0];
        foreach (Space bathroom in bathroomLocations)
        {

            if (Vector3.Distance(pupil.transform.position, bathroom.transform.position) <
                Vector3.Distance(pupil.transform.position, prevBathroom.transform.position))
            {
                closestBathroom = bathroom;
            }
        }

        return closestBathroom;
    }

   
}
