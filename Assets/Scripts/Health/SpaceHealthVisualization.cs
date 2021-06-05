
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
            space = GetComponent<SpaceHealth>();
            planeRenderer = transform.GetChild(1).GetComponent<Renderer>();
        }

        void Update()
        {
            planeRenderer.material.SetColor("_BaseColor", new Color(1, 0, 0, (Mathf.Min(space.Concentration * SimulationDefaults.SpaceInfectionThreshold, 0.6f))));
        }
    }
}
