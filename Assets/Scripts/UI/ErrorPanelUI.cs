using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SES.UI
{
    public class ErrorPanelUI : MonoBehaviour
    {
        public RectTransform errorPanel;

        public void DisablePanel()
        {
            errorPanel.gameObject.SetActive(false);
        }
    }
}
