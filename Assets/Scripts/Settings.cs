using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] int simulationStep = 1;
    [SerializeField] int bathroomBreakChance = 10;
    public float INDOORFACTOR = 18.7f;

    Pupil[] pupils;
    float timer = 0;
    float steps = 0f;

    // Start is called before the first frame update
    private void Awake()
    {
        pupils = FindObjectsOfType<Pupil>();
    }

    void Start()
    {
        Debug.Log("Starting");
        
    }

    // Update is called once per frame
    void Update()
    {
        // pupils.AddRange(FindObjectsOfType<Pupil>()); //cast array into a list
        

        timer += Time.deltaTime;
        steps += Time.deltaTime;
        if (timer > simulationStep)
        {
            Debug.Log(" Simulation step No. " + steps/ simulationStep);





            timer -= simulationStep;
        }


    }


    public int GetSimulationStep()
    {
        return simulationStep;
    }

    public Pupil[] GetAllPupils()
    {
        return pupils;
    }

    public int GetBathroomBreakChacne()
    {
        return bathroomBreakChance;
    }
}
