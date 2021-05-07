using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SES.Health;
using SES.AIControl;
using SES.Core;
/*
namespace SES.UI
{
    public class AgentPanelUI : MonoBehaviour
    {
        GeneralHealthParamaters healthParamaters;
        AgentHealth agentHealth;
        GameObject agent;
        public bool agentPanelUp = false;

        [SerializeField] GameObject agentPanel;
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
        [SerializeField] Dropdown activityOptions;
        [SerializeField] Button restrictMovementButton;
        [SerializeField] Button freeMovementButton;

        Animator animator;

        void Start()
        {
            healthParamaters = FindObjectOfType<GeneralHealthParamaters>();
            animator = agentPanel.GetComponent<Animator>();
        }

        void Update()
        {
            UpdateInformation();
        }

        public void UpdateInformation()
        {
            if (agent == null) { return; }
            agentName.text = agent.name;
            infected.text = agentHealth.IsInfected() ? "Yes" : "No";
            infectionQuanta.text = agentHealth.IsInfected() ? "infected" : string.Format("{0:N3}", agentHealth.GetInfectionQuanta());
            maskFactor.text = string.Format("{0:P2}", agentHealth.GetMaskFactor());
            maskFactorValue.text = string.Format("{0:P2}", agentHealth.GetMaskFactor());
        }

        public void UIConfigureTeacherMovement()
        {
            if (agent.GetComponent<TeacherAI>() && agent.GetComponent<TeacherAI>().IsInClassroom())
            {
                if (agent.GetComponent<TeacherAI>().movementStyle == TeacherMovementStyle.restricted)
                {
                    freeMovementButton.gameObject.SetActive(true);
                    restrictMovementButton.gameObject.SetActive(false);

                }
                else if (agent.GetComponent<TeacherAI>().movementStyle == TeacherMovementStyle.classroom)
                {
                    restrictMovementButton.gameObject.SetActive(true);
                    freeMovementButton.gameObject.SetActive(false);
                }
            }
            else
            {
                restrictMovementButton.gameObject.SetActive(false);
                freeMovementButton.gameObject.SetActive(false);
            }
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

        public void SetAgent(GameObject _agent)
        {
            agent = _agent;
            agentHealth = _agent.GetComponent<AgentHealth>();
        }

        public void UpdateActivityDropdown()
        {
            switch (agentHealth.activity)
            {
                case ActivityType.Breathing:
                    activityOptions.value = 0;
                    break;
                case ActivityType.Talking:
                    activityOptions.value = 1;
                    break;
                case ActivityType.LoudTalking:
                    activityOptions.value = 2;
                    break;
            }
        }

        public void UISetActivityLevel()
        {
            if (agent == null) { return; }
            switch (activityOptions.value)
            {
                case 0:
                    agentHealth.SetActivityType(ActivityType.Breathing);
                    break;
                case 1:
                    agentHealth.SetActivityType(ActivityType.Talking);
                    break;
                case 2:
                    agentHealth.SetActivityType(ActivityType.LoudTalking);
                    break;
            }
        }

        public void UIRestrictMovement()
        {
            agent.GetComponent<TeacherAI>().RestrictClassMovement();
            freeMovementButton.gameObject.SetActive(true);
            restrictMovementButton.gameObject.SetActive(false);
        }

        public void UIFreeMovement()
        {
            agent.GetComponent<TeacherAI>().FreeClassMovement();
            restrictMovementButton.gameObject.SetActive(true);
            freeMovementButton.gameObject.SetActive(false);
        }

        public void MovePanelUp()
        {
            animator.Play("PlayerPanelUp");
            agentPanelUp = true;
        }

        public void MovePanelDown()
        {
            animator.Play("PlayerPanelDown");
            agentPanelUp = false;
        }
    }
}
*/