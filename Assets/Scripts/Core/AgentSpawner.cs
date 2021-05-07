using System.Collections.Generic;
using UnityEngine;

namespace SES.Core
{
    public class AgentSpawner : MonoBehaviour
    {
        [SerializeField] GameObject agentPrefab;

        public List<GameObject> SpawnAgents(List<Spot> spawnLocations)
        {
            //int numTeachersTobeSpawned = desksBucket.desks.Count / 2;
            //desksBucket.ShuffleDesks();
            List<GameObject> spawnedAgents = new List<GameObject>();
            int numAgentsToBeSpawned = spawnLocations.Count;
            for (int i = 0; i < numAgentsToBeSpawned; i++)
            {
                GameObject agent = Instantiate(agentPrefab, spawnLocations[i].transform.position, Quaternion.identity);
                spawnedAgents.Add(agent);
                //TeacherAI teacherAgent = teacher.GetComponent<TeacherAI>();
                //teacherAgent.SetInClassroomto(false);
                //teacherspool.AddToTeachersPool(teacherAgent);
                //teacherAgent.AssignTeachersRoom(gameObject.GetComponent<Teachersroom>());
                //teacherAgent.AssignTeacherDesk(desksBucket.desks[i]);
            }
            return spawnedAgents;
        }

        public List<GameObject> SpawnAgents(GameObject _agentPrefab, List<Spot> spawnLocations)
        {
            //int numTeachersTobeSpawned = desksBucket.desks.Count / 2;
            //desksBucket.ShuffleDesks();
            List<GameObject> spawnedAgents = new List<GameObject>();
            int numAgentsToBeSpawned = spawnLocations.Count;
            for (int i = 0; i < numAgentsToBeSpawned; i++)
            {
                GameObject agent = Instantiate(_agentPrefab, ((Component)GetComponent<ISpace>()).transform.position, Quaternion.identity);
                spawnedAgents.Add(agent);
                //TeacherAI teacherAgent = teacher.GetComponent<TeacherAI>();
                //teacherAgent.SetInClassroomto(false);
                //teacherspool.AddToTeachersPool(teacherAgent);
                //teacherAgent.AssignTeachersRoom(gameObject.GetComponent<Teachersroom>());
                //teacherAgent.AssignTeacherDesk(desksBucket.desks[i]);
            }
            return spawnedAgents;
        }
    }
}
