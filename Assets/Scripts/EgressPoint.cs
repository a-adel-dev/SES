using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EgressPoint : MonoBehaviour
{
    /// <summary>
    /// List of classroom whose staircase exit is this object
    /// </summary>
    [Tooltip("List of classroom whose staircase exit is this object")]
    [SerializeField] List<Classroom> associatedClassrooms;
    SchoolManager schoolmanager;


    private void Start()
    {
        schoolmanager = FindObjectOfType<SchoolManager>();
    }


    /// <summary>
    /// Prompt associated classes to send their respective pupils to this exit point
    /// </summary>
    /// <param name="waitingTime">The time between the exit of each class</param>
    public void RecallClasses(int waitingTime)
    {
        Debug.Log($"{this.name} recalling classes");
        if (waitingTime == 0)
        {
            MoveAllClasses();
        }
        else
        {
            StartCoroutine(MoveClasses(waitingTime));
        }
    }


    /// <summary>
    /// Prompt classes to send their pupils to this exit point with cooldown timer
    /// </summary>
    /// <param name="classroom">target classroom</param>
    /// <param name="waitingTime">cooldown time</param>
    IEnumerator MoveClasses(int waitingTime)
    {
        foreach (Classroom classroom in associatedClassrooms)
        {
            classroom.SendClassOutOfFloor(this.transform.position);
            yield return new WaitForSeconds(waitingTime * schoolmanager.timeStep);
        }
    }
    /// <summary>
    /// Prompt classes to send their pupils to this exit point all at once
    /// </summary>
    public void MoveAllClasses()
    {
        foreach (Classroom classroom in associatedClassrooms)
        {
            classroom.SendClassOutOfFloor(this.transform.position);
        }
    }
}
