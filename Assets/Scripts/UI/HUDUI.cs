using System;
using UnityEngine;
using UnityEngine.UI;
using SES.School;
using SES.SimManager;
using SES.Core;

namespace SES.UI
{
    public class HUDUI : MonoBehaviour
    {
        SimulationController sim;
        [SerializeField] Text classStatus;
        [SerializeField] Text classDate;
        [SerializeField] Button spaceControlButton;
        [SerializeField] Button mainControlsShowButton;
        [SerializeField] Button mainControlsHideButton;
        [SerializeField] Button startSim;
        [SerializeField] Button pauseSim;
        [SerializeField] Button resumeSim;
        [SerializeField] Button endSim;

        SchoolScheduler school;

        private void Start()
        {
            sim = FindObjectOfType<SimulationController>();
            school = FindObjectOfType<SchoolScheduler>();
        }

        // Update is called once per frame
        void Update()
        {
            updateUIInfo();
        }

        private void updateUIInfo()
        {
            classStatus.text = school.SchoolState;
            //time
            //year - month - day - hour - minute - seconds
            //DateTime dt = new DateTime(0, 0, 0, 0, schoolManager.schoolTime, 0);

            classDate.text = String.Format("{0:f}", DateTimeRecorder.schoolTime);

        }

        public void UIStartSim()
        {
            sim.StartSim();
            pauseSim.gameObject.SetActive(true);
        }

        public void UIPauseSim()
        {
            sim.PauseSim();
            resumeSim.gameObject.SetActive(true);
            pauseSim.gameObject.SetActive(false);
        }

        public void UIResumeSim()
        {
            sim.ResumeSim();
            pauseSim.gameObject.SetActive(true);
            resumeSim.gameObject.SetActive(false);
        }

        public void ShowSpacePanel()
        {
            //if (GetComponent<AgentPanelUI>().agentPanelUp)
            //{
            //    GetComponent<ScreenSelector>().DeactivateAgentPanel();
            //}
            //GetComponent<SpacePanelUI>().MovePanelUp();
        }

        public void HideSpacePanel()
        {
            //GetComponent<SpacePanelUI>().MovePanelDown();
        }

        public void EnableStart()
        {
            startSim.interactable = true;
        }

        public void ShowParameters()
        {
            mainControlsHideButton.gameObject.SetActive(true);
            mainControlsShowButton.gameObject.SetActive(false);
        }

        public void HideParameters()
        {
            mainControlsShowButton.gameObject.SetActive(true);
            mainControlsHideButton.gameObject.SetActive(false);
        }

    }
}

