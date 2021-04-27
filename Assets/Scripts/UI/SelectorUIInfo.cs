using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectorUIInfo : MonoBehaviour
{
    GeneralHealthParamaters healthParamaters;
    Health agentHealth;
    GameObject agent;

    [Header("Fields")]
    [SerializeField] Text agentName;
    [SerializeField] Text infected;
    [SerializeField] Text infectionQuanta;
    [SerializeField] Text maskFactor;
    [SerializeField] Button infect;
    [SerializeField] Dropdown maskOptions;
    [SerializeField] Slider maskFactorSlider;
    [SerializeField] Text maskFactorValue;
    [SerializeField] GameObject maskLabels;

    void Start()
    {
        healthParamaters = FindObjectOfType<GeneralHealthParamaters>();
    }

    void Update()
    {
        UpdateInformation();
    }

    public void UpdateInformation()
    {
        if (agent == null) { return; }
        agentName.text = agent.name;
        infected.text = agentHealth.IsInfected().ToString();
        if(agentHealth.IsInfected())
        {
            infectionQuanta.text = "infected";
        }
        else
        {
            infectionQuanta.text = string.Format("{0:N3}", agentHealth.GetInfectionQuanta());
        }
        maskFactor.text = string.Format("{0:P2}", agentHealth.GetMaskFactor());
        maskFactorValue.text = string.Format("{0:P2}", agentHealth.GetMaskFactor());
    }

    public void InfectSelectedAgent()
    {
        agentHealth.InfectAgent();
    }

    public void SetMaskForSelected()
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
                maskFactorSlider.value = agentHealth.GetMaskFactor();
                break;
        }
    }

    public void SetCustomMaskForSelected()
    {
        agentHealth.SetMaskFactor(maskFactorSlider.value);
    }

    public void UpdateMaskDropdown()
    {
        if (agent == null) { return; }

        if (agentHealth.GetMaskFactor() == 1f)
        {
            maskOptions.value = 0;
            ShowMaskSlider(false);
        }
        else if (agentHealth.GetMaskFactor() == healthParamaters.clothMaskValue)
        {
            maskOptions.value = 1;
            ShowMaskSlider(false);
        }
        else if (agentHealth.GetMaskFactor() == healthParamaters.surgicalMaskValue)
        {
            maskOptions.value = 2;
            ShowMaskSlider(false);
        }
        else if (agentHealth.GetMaskFactor() == healthParamaters.n95MaskValue)
        {
            maskOptions.value = 3;
            ShowMaskSlider(false);
        }
        else
        {
            maskOptions.value = 4;
            ShowMaskSlider(true);
            maskFactorSlider.value = agentHealth.GetMaskFactor();
        }
    }

    void ShowMaskSlider(bool value)
    {
        maskFactorValue.gameObject.SetActive(value);
        maskFactorSlider.gameObject.SetActive(value);
        maskLabels.SetActive(value);
    }

    public void SetAgent( GameObject _agent)
    {
        agent = _agent;
        agentHealth = _agent.GetComponent<Health>();
    }
}
