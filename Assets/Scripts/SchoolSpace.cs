using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SchoolSpace : MonoBehaviour
{
    SubspaceManager subSpaceManager = new SubspaceManager();
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

    public bool isSubSpaceAvailable()
    {
        if (subSpaceManager.GetAvailableSubSpacesCount() > 0)
            return true;
        else
            return false;
    }

    public SchoolSubSpace AssignSpaceToAgent(AI agent)
    {
        SchoolSubSpace subspace = subSpaceManager.GetAvailableSubSpace(agent);
        return subspace;
    }

    public void ReleaseSpace(AI agent)
    {
        subSpaceManager.ReleaseSpace(agent);
    }

    public void MonitorAvailableSpaces()
    {
        text.text = subSpaceManager.ShowStats();
    }

}
