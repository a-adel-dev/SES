using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SES.UI
{
    public class ToolTip : MonoBehaviour
    {
        [SerializeField]

        private static ToolTip instance;
        [SerializeField] TextMeshProUGUI displayText;
        [SerializeField] RectTransform backgroundRectTransform;
        public LayoutElement layoutElement;
        public int characterWrapLimit;

        //public RectTransform rectTransform;



        private void Awake()
        {
            //rectTransform = GetComponent<RectTransform>();
            instance = this;
            HideToolTip();
        }

        private void Update()
        {
            int contentLength = displayText.text.Length;

            layoutElement.enabled = (contentLength > characterWrapLimit) ? true : false;
            Vector2 position = Input.mousePosition;
            /*
            float pivotX = position.x / Screen.width;
            float pivotY = position.y / Screen.height;

            rectTransform.pivot = new Vector2(pivotX, pivotY);
            */
            transform.position = position;



        }
        void ShowToolTip(string tooltipString)
        {
            gameObject.SetActive(true);

            displayText.text = tooltipString;
        }

        void HideToolTip()
        {
            gameObject.SetActive(false);
        }

        public static void ShowTooltip_Static(string tooltipString)
        {
            instance.ShowToolTip(tooltipString);
        }

        public static void HideTooltip_Static()
        {
            instance.HideToolTip();
        }
    }
}
