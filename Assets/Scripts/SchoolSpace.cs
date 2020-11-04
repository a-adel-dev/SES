using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SchoolSpace : MonoBehaviour
{
    public SubspaceManager subSpaceManager = new SubspaceManager();
    [SerializeField] Text text;
    

    void Start()
    {        
        foreach (Transform child in transform) //get all children
        {
            if (child.GetComponent<SchoolSubSpace>())
                subSpaceManager.AddAvaialableSpace(child.GetComponent<SchoolSubSpace>());
        }
        //subSpaceManager.PrintAvailableSpaces();
    }

    // Update is called once per frame
    void Update()
    {
        MonitorAvailableSpaces();
    }


    public void MonitorAvailableSpaces()
    {
        text.text = subSpaceManager.ShowStats();
    }

}
