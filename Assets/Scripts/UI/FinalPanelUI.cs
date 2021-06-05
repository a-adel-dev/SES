using UnityEngine;

namespace SES.UI
{
    public class FinalPanelUI : MonoBehaviour
    {

        public void ShowPanel()
        {
            gameObject.SetActive(true);
        }
        public void Quit()
        {
            Application.Quit();
        }
    }

}
