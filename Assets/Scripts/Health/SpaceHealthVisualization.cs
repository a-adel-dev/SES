using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceHealthVisualization : MonoBehaviour
{
    SpaceHealth space;
    Renderer planeRenderer;
    GeneralHealthParamaters healthParamaters;
    // Start is called before the first frame update
    private void Start()
    {
        Transform parentClass = this.transform.parent;
        space = parentClass.GetComponent<SpaceHealth>();
        planeRenderer = GetComponent<Renderer>();
        healthParamaters = FindObjectOfType<GeneralHealthParamaters>();
    }

    // Update is called once per frame
    void Update()
    {
        planeRenderer.material.SetColor("_BaseColor", new Color(1, 0, 0, (Mathf.Min(space.concentration * healthParamaters.spaceInfectionThreshold,0.6f))));
        //Debug.Log(space.Concentration * .001f);
    }
}
