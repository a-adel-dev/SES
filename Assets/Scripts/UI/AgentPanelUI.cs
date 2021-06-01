using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SES.Health;
using SES.AIControl;
using SES.Core;
using System;

namespace SES.UI
{
    public class AgentPanelUI : MonoBehaviour
    {
        GameObject agent;
        public bool agentPanelUp = false;
        IAgentHealth agentHealth;
        [Header("Fields")]
        [SerializeField] Text agentName;
        [SerializeField] Text infected;
        [SerializeField] Text infectionQuanta;
        [SerializeField] Text maskFactor;
        [SerializeField] Button infect;
        [SerializeField] Dropdown maskOptionsDropdown;
        [SerializeField] Button maskAllButton;
        [SerializeField] Button maskRandomButton;
        [SerializeField] Slider maskFactorSlider;
        [SerializeField] Text maskFactorValue;
        [SerializeField] GameObject maskLabels;
        [SerializeField] Dropdown activityOptions;
        [SerializeField] Button restrictMovementButton;
        [SerializeField] Button freeMovementButton;

        Animator animator;

        void Start()
        {
            animator = GetComponent<Animator>(); 
        }

        void Update()
        {
            UpdateInformation();
        }

        public void UpdateInformation()
        {
            if (agent == null) { return; }
            agentName.text = agent.name;
            infected.text = agentHealth.HealthCondition.ToString();
            infectionQuanta.text = agentHealth.HealthCondition != HealthCondition.healthy ? "infected" : string.Format("{0:N3}", agentHealth.GetInfectionQuanta());
            maskFactor.text = string.Format("{0:P2}", agentHealth.GetMaskFactor());
            maskFactorValue.text = string.Format("{0:P2}", agentHealth.GetMaskFactor());
        }

        public void UIConfigureTeacherMovement()
        {
            if (agent.GetComponent<ITeacherAI>() == null)
            {
                return;
            }
            switch (agent.GetComponent<ITeacherAI>().GetClassMovementStyle())
            {
                case -1:
                    restrictMovementButton.gameObject.SetActive(false);
                    freeMovementButton.gameObject.SetActive(false);
                    break;
                case 0:
                    restrictMovementButton.gameObject.SetActive(false);
                    freeMovementButton.gameObject.SetActive(true);
                    break;
                case 1:
                    restrictMovementButton.gameObject.SetActive(true);
                    freeMovementButton.gameObject.SetActive(false);
                    break;
            }
        }

        public void InfectSelectedAgent()
        {
            agentHealth.InfectAgent();
        }

        public void SetMaskForSelected()
        {
            switch (maskOptionsDropdown.value)
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
        public void SetMasks()
        {
            if (agent.GetComponent<ITeacherAI>() != null)
            {
                GeneralHealthParamaters.SetMaskForTeachers(MaskDropDownEnumValue());
            }
            else if (agent.GetComponent<IStudentAI>() != null)
            {
                GeneralHealthParamaters.SetMakForStudents(MaskDropDownEnumValue());
            }
        }

        public void SetMasksRandom()
        {
            if (agent.GetComponent<ITeacherAI>() != null)
            {
                GeneralHealthParamaters.SetRandomMaksForTeachers();
            }
            else if (agent.GetComponent<IStudentAI>() != null)
            {
                GeneralHealthParamaters.SetRandomMaksForStudents();
            }
        }


        private MaskFactor MaskDropDownEnumValue()
        {
            MaskFactor factor = MaskFactor.none;
            switch (maskOptionsDropdown.value)
            {
                case 0:
                    factor = MaskFactor.none;
                    break;
                case 1:
                    factor = MaskFactor.cloth;
                    break;
                case 2:
                    factor = MaskFactor.surgical;
                    break;
                case 3:
                    factor = MaskFactor.N95;
                    break;
            }
            return factor;
        }

        public void UpdateMaskDropdown()
        {
            if (agent == null) { return; }

            if (agentHealth.GetMaskFactor() == 1f)
            {
                maskOptionsDropdown.value = 0;
                ShowMaskSlider(false);
            }
            else if (agentHealth.GetMaskFactor() == SimulationDefaults.ClothMaskValue)
            {
                maskOptionsDropdown.value = 1;
                ShowMaskSlider(false);
            }
            else if (agentHealth.GetMaskFactor() == SimulationDefaults.SurgicalMaskValue)
            {
                maskOptionsDropdown.value = 2;
                ShowMaskSlider(false);
            }
            else if (agentHealth.GetMaskFactor() == SimulationDefaults.N95MaskValue)
            {
                maskOptionsDropdown.value = 3;
                ShowMaskSlider(false);
            }
            else
            {
                maskOptionsDropdown.value = 4;
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
            agentHealth = _agent.GetComponent<IAgentHealth>();
        }

        public void UpdateActivityDropdown()
        {
            switch (agentHealth.Activity)
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

        public void UIRestrictMovement()
        {
            agent.GetComponent<ITeacherAI>().ClassroomRestricted();
            freeMovementButton.gameObject.SetActive(true);
            restrictMovementButton.gameObject.SetActive(false);
        }

        public void UIFreeMovement()
        {
            agent.GetComponent<ITeacherAI>().ClassroomFree();
            restrictMovementButton.gameObject.SetActive(true);
            freeMovementButton.gameObject.SetActive(false);
        }

        public void MovePanelUp()
        {
            if (agentPanelUp == false)
            {
                animator.Play("PlayerPanelUp");
            }
            agentPanelUp = true;
        }

        public void MovePanelDown()
        {
            if (agentPanelUp)
            {
                animator.Play("PlayerPanelDown");
            }
            agentPanelUp = false;
        }
    }
}
