using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using SES.Core;
using SES.Spaces.Classroom;
using SES.Spaces;
/*
namespace SES.AIControl
{
    public class TeacherNavigation : MonoBehaviour
    {
        NavMeshAgent agent;
        bool wandering = false;
        [SerializeField] float timeStep;
        ClassroomSpace currentClass;
        Lab ownLab;
        public Spot currentTeacherDesk;
        public Teachersroom mainTeacherRoom { get; private set; }


        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            var remaining = (agent.destination - this.transform.position);
            Debug.DrawRay(this.transform.position, remaining, Color.blue);
        }

        /// <summary>
        /// Directs agent to a point, ignoring its hight information
        /// </summary>
        /// <param name="destination">The position of the target location</param>
        public void GuideTo(Vector3 destination)
        {
            agent.SetDestination(new Vector3(destination.x, 0f, destination.z));
        }

        public void GoToClassRoom()
        {
            SetWandering(false);
            GuideTo(currentClass.classroomSubSpaces.board.transform.position);
            SetWandering(true);
        }

        public void GoToTeachersRoom()
        {
            //Debug.Log($"teacher is going to teachersRoom");
            Spot desk = mainTeacherRoom.teacherroomObjects.GetAvailableDesk();
            GuideTo(desk.transform.position);
            desk.FillSpot(GetComponent<TeacherAI>());
        }


        public void SetWandering(bool status)
        {
            wandering = status;
        }

        public IEnumerator Wander()
        {
            while (wandering)
            {
                BoxCollider area = null;
                if (currentClass != null)
                {
                    area = currentClass.classroomSubSpaces.GetTeacherSpace();
                }
                else if (ownLab != null)
                {
                    area = ownLab.GetTeacherSpace();
                }
                Vector3 bounds_min = area.bounds.min;
                Vector3 bounds_max = area.bounds.max;
                float waypoint_x = Random.Range(bounds_min[0], bounds_max[0]);
                float waypoint_z = Random.Range(bounds_min[2], bounds_max[2]);
                Vector3 waypoint = new Vector3(waypoint_x, 0f, waypoint_z);
                GuideTo(waypoint);
                yield return new WaitForSeconds(Random.Range(5f, 20f) * timeStep);
            }
        }

        public void StopWandering()
        {
            StopCoroutine(Wander());
        }

        public void AssignClassRoom(ClassroomSpace classroom)
        {
            currentClass = classroom;
        }

        public void AssignLab(Lab lab)
        {
            ownLab = lab;
        }

        public void AssignTeachersRoom(Teachersroom room)
        {
            mainTeacherRoom = room;
        }

        public void ClearClassRoom()
        {
            currentClass = null;
        }

        public void AssignTeacherDesk(Spot desk)
        {
            currentTeacherDesk = desk;
        }

        public void ClearTeacherDesk()
        {
            currentTeacherDesk = null;
        }
    }
}
*/