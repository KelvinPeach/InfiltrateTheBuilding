using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiltrate
{
    public class Panel : MonoBehaviour
    {
        [SerializeField] GameObject panel;

        protected void Show()
        {
            panel.SetActive(true);
        }

        protected void Hide()
        {
            panel.SetActive(false);
        }
    }
}