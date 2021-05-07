using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SES.UI
{
    public class MainControlPanel : MonoBehaviour
    {
        Animator anim;
        public void DisableWindow()
        {
            anim = GetComponent<Animator>();
            anim.Play("DisableWindow");
        }
    }
}
