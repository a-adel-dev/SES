using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SES.UI
{
    public class MainControlPanel : MonoBehaviour
    {
        Animator anim;
        public void DisableWindow()
        {
            GetComponent<Animator>().Play("DisableWindow");
        }

        public void EnableWindows()
        {
            GetComponent<Animator>().Play("EnableWindow");
        }
    }
}
