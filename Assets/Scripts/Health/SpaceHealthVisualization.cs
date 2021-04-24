using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceHealthVisualization : MonoBehaviour
{
    Material visualizationMat;
    SpaceHealth space;
    Renderer planeRenderer;
    // Start is called before the first frame update
    private void Start()
    {
        Transform parentClass = this.transform.parent;
        space = parentClass.GetComponent<SpaceHealth>();
        planeRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        planeRenderer.material.SetColor("_Color", new Color(1, 0, 0, (Mathf.Min(space.concentration * .001f,0.6f))));
        //Debug.Log(space.Concentration * .001f);
    }
}
