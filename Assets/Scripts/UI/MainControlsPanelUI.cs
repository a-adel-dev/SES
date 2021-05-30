using UnityEngine;
using UnityEngine.UI;
using SES.Core;
using SES.SimManager;

namespace SES.UI
{
    public class MainControlsPanelUI : MonoBehaviour
    {
        SimInitializer initializer;

        [SerializeField] Slider timeScaleSlider;
        [SerializeField] InputField numDaysInput;
        [SerializeField] InputField numPeriodsInput;
        [SerializeField] InputField periodLengthInput;
        [SerializeField] InputField breakLengthInput;
        [SerializeField] Toggle activitiesToggle;
        [SerializeField] Toggle relocationToggle;

        [SerializeField] InputField numInfectedStudentsInput;
        [SerializeField] InputField numInfectedTeachersInput;
        [SerializeField] InputField egressCoolDownInput;
        [SerializeField] Dropdown studentsMasksDropDown;
        [SerializeField] Dropdown teachersMasksDropDown;
        [SerializeField] Dropdown airControlDropDown;
        [SerializeField] Toggle schoolHalfToggle;
        [SerializeField] Toggle classHalfToggle;

        [SerializeField] RectTransform errorPanel;
        [SerializeField] Button defaultsButton;
        [SerializeField] Button applyButton;
        [SerializeField] Button continueButton;


        private void Start()
        {
            initializer = FindObjectOfType<SimInitializer>();
        }

        public void DefaultSettings()
        {
            timeScaleSlider.value = SimulationDefaults.timeStep;

            numDaysInput.text = SimulationDefaults.simLength.ToString();
            numPeriodsInput.text = SimulationDefaults.numPeriods.ToString();
            periodLengthInput.text = SimulationDefaults.periodLength.ToString();
            breakLengthInput.text = SimulationDefaults.breakLength.ToString();

            activitiesToggle.isOn = SimulationDefaults.activitiesEnabled;
            relocationToggle.isOn = SimulationDefaults.relocationEnabled;

            numInfectedStudentsInput.text = SimulationDefaults.initialNumStudentsContagious.ToString();
            numInfectedTeachersInput.text = SimulationDefaults.initialNumTeachersContagious.ToString();

            egressCoolDownInput.text = SimulationDefaults.cooldownClassExit.ToString();

            GetHealthMasks(0, SimulationDefaults.studentsMaskSettings);
            GetHealthMasks(1, SimulationDefaults.teacherMaskSettings);

            schoolHalfToggle.isOn = SimulationDefaults.halfCapacity;

            classHalfToggle.isOn = SimulationDefaults.classroomHalfCapacity;
            airControlDropDown.value = SimulationDefaults.airControlSettings;
        }
        public void ApplySettings()
        {
            SimulationParameters.TimeStep = timeScaleSlider.value;
            if (numDaysInput.text == "0" || string.IsNullOrWhiteSpace(numDaysInput.text) || numPeriodsInput.text == "0" || string.IsNullOrWhiteSpace(numPeriodsInput.text)
                || string.IsNullOrWhiteSpace(numInfectedStudentsInput.text) || string.IsNullOrWhiteSpace(numInfectedTeachersInput.text) || string.IsNullOrWhiteSpace(egressCoolDownInput.text))
            {
                errorPanel.gameObject.SetActive(true);
                continueButton.interactable = false;
            }
            else
            {
                SimulationParameters.simLength = Mathf.Abs(int.Parse(numDaysInput.text));
                SimulationParameters.numPeriods = Mathf.Abs(int.Parse(numPeriodsInput.text));
                SimulationParameters.periodLength = Mathf.Abs(int.Parse(periodLengthInput.text));
                SimulationParameters.breakLength = Mathf.Abs(int.Parse(breakLengthInput.text));

                SimulationParameters.activitiesEnabled = activitiesToggle.isOn;
                SimulationParameters.RelocationEnabled = relocationToggle.isOn;

                SimulationParameters.initialNumStudentsContagious = Mathf.Abs(int.Parse(numInfectedStudentsInput.text));
                SimulationParameters.initialNumTeachersContagious = Mathf.Abs(int.Parse(numInfectedTeachersInput.text));

                SimulationParameters.CooldownClassExit = Mathf.Abs(int.Parse(egressCoolDownInput.text));

                SetHealthMasks(0, studentsMasksDropDown.value);
                SetHealthMasks(1, teachersMasksDropDown.value);

                SimulationParameters.airControlSettings = airControlDropDown.value;

                SimulationParameters.schoolHalfCapacity = schoolHalfToggle.isOn;
                SimulationParameters.classroomHalfCapacity = classHalfToggle.isOn;

                MakeValuesReadOnly();
                continueButton.interactable = true;
            }
        }

