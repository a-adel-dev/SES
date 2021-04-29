using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    Animator animator;
    

    // Start is called before the first frame update
    void Start()
    {
        PopulateClassSelectorDropDown();
        animator = spacePanel.GetComponent<Animator>();
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
    }

    void DropdownItemSelected(Dropdown dropdown)
    {
        int index = dropdown.value;
        space = spacesList[index];  
    }
}
