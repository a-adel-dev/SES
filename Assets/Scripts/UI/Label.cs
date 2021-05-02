using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Label : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] 
    string displayedText;
    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(ShowToolTip());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ToolTip.HideTooltip_Static();
        StopAllCoroutines();
    }

    IEnumerator ShowToolTip()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        ToolTip.ShowTooltip_Static(displayedText);
    }
}
