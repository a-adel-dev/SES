using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubSpace
{
    public SchoolSubSpace space { get; set; }
    public bool available { get; set; }
    public AI agent { get; set; }
    public int SubSpaceID;
    
    public SubSpace(SchoolSubSpace space, int ID)
    {
        this.space = space;
        available = true;
        agent = null;
        SubSpaceID = ID;
    }

    public SubSpace(SchoolSubSpace space, AI agent, int ID)
    {
        this.space = space;
        available = false;
        this.agent = agent;
        SubSpaceID = ID;
    }
}

public class SubspaceManager
{
    private List<SubSpace> subspaces = new List<SubSpace>();
    private int availableSpaceCount = 0;
    private int idCount = 0;


    public void AddAvaialableSpace(SchoolSubSpace subspace)
    {
        SubSpace space = new SubSpace(subspace, idCount);
        subspaces.Add(space);
        idCount++;
        //Debug.Log("Adding space " + space.space + "with ID: " + space.SubSpaceID);
        availableSpaceCount++;
        //Debug.Log("Count " + availableSpaceCount);
    }
    
    public void AddOccupiedSpace(SchoolSubSpace subspace, AI agent, int ID)
    {
        SubSpace space = new SubSpace(subspace, agent, ID);
        subspaces.Add(space);
    }
   
    public SchoolSubSpace GetAvailableSubSpace(AI agent)
    {
        if (availableSpaceCount <= 0)
            return null;
        bool spaceNotFound = true; 
        while (spaceNotFound)
        {
            var randomSpot = Random.Range(0, subspaces.Count - 1);
            if (subspaces[randomSpot].available)
            {
                subspaces[randomSpot].available = false;
                subspaces[randomSpot].agent = agent;
                spaceNotFound = false;
                availableSpaceCount--;
                return subspaces[randomSpot].space;  
            }
        }
        return null;
    }

    public void ReleaseSpace(AI agent)
    {
        foreach (SubSpace subspace in subspaces)
        {
            if (ReferenceEquals(subspace.agent,agent))
            {
                subspace.agent = null;
                subspace.available = true;
            }
        }
        availableSpaceCount++;
    }

    public int GetAvailableSubSpacesCount()
    {
        return availableSpaceCount;
    }

    public void PrintAvailableSpaces()
    {
        for (int i = 0; i < subspaces.Count ; i++)
        {
            if (subspaces[i].available)
                Debug.Log("Available space is " + subspaces[i].space + ": " +availableSpaceCount);
        }
    }

    public string ShowStats()
    {
        string text = "";
        for (int i = 0; i < subspaces.Count; i++)
        {
            text += string.Format("space: {0} is {1}\n", subspaces[i].space, subspaces[i].available);
        }
        return text;
        
    }
}

