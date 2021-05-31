
using UnityEngine;
using SES.Core;


namespace SES.Health
{
    public class SpaceHealthVisualization : MonoBehaviour
    {
        SpaceHealth space;
        Renderer planeRenderer;

        private void Start()
        {
            Transform parentClass = this.transform.parent;
            space = parentClass.GetComponent<SpaceHealth>();
            planeRenderer = GetComponent<Renderer>();
        }

        // Update is called once per frame
        void Update()
        {
            planeRenderer.material.SetColor("_BaseColor", new Color(1, 0, 0, (Mathf.Min(space.Concentration * SimulationDefaults.SpaceInfectionThreshold, 0.6f))));
            //Debug.Log(space.Concentration * .001f);
        }
    }
}