        public void Initializer()
        {
            initializer.InitializeVariables();
        }

        public void MakeValuesReadOnly()
        {
            timeScaleSlider.interactable = false;
            timeScaleSlider.interactable = false;
            numDaysInput.interactable = false;
            numPeriodsInput.interactable = false;
            periodLengthInput.interactable = false;
            breakLengthInput.interactable = false;
            activitiesToggle.interactable = false;
            relocationToggle.interactable = false;

            numInfectedStudentsInput.interactable = false;
            numInfectedTeachersInput.interactable = false;

            egressCoolDownInput.interactable = false;

            studentsMasksDropDown.interactable = false;
            teachersMasksDropDown.interactable = false;

            schoolHalfToggle.interactable = false;
            classHalfToggle.interactable = false;
            airControlDropDown.interactable = false;
            defaultsButton.gameObject.SetActive(false);
            applyButton.gameObject.SetActive(false);
        }

        public void ResetSettings()
        {
            timeScaleSlider.interactable = true;
            timeScaleSlider.interactable = true;
            numDaysInput.interactable = true;
            numPeriodsInput.interactable = true;
            periodLengthInput.interactable = true;
            breakLengthInput.interactable = true;
            activitiesToggle.interactable = true;
            relocationToggle.interactable = true;

            numInfectedStudentsInput.interactable = true;
            numInfectedTeachersInput.interactable = true;

            egressCoolDownInput.interactable = true;

            studentsMasksDropDown.interactable = true;
            teachersMasksDropDown.interactable = true;

            schoolHalfToggle.interactable = true;
            classHalfToggle.interactable = true;
            airControlDropDown.interactable = true;
            defaultsButton.gameObject.SetActive(true);
            applyButton.gameObject.SetActive(true);

            continueButton.interactable = false;
        }

        #region enums UI wiring
        /// <summary>
        /// returns the value of healthMasks settings for a group of agents
        /// </summary>
        /// <param name="identifier">0 if students, 1 if teachers</param>
        /// <param name="factor">0-none, 1-cloth, 2-surgical, 3-N95</param>
        private void GetHealthMasks(int identifier, MaskFactor factor)
        {
            if (identifier == 0)
            {
                switch (factor)
                {
                    case MaskFactor.none:
                        studentsMasksDropDown.value = 0;
                        break;
                    case MaskFactor.cloth:
                        studentsMasksDropDown.value = 1;
                        break;
                    case MaskFactor.surgical:
                        studentsMasksDropDown.value = 2;
                        break;
                    case MaskFactor.N95:
                        studentsMasksDropDown.value = 3;
                        break;
                    default:
                        studentsMasksDropDown.value = 0;
                        break;
                }
            }
            else
            {
                switch (factor)
                {
                    case MaskFactor.none:
                        teachersMasksDropDown.value = 0;
                        break;
                    case MaskFactor.cloth:
                        teachersMasksDropDown.value = 1;
                        break;
                    case MaskFactor.surgical:
                        teachersMasksDropDown.value = 2;
                        break;
                    case MaskFactor.N95:
                        teachersMasksDropDown.value = 3;
                        break;
                    default:
                        teachersMasksDropDown.value = 0;
                        break;
                }
            }
        }
        /// <summary>
        /// Set the mask factor for a class of agents
        /// </summary>
        /// <param name="identifier">0 if students, 1 if teachers</param>
        /// <param name="Value">0-none, 1-cloth, 2-surgical, 3-N95</param>
        private void SetHealthMasks(int identifier, int Value)
        {
            if (identifier == 0)
            {
                switch (Value)
                {
                    case 0:
                        SimulationDefaults.studentsMaskSettings = MaskFactor.none;
                        break;
                    case 1:
                        SimulationDefaults.studentsMaskSettings = MaskFactor.cloth;
                        break;
                    case 2:
                        SimulationDefaults.studentsMaskSettings = MaskFactor.surgical;
                        break;
                    case 3:
                        SimulationDefaults.studentsMaskSettings = MaskFactor.N95;
                        break;
                    default:
                        SimulationDefaults.studentsMaskSettings = MaskFactor.none;
                        break;
                }
            }
            else
            {
                switch (Value)
                {
                    case 0:
                        SimulationDefaults.teacherMaskSettings = MaskFactor.none;
                        break;
                    case 1:
                        SimulationDefaults.teacherMaskSettings = MaskFactor.cloth;
                        break;
                    case 2:
                        SimulationDefaults.teacherMaskSettings = MaskFactor.surgical;
                        break;
                    case 3:
                        SimulationDefaults.teacherMaskSettings = MaskFactor.N95;
                        break;
                    default:
                        SimulationDefaults.teacherMaskSettings = MaskFactor.none;
                        break;
                }
            }
        }
        #endregion

    }
}
