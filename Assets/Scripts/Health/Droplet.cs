using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Droplet
{
    public float dropletRadius;
    public float virionsConcentration;
    public float dropletVolume { get; private set; }

    
    


    public Droplet(float radius, float concentration)
    {
        dropletRadius = radius;
        virionsConcentration = concentration;
        dropletVolume = Mathf.PI * Mathf.Pow(dropletRadius, 3f);
    }
}
