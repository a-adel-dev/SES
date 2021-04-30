using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDUI : MonoBehaviour
{
    SchoolManager schoolManager;

    [SerializeField] Text classStatus;
    [SerializeField] Text classTime;
    [SerializeField] Text classDate;
    [SerializeField] Button startSim;
    [SerializeField] Button pauseSim;
    [SerializeField] Button resumeSim;
    [SerializeField] Button endSim;
    [SerializeField] Button spaceControlButton;
    [SerializeField] Button mainControlsButton;




    // Start is called before the first frame update
    void Start()
    {
        schoolManager = FindObjectOfType<SchoolManager>();
    }

    // Update is called once per frame
    void Update()
    {
        updateUIInfo();
    }

    private void updateUIInfo()
    {
        //class status
        if (schoolManager.classInSession)
        {
            classStatus.text = "Classes in Session";
        }
        else
        {
            classStatus.text = "Break time";
        }

        //time
        //year - month - day - hour - minute - seconds
        //DateTime dt = new DateTime(0, 0, 0, 0, schoolManager.schoolTime, 0);
        
        classTime.text = String.Format("Time{0:hh:mm}", schoolManager.dateTime);

        //date
        classDate.text = String.Format("Day{0:dd}", schoolManager.dateTime);

    }

    public void UIStartSim()
    {
        schoolManager.StartSim();
        pauseSim.gameObject.SetActive(true);
    }

    public void UIPauseSim()
    {
        schoolManager.PauseSim();
        resumeSim.gameObject.SetActive(true);
        pauseSim.gameObject.SetActive(false);
    }

    public void UIResumeSim()
    {
        schoolManager.ResumeSim();
        pauseSim.gameObject.SetActive(true);
        resumeSim.gameObject.SetActive(false);
    }

    public void ShowSpacePanel()
    {
        
        if (GetComponent<AgentPanelUI>().agentPanelUp)
        {
            GetComponent<ScreenSelector>().DeactivateAgentPanel();
        }
        GetComponent<SpacePanelUI>().MovePanelUp();
    }

    public void HideSpacePanel()
    {
        GetComponent<SpacePanelUI>().MovePanelDown();
    }
}
