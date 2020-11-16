using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classroom : MonoBehaviour
{
    public ClassroomManager classroomManager = new ClassroomManager(); 
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Location>().type == "Board")
            {
                classroomManager.AssignBoard(child.GetComponent<Location>());
            }
            else if (child.GetComponent<Location>().type == "Locker")
            {
                classroomManager.AddLocker(child.GetComponent<Location>());
            }
        }
        Debug.Log(classroomManager.ShowSpotStats(this));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
