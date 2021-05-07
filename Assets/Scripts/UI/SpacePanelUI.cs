using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SES.Health;

namespace SES.UI
{
    public class SpacePanelUI : MonoBehaviour
    {
        GeneralHealthParamaters healthParamaters;
        SpaceHealth space;
        [SerializeField] GameObject spacePanel;
        public bool spacePanelUp = false;
        SpaceHealth[] spacesList;
        [Header("Fields")]

        [SerializeField] Dropdown classSelectorDropDown;
        [SerializeField] Text spaceNameText;
        [SerializeField] Text spaceVolumeText;
        [SerializeField] Text isOutdoorText;
        [SerializeField] Text concentrationText;
        [SerializeField] Text numAgentsText;
        [SerializeField] Dropdown airControlDropdown;
        [SerializeField] Text ACHText;

        Animator animator;


        // Start is called before the first frame update
        void Start()
        {
            PopulateClassSelectorDropDown();
            animator = spacePanel.GetComponent<Animator>();
            airControlDropdown.onValueChanged.AddListener(delegate { SetAirExchangeRate(airControlDropdown); });
        }

        // Update is called once per frame
        void Update()
        {
            UpdateInfo();
        }

        void PopulateClassSelectorDropDown()
        {
            classSelectorDropDown.options.Clear();

            spacesList = FindObjectsOfType<SpaceHealth>();
            foreach (SpaceHealth space in spacesList)
            {
                classSelectorDropDown.options.Add(new Dropdown.OptionData() { text = space.gameObject.name });
            }

            DropdownItemSelected(classSelectorDropDown);
            classSelectorDropDown.onValueChanged.AddListener(delegate { DropdownItemSelected(classSelectorDropDown); });
        }

        public void MovePanelUp()
        {
            animator.Play("PlayerPanelUp");
            spacePanelUp = true;
        }

        public void MovePanelDown()
        {
            animator.Play("PlayerPanelDown");
            spacePanelUp = false;
        }

        public void UpdateInfo()
        {
            if (space == null) { return; }
            spaceNameText.text = space.gameObject.name;
            spaceVolumeText.text = string.Format("{0:F2} m^3", space.spaceVolume);
            isOutdoorText.text = space.outdoor ? "Yes" : "No";
            concentrationText.text = string.Format("{0:F3} m^3", space.concentration);
            numAgentsText.text = space.GetNumAgents().ToString();
            UpdateACH();
            ACHText.text = string.Format("{0:F2}", space.GetAirExhangeRate());
        }

        void DropdownItemSelected(Dropdown dropdown)
        {
            int index = dropdown.value;
            space = spacesList[index];
        }

        void UpdateACH()
        {
            switch (space.GetAirExhangeRate())
            {
                case 0.12f:
                    airControlDropdown.value = 0;
                    break;

                case 0.23f:
                    airControlDropdown.value = 1;
                    break;

                case 0.85f:
                    airControlDropdown.value = 2;
                    break;

                case 0.90f:
                    airControlDropdown.value = 3;
                    break;

                case 2.16f:
                    airControlDropdown.value = 4;
                    break;

                case 7.92f:
                    airControlDropdown.value = 5;
                    break;

                default:
                    airControlDropdown.value = 0;
                    break;
            }
        }

        void SetAirExchangeRate(Dropdown dropdown)
        {
            int index = dropdown.value;
            switch (index)
            {
                case 0:
                    space.SetAirExhangeRate(0.12f);
                    break;

                case 1:
                    space.SetAirExhangeRate(0.23f);
                    break;

                case 2:
                    space.SetAirExhangeRate(0.85f);
                    break;

                case 3:
                    space.SetAirExhangeRate(0.90f);
                    break;

                case 4:
                    space.SetAirExhangeRate(2.16f);
                    break;

                case 5:
                    space.SetAirExhangeRate(7.92f);
                    break;

                default:
                    space.SetAirExhangeRate(0.12f);
                    break;
            }
        }
    }
}
