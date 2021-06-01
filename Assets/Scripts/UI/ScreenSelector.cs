
using UnityEngine;

namespace SES.UI
{
    public class ScreenSelector : MonoBehaviour
    {
        GameObject agent;
        GameObject previousAgent;
        AgentPanelUI info;

        void Start()
        {
            info = GetComponent<AgentPanelUI>();
        }

        void FixedUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Agents")))
                {
                    //Debug.Log($"selection");
                    ActivateAgentPanel();
                    Transform selection = hit.transform;
                    agent = selection.parent.gameObject;
                    info.SetAgent(agent);
                    info.UpdateMaskDropdown();
                    info.UpdateActivityDropdown();
                    info.UIConfigureTeacherMovement();

                    agent.transform.GetChild(1).gameObject.SetActive(true);
                    if (previousAgent != null && ReferenceEquals(agent, previousAgent) == false)
                    {
                        previousAgent.transform.GetChild(1).gameObject.SetActive(false);
                    }
                    previousAgent = agent;
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (agent != null)
                {
                    DeactivateAgentPanel();
                }
            }
        }
        public void ActivateAgentPanel()
        {
            //if (GetComponent<SpacePanelUI>().spacePanelUp)
            //{
            //    GetComponent<SpacePanelUI>().MovePanelDown();
            //}
            GetComponent<AgentPanelUI>().MovePanelUp();
        }

        public void DeactivateAgentPanel()
        {
            GetComponent<AgentPanelUI>().MovePanelDown();
            agent.transform.GetChild(1).gameObject.SetActive(false);
            agent = null;
        }

        public GameObject GetAgent()
        {
            return agent;
        }
    }
}
