
using UnityEngine;

namespace SES.UI
{
    public class ScreenSelector : MonoBehaviour
    {
        GameObject agent;
        GameObject previousAgent;
        AgentPanelUI info;
        [SerializeField] SpacePanelUI spacePanel;

        void Start()
        {
            info = GetComponent<AgentPanelUI>();
        }

        void FixedUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                int combinedLayer = 1 << LayerMask.NameToLayer("Students") | 1 << LayerMask.NameToLayer("Teachers");
                if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, combinedLayer))
                {
                    //Debug.Log($"selection");
                    ActivateAgentPanel();
                    Transform selection = hitInfo.transform;
                    agent = selection.parent.gameObject;
                    info.SetAgent(agent);
                    info.UpdateMaskDropdown();
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
            if (spacePanel.spacePanelUp)
            {
                spacePanel.MovePanelDown();
            }
            info.MovePanelUp();
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
