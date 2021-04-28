using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScreenSelector : MonoBehaviour
{
    GameObject agent;
    GameObject previousAgent;
    Material originalMaterial;
    Vector3 agentPanelOriginalPosition;
    Vector3 agentPanelTargetPosition;
    Animator animator;

    [SerializeField]float panelMovementSpeed = 30f;

    SelectorUIInfo info;

    [SerializeField] RectTransform agentSelectionPanel;
    [SerializeField] Material agentHighlightMaterial;
    [SerializeField] Material spaceHighlightMaterial;


    void Start()
    {
        info = GetComponent<SelectorUIInfo>();
        animator = agentSelectionPanel.GetComponent<Animator>();
        agentPanelOriginalPosition = agentSelectionPanel.transform.position;
        agentPanelTargetPosition = new Vector3(
            agentPanelOriginalPosition[0],
            66.5f,
            agentPanelOriginalPosition[2]);
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log($"down");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Agents")))
            {
                
                MovePanelUp();
                Transform selection = hit.transform;
                agent = selection.gameObject;
                info.SetAgent(agent);
                info.UpdateMaskDropdown();
                info.UpdateActivityDropdown();
                

                originalMaterial = agent.GetComponent<Renderer>().material;
                if (previousAgent != null)
                {
                    previousAgent.GetComponent<Renderer>().material = originalMaterial;
                }
                agent.GetComponent<Renderer>().material = agentHighlightMaterial;
                previousAgent = agent;
            }

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MovePanelDown();
            //agent.GetComponent<Renderer>().material = originalMaterial;
            agent = null;
        }
    }
    void MovePanelUp()
    {
        animator.Play("PlayerPanelUp");
    }

    void MovePanelDown()
    {
        animator.Play("PlayerPanelDown");
    }

    public GameObject GetAgent()
    {
        return agent;
    }  
}
