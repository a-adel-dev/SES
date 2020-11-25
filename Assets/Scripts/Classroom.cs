using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classroom : MonoBehaviour
{
    GameObject board;
    List<Spot> lockers = new List<Spot>();
    List<Spot> boardSpots = new List<Spot>();
    List<Spot> desks = new List<Spot>();
    List<AI> pupils = new List<AI>();
    Vector3 boardDirection;
    bool spawned = false;
    int counter = 0;
    

    Queue<AI> availablePupils = new Queue<AI>();

    [SerializeField] GameObject pupilPrefab;
    [SerializeField] GameObject teacherPrefab;


    //temp variables
    public bool move = false;

    private void Start()
    {
        
    }
    private void Update()
    {
        SpawnPupils();
        MoveFirstPupil();
    }

    private void MoveFirstPupil()
    {
        if (move)
        {
            pupils[0].Move();
        }
    }

    private void SpawnPupils()
    {
        if (spawned)
            return;
        foreach (Spot deskspot in desks)
        {
            GameObject pupil = Instantiate(pupilPrefab,
                        deskspot.gameObject.transform.position,
                        Quaternion.identity) as GameObject;
            
            pupil.transform.LookAt(board.gameObject.transform);
            pupils.Add(pupil.GetComponent<AI>());
            pupil.name = "pupil " + counter.ToString();
            counter++;
        }
        spawned = true;
        ShufflePupils();        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Board"))
        {
            board = other.gameObject;
            boardDirection = (board.transform.position - transform.position );
        }
        else if (other.CompareTag("Locker"))
        {
            lockers.Add(other.GetComponent<Spot>());
        }
        else if (other.CompareTag("BoardSpot"))
        {
            boardSpots.Add(other.GetComponent<Spot>());
        }
        else if (other.CompareTag("Desk"))
        {
            desks.Add(other.GetComponent<Spot>());
        }
    }

    private void ShufflePupils()
    {
        int listLength = pupils.Count;
        int random;
        AI temp;
        while (--listLength > 0)
        {
            random = UnityEngine.Random.Range(0, listLength);
            temp = pupils[random];
            pupils[random] = pupils[listLength];
            pupils[listLength] = temp;  
        }
        
    }
}
