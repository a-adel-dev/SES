using System.Collections.Generic;
using UnityEngine;


public class ClassroomsObjectsBucket : MonoBehaviour
{
    [SerializeField]    List<Spot> desks = new List<Spot>();
    [SerializeField]    List<Spot> lockers = new List<Spot>();
    [SerializeField]    List<Spot> boardSpots = new List<Spot>();
    [SerializeField]    BoxCollider teachersSpace;
    [SerializeField]    GameObject board;
    List<Spot> availableLockers;


    private void Start()
    {
        availableLockers = new List<Spot>(lockers);
    }
    public List<Spot> GetClassroomDesks()
    {
        return desks;
    }

    public BoxCollider GetTeacherSpace()
    {
        return teachersSpace;
    }

    public Spot GetLocker()
    {
        if (availableLockers.Count <= 0)
        {
            return null;
        }
        else
        {
            Spot randomLocker;
            int randomIndex = Random.Range(0, availableLockers.Count);
            randomLocker = availableLockers[randomIndex];
            availableLockers.Remove(randomLocker);
            return randomLocker;
        }
    }

    public void ReturnLocker(Spot locker)
    {
        if (!availableLockers.Contains(locker) && lockers.Contains(locker))
        {
            availableLockers.Add(locker);
        }
        else
        {
            Debug.LogError("locker is not in class!");
        }
    }

    private List<Spot> ShuffleSpots(List<Spot> spots)
    {
        int listLength = spots.Count;
        int random;
        Spot temp;
        while (--listLength > 0)
        {
            random = UnityEngine.Random.Range(0, listLength);
            temp = spots[random];
            spots[random] = spots[listLength];
            spots[listLength] = temp;
        }
        return spots;
    }

    public List<Spot> ShuffleBoardSpots()
    {
        return ShuffleSpots(boardSpots);
    }

    public GameObject GetClassBoard()
    {
        return board;
    }
}
