using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space : MonoBehaviour
{
    [SerializeField] bool indoor;
    [SerializeField] float VOLUME;
    [SerializeField] List<Transform> subSpaces = new List<Transform>();
    [SerializeField] List<Transform> nonOccupiedSubSpaces = new List<Transform>();

    int pupilsInSpace = 0; //exposed for debugging
    
    float pupilsToVolumeRatio;



    // Start is called before the first frame update
    void Start()
    {
        var collider = GetComponent<BoxCollider>();
        VOLUME = collider.size.x * collider.size.y * collider.size.z;

        
    }

    // Update is called once per frame
    void Update()
    {
        pupilsToVolumeRatio = pupilsInSpace / VOLUME;
        PopulateNonOccupiedSubSpaces();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<SubSpace>() != null )
        {
            subSpaces.Add(other.transform);
        }

        else if (other.CompareTag("Pupil"))
        {
            pupilsInSpace++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        pupilsInSpace--;
    }

    public bool GetIndoor()
    {
        return indoor;
    }

    public float GetPupilsToVolumeRatio()
    {
        return pupilsToVolumeRatio;
    }

    public Transform GetAvailableSubspace()
    {
        var randomIndex = Random.Range(0, nonOccupiedSubSpaces.Count-1);
        Debug.Log("calling getAvailableSubSpace with a result of " + subSpaces[randomIndex]);
        return subSpaces[randomIndex];
    }
    
    private void PopulateNonOccupiedSubSpaces()
    {
        foreach (Transform transform in subSpaces)
        {
            if (!transform.GetComponent<SubSpace>().isOccupied() && !nonOccupiedSubSpaces.Contains(transform))
            {
                nonOccupiedSubSpaces.Add(transform);
                
            }
        }
    }  
}
