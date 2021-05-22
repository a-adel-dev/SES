using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SES.Core;
using SES.Spaces;
using SES.Spaces.Classroom;

namespace SES.School
{
    public class SchoolSubSpacesBucket : MonoBehaviour
    {
        private ClassroomSpace[] totalClassrooms;
        public Bathroom[] bathrooms { get; set; }
        public Corridor[] corridors { get; set; }
        public Teachersroom[] teachersrooms { get; set; }
        public Lab[] labs { get; set; }
        public EgressPoint[] staircases { get; set; }
        public List<ClassroomSpace> classrooms { get; set; } = new List<ClassroomSpace>();

        // Use this for initialization
        public void Initialize()
        {
            AllocateSubSpaces();
            PopulateWorkingClassrooms();
        }

        private void AllocateSubSpaces()
        {
            totalClassrooms = FindObjectsOfType<ClassroomSpace>();
            bathrooms = FindObjectsOfType<Bathroom>();
            corridors = FindObjectsOfType<Corridor>();
            teachersrooms = FindObjectsOfType<Teachersroom>();
            labs = FindObjectsOfType<Lab>();
            staircases = FindObjectsOfType<EgressPoint>();
        }

        private void PopulateWorkingClassrooms()
        {
            if (SimulationParameters.schoolHalfCapacity)
            {
                for (int i = 0; i < totalClassrooms.Length; i = i + 2)
                {
                    classrooms.Add(totalClassrooms[i]);
                }
            }
            else
            {
                foreach (ClassroomSpace classroom in totalClassrooms)
                {
                    classrooms.Add(classroom);
                }
            }
        }

        public Bathroom GetNearestBathroom(IAI agent)
        {
            Bathroom nearestBathroom = null;
            float distance = Mathf.Infinity;
            Vector3 pupilPos = agent.GetGameObject().transform.position;
            //NavMeshPath path = new NavMeshPath();
            foreach (Bathroom bathroom in bathrooms)
            {
                if (Vector3.Distance(bathroom.transform.position, agent.GetGameObject().transform.position) < distance)
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
}
