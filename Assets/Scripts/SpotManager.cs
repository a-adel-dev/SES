using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotManager
{
    List<Spot> spots = new List<Spot>();

    public void AddSpot(Spot spot)
    {
        spots.Add(spot);
    }

    bool IsSpotAvailable(Spot spot)
    {
        return spot.available;
    }

    int GetNumSpots()
    {
        return spots.Count;
    }

    int GetNumAvailableSpots()
    {
        int count = 0;
        foreach (Spot spot in spots)
        {
            if (spot.available)
            {
                count++;
            }
        }
        return count;
    }

    Transform GetSpotParent(Spot spot)
    {
        return spot.gameObject.transform.parent;
    }

    public string ShowSpotStats()
    {
        string displayText = "";
        foreach (Spot spot in spots)
        {
            displayText += string.Format(GetSpotParent(spot).name + "| " + spot.name + " is " + spot.IsAvailableText() + " \n");
        }

        return displayText;
    }
 
}
