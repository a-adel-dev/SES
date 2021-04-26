using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSelector : MonoBehaviour
{
    Camera mainCamera;
    GameObject agent;
    GameObject previousAgent;
    Material originalMaterial;
    Vector3 agentPanelOriginalPosition;
    Vector3 agentPanelTargetPosition;
    Animator animator;
    [SerializeField]
    float panelMovementSpeed = 30f;

    Information info;

    [SerializeField] RectTransform agentSelectionPanel;
    [SerializeField] Material agentHighlightMaterial;
    [SerializeField] Material spaceHighlightMaterial;

    [Header("Fields")]
    [SerializeField] Text agentName;
    [SerializeField] Text infected;
    [SerializeField] Text infectionQuanta;
    [SerializeField] Button infect;
    [SerializeField] Dropdown maskOptions;

    Health agentHealth;

    
    // Start is called before the first frame update
    void Start()
    {
        info = GetComponent<Information>();
        animator = agentSelectionPanel.GetComponent<Animator>();
        agentPanelOriginalPosition = agentSelectionPanel.transform.position;
        agentPanelTargetPosition = new Vector3(
            agentPanelOriginalPosition[0],
            66.5f,
            agentPanelOriginalPosition[2]);
        mainCamera = Camera.main;
    }

    private void Update()
    {
        UpdateInformation();
    }

    private void UpdateInformation()
    {
        if (agent == null) { return; }
        agentName.text = agent.name;
        infected.text = agentHealth.IsInfected().ToString();
        infectionQuanta.text = string.Format("{0:N3}",agentHealth.GetInfectionQuanta());

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log($"down");
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Agents")))
            {
                MovePanelUp();
                

                Transform selection = hit.transform;
                agent = selection.gameObject;
                agentHealth = agent.GetComponent<Health>();
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
            agent.GetComponent<Renderer>().material = originalMaterial;
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

    public void InfectSelectedAgent()
    {
        agentHealth.InfectAgent();
    }
}
