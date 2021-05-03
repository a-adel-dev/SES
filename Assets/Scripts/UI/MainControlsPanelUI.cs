using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainControlsPanelUI : MonoBehaviour
{
    SchoolManager schoolManager;
    GeneralHealthParamaters healthSettings;

    [SerializeField] MainControlPanel mainControlPanel;

    [SerializeField] Slider timeScaleSlider;
    [SerializeField] InputField numDaysInput;
    [SerializeField] InputField numPeriodsInput;
    [SerializeField] Slider periodLengthSlider;
    [SerializeField] Toggle activitiesToggle;

    [SerializeField] InputField numInfectedStudentsInput;
    [SerializeField] InputField numInfectedTeachersInput;
    [SerializeField] InputField egressCoolDownInput;
    [SerializeField] Dropdown studentsMasksDropDown;
    [SerializeField] Dropdown teachersMasksDropDown;
    [SerializeField] Dropdown airControlDropDown;
    [SerializeField] Toggle schoolHalfToggle;
    [SerializeField] Toggle classHalfToggle;

    [SerializeField] RectTransform ErrorPanel;
    bool noErrors = true;

    // Start is called before the first frame update
    void Start()
    {
        schoolManager = FindObjectOfType<SchoolManager>();
        healthSettings = FindObjectOfType<GeneralHealthParamaters>();  
    }

    public void DefaultSettings()
    {
        timeScaleSlider.value = schoolManager.sim.timeStep;
        numDaysInput.text = schoolManager.sim.simLength.ToString();
        numPeriodsInput.text = schoolManager.sim.numPeriods.ToString();
        periodLengthSlider.value = schoolManager.sim.periodLength;
        activitiesToggle.isOn = schoolManager.sim.activitiesEnabled;

        numInfectedStudentsInput.text = healthSettings.numStudentInfected.ToString();
        numInfectedTeachersInput.text = healthSettings.numTeachersInfected.ToString();

        egressCoolDownInput.text = schoolManager.sim.cooldownClassExit.ToString();
        GetHealthMasks(0, healthSettings.studentsMasks);
        GetHealthMasks(1, healthSettings.teachersMasks);

        schoolHalfToggle.isOn = schoolManager.sim.halfCapacity;
        classHalfToggle.isOn = schoolManager.sim.classroomHalfCapacity;
        airControlDropDown.value = healthSettings.airControlSettings;


    }
    public void ApplySettings()
    {
        noErrors = true;
        while (noErrors)
        {
            schoolManager.sim.timeStep = timeScaleSlider.value;
            if (numDaysInput.text == "0" || string.IsNullOrWhiteSpace(numDaysInput.text) ||  numPeriodsInput.text == "0" || string.IsNullOrWhiteSpace(numPeriodsInput.text)
                || string.IsNullOrWhiteSpace(numInfectedStudentsInput.text) || string.IsNullOrWhiteSpace(numInfectedTeachersInput.text) || string.IsNullOrWhiteSpace(egressCoolDownInput.text))
            {
                ErrorPanel.gameObject.SetActive(true);
                noErrors = false;
            }
            else
            {
                schoolManager.sim.SetSimLength(int.Parse(numDaysInput.text));
                schoolManager.sim.SetNumPeriods(int.Parse(numPeriodsInput.text));
                schoolManager.sim.SetPeriodLength((int)periodLengthSlider.value);
                schoolManager.sim.EnableActivities(activitiesToggle.isOn);

                healthSettings.numStudentInfected = int.Parse(numInfectedStudentsInput.text);
                healthSettings.numTeachersInfected = int.Parse(numInfectedTeachersInput.text);

                schoolManager.sim.cooldownClassExit = int.Parse(egressCoolDownInput.text);

                SetHealthMasks(0, studentsMasksDropDown.value);
                SetHealthMasks(1, teachersMasksDropDown.value);

                healthSettings.airControlSettings = airControlDropDown.value;

                schoolManager.sim.halfCapacity = schoolHalfToggle.isOn;
                schoolManager.sim.classroomHalfCapacity = classHalfToggle.isOn;

                mainControlPanel.DisableWindow();
                noErrors = false;
            }
        }
        
    }

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
                    healthSettings.studentsMasks = MaskFactor.none;
                    break;
                case 1:
                    healthSettings.studentsMasks = MaskFactor.cloth;
                    break;
                case 2:
                    healthSettings.studentsMasks = MaskFactor.surgical;
                    break;
                case 3:
                    healthSettings.studentsMasks = MaskFactor.N95;
                    break;
                default:
                    healthSettings.studentsMasks = MaskFactor.none;
                    break;
            }
        }
        else
        {
            switch (Value)
            {
                case 0:
                    healthSettings.teachersMasks = MaskFactor.none;
                    break;
                case 1:
                    healthSettings.teachersMasks = MaskFactor.cloth;
                    break;
                case 2:
                    healthSettings.teachersMasks = MaskFactor.surgical;
                    break;
                case 3:
                    healthSettings.teachersMasks = MaskFactor.N95;
                    break;
                default:
                    healthSettings.teachersMasks = MaskFactor.none;
                    break;
            }
        }
    }


}
