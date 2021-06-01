using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionVisualization : MonoBehaviour
{
    [SerializeField] Material highlightMaterial;
    [SerializeField] Material originalMaterial;
    public LayerMask mask;
    GameObject hitObject;

    private void Update()
    {
        if (hitObject != null)
        {
            hitObject.GetComponent<Renderer>().material = originalMaterial;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo,Mathf.Infinity, mask))
        {
            //Debug.DrawLine(ray.origin, hitInfo.point, Color.red);
            //Debug.Log($"{hitInfo.collider.gameObject.transform.parent.name}");
            hitObject = hitInfo.collider.gameObject;
            hitObject.GetComponent<Renderer>().material = highlightMaterial;
        }
        else
        {

            hitObject = null;
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100, Color.green);
            
        }
    }
}
