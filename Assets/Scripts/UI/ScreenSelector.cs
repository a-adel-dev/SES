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
    GeneralHealthParamaters healthParamaters;
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
    [SerializeField] Text maskFactor;
    [SerializeField] Slider maskFactorSilder;
    [SerializeField] Text maskFactorValue;

    Health agentHealth;

    
    // Start is called before the first frame update
    void Start()
    {

        healthParamaters = FindObjectOfType<GeneralHealthParamaters>();
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
        maskFactor.text = string.Format("{0:N2}", maskFactorSilder.value);
        maskFactorValue.text = agentHealth.GetMaskFactor().ToString();
        
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
                UpdateMaskDropdown();
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

    public void SetMaskForSelected ()
    {
        switch (maskOptions.value)
        {
            case 0:
                agentHealth.SetMaskFactor(MaskFactor.none);
                break;
            case 1:
                agentHealth.SetMaskFactor(MaskFactor.cloth);
                break;
             case 2:
                agentHealth.SetMaskFactor(MaskFactor.surgical);
                break;
            case 3:
                agentHealth.SetMaskFactor(MaskFactor.N95);
                break;
            case 4:
                ShowMaskSlider(true);
                break;
        }
    }

    public void SetCustomMaskForSelected()
    {
        agentHealth.SetMaskFactor(maskFactorSilder.value);
    }

    public void SliderValueChangeCheck()
    {
        maskFactor.text = (maskFactorSilder.value).ToString();
    }

    private void UpdateMaskDropdown()
    {
        if (agent == null) { return; }
        float surgical = healthParamaters.surgicalMaskValue;
        float n95 = healthParamaters.n95MaskValue;
        float cloth = healthParamaters.clothMaskValue;
            
        if (agentHealth.GetMaskFactor() == 1f)
        {
            maskOptions.value = 0;
            ShowMaskSlider(false);

        }
        else if (agentHealth.GetMaskFactor() == cloth)
        {
            maskOptions.value = 1;
            ShowMaskSlider(false);
        }
        else if (agentHealth.GetMaskFactor() == surgical)
        {
            maskOptions.value = 2;
            ShowMaskSlider(false);
        }
        else if (agentHealth.GetMaskFactor() == n95)
        {
            maskOptions.value = 3;
            ShowMaskSlider(false);
        }
        else
        {
            maskOptions.value = 4;
            ShowMaskSlider(true);
            maskFactorSilder.value = agentHealth.GetMaskFactor();
        }
    }
    
    void ShowMaskSlider(bool value)
    {
        maskFactor.gameObject.SetActive(value);
        maskFactorSilder.gameObject.SetActive(value);
    }
}
