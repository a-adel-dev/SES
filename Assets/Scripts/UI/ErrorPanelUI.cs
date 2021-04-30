using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorPanelUI : MonoBehaviour
{
    public RectTransform errorPanel;

    public void DisablePanel()
    {
        errorPanel.gameObject.SetActive(false);
    }
}
