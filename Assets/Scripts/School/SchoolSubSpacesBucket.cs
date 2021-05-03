using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SchoolSubSpacesBucket : MonoBehaviour
{
    public Classroom[] classrooms;
    public Bathroom[] bathrooms;
    public Corridor[] corridors;
    public Teachersroom[] teachersrooms;
    public Lab[] labs;
    public EgressPoint[] staircases;

    SchoolManager schoolManager;

    // Use this for initialization
    void Awake()
    {
        schoolManager = GetComponent<SchoolManager>();
        AllocateSubSpaces();
    }

    private void AllocateSubSpaces()
    {
        classrooms = FindObjectsOfType<Classroom>();
        bathrooms = FindObjectsOfType<Bathroom>();
        corridors = FindObjectsOfType<Corridor>();
        teachersrooms = FindObjectsOfType<Teachersroom>();
        labs = FindObjectsOfType<Lab>();
        staircases = FindObjectsOfType<EgressPoint>();

    }

    public Bathroom GetNearestBathroom(AI pupil)
    {
        Bathroom nearestBathroom = null;
        float distance = Mathf.Infinity;
        Vector3 pupilPos = pupil.transform.position;
        //NavMeshPath path = new NavMeshPath();
        foreach (Bathroom bathroom in schoolManager.subspaces.bathrooms)
        {
            if (Vector3.Distance(bathroom.transform.position, pupil.transform.position) < distance)
            {
                distance = Vector3.Distance(bathroom.transform.position, pupilPos);
                nearestBathroom = bathroom;
            }
            /*
            //Debug.Log(NavMesh.CalculatePath(pupilPos, bathroom.transform.position, NavMesh.AllAreas, path));
            Vector3 bathroomPos = bathroom.transform.position;
            NavMesh.CalculatePath(pupilPos, bathroomPos, NavMesh.AllAreas, path);
            
            while (!(path.status == NavMeshPathStatus.PathComplete))
            {
                NavMeshHit hit;
                NavMesh.SamplePosition(bathroomPos, out hit, 1, NavMesh.AllAreas);
                bathroomPos = hit.position;
                NavMesh.CalculatePath(pupilPos, bathroomPos, NavMesh.AllAreas, path);
            }
            
            if (PathLength(path) < distance)
            {
                nearestBathroom = bathroom;
                distance = PathLength(path);
                Debug.Log(distance);
            }
            */
        }
        return nearestBathroom;
    }

    float PathLength(NavMeshPath path)
    {
        if (path.corners.Length < 2)
            return 0;

        Vector3 previousCorner = path.corners[0];
        float lengthSoFar = 0.0F;
        int i = 1;
        while (i < path.corners.Length)
        {
            Vector3 currentCorner = path.corners[i];
            lengthSoFar += Vector3.Distance(previousCorner, currentCorner);
            previousCorner = currentCorner;
            i++;
        }
        return lengthSoFar;
    }

}
