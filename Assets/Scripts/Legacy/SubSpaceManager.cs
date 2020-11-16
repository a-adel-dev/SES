using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubSpace
{
    public SchoolSubSpace space { get; set; }
    public bool available { get; set; }
    public AI agent { get; set; }

    public string name;
    
    public SubSpace(SchoolSubSpace space)
    {
        this.space = space;
        available = true;
        agent = null;
        name = space.name;
    }

    public SubSpace(SchoolSubSpace space, AI agent)
    {
        this.space = space;
        available = false;
        this.agent = agent;
        name = space.name;
    }
}

public class SubspaceManager
{
    private List<SubSpace> subSpaces = new List<SubSpace>(); //list of all subspaces
    private List<SubSpace> availableSubspaces = new List<SubSpace>(); // list of avbailable subspaces


    public void AddAvaialableSpace(SchoolSubSpace subspace) // adds an available subspace to both lists
    {
        SubSpace subSpace = new SubSpace(subspace);
        subSpaces.Add(subSpace);
        availableSubspaces.Add(subSpace);
    }
    
    public void AddOccupiedSpace(SchoolSubSpace subspace, AI agent)
    {
        SubSpace space = new SubSpace(subspace, agent);
        subSpaces.Add(space);
    }
   
    public SubSpace PopAvailableSubSpace(AI agent)
    {
        if (availableSubspaces.Count == 0)
            return null;
        else
        {
            var randomSpot = Random.Range(0, subSpaces.Count);
            SubSpace availableSubspace = availableSubspaces[randomSpot]; //needs some kind of transaction crapola
            availableSubspace.agent = agent;
            availableSubspace.available = false;
            availableSubspaces.RemoveAt(randomSpot);
            return availableSubspace; 
        }
    }

    public void ReleaseSpace(AI agent)
    {
        foreach (SubSpace subspace in subSpaces)
        {
            if (ReferenceEquals(subspace.agent,agent))
            {
                availableSubspaces.Add(subspace);
                subspace.agent = null;
                subspace.available = true;
            }
        }
    }

    public int GetAvailableSubSpacesCount()
    {
        return availableSubspaces.Count;
    }

    public void PrintAvailableSpaces()
    {
        for (int i = 0; i < subSpaces.Count ; i++)
        {
            if (subSpaces[i].available)
                Debug.Log("Available space is " + subSpaces[i].space + ": " + availableSubspaces.Count);
        }
    }

    public string ShowStats()
    {
        string text = "";
        for (int i = 0; i < availableSubspaces.Count; i++)
        {
            text += string.Format("space: {0} is {1}\n", availableSubspaces[i].space, availableSubspaces[i].available);
        }
        return text;  
    }
    public void SpaceVisualizer()
    {
        string textToDisplay = "";
        foreach (SubSpace space in subSpaces)
        {
            textToDisplay += string.Format("Space | " + space.name +"\n");
        }
        Debug.Log(textToDisplay);
    }
}

public class SpaceManager
{
    //list of spaces
    List<SchoolSpace> spaces = new List<SchoolSpace>();

    public void AddSpace(SchoolSpace space)
    {
        spaces.Add(space);
    }

    public List<SchoolSpace> GetSpacesOfType(string type)
    {
        List<SchoolSpace> spacesOfType = new List<SchoolSpace>();
        foreach (SchoolSpace space in spaces)
        {
            if (space.CompareTag(type))
            {
                spacesOfType.Add(space);
            }
        }
        return spacesOfType;
    }


}

