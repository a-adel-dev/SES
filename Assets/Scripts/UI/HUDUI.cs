using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SES.School;
using SES.SimProperties;
/*
namespace SES.UI
{
    
    public class HUDUI : MonoBehaviour
    {
        SchoolManager schoolManager;
        SimulationProperties simProperties;

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
            simProperties = FindObjectOfType<SimulationProperties>();
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

            classTime.text = String.Format("Time{0:hh:mm}", simProperties.schoolDate);

            //date
            classDate.text = String.Format("Day{0:dd}", simProperties.schoolDate);

        }

        public void UIStartSim()
        {
            simProperties.StartSim();
            pauseSim.gameObject.SetActive(true);
        }

        public void UIPauseSim()
        {
            simProperties.PauseSim();
            resumeSim.gameObject.SetActive(true);
            pauseSim.gameObject.SetActive(false);
        }

        public void UIResumeSim()
        {
            simProperties.ResumeSim();
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
}
    */
