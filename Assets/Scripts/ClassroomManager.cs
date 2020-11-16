using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassroomManager : SchoolSpaceManager
{
    Location board;
    List<Location> lockers = new List<Location>();

    public void AssignBoard(Location board)
    {
        this.board = board;
    }
    
    public void AddLocker(Location locker)
    {
        lockers.Add(locker);
    }

    public string ShowSpotStats(Classroom classroom)
    {
        string displayText = string.Format(classroom.gameObject.name + "-----------------------" + "\n");
        displayText += board.spotManager.ShowSpotStats();
        foreach (Location locker in lockers)
        {
            displayText += locker.spotManager.ShowSpotStats();
        }
        return displayText;
    }
}
