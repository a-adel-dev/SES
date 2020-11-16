using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolMajorSpace : MonoBehaviour
{
    public SpaceManager spaceManager = new SpaceManager();
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform) //get all children
        {
            if (child.GetComponent<SchoolSpace>())
            {
                spaceManager.AddSpace(child.GetComponent<SchoolSpace>());
            }
        }
       //spaceManager.ShowSpaces();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
