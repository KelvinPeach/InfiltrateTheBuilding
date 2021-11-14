using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Infiltrate
{
    public class TresspassingWarning : MonoBehaviour
    {
        [SerializeField] GameObject panel;
        [SerializeField] Text warningText;

        void Awake()
        {
            // Subscribe to events
            PlayerDisguise.onTresspassing += Show;
            PlayerDisguise.onStoppedTresspassing += Hide;
        }

        void Show(TresspassingTrigger location)
        {
            warningText.text = string.Format("Trespassing in {0}", location.DisplayName); // string.Format for localization

            panel.SetActive(true);
        }

        void Hide()
        {
            panel.SetActive(false);
        }

        void OnDestroy()
        {
            // Unsubscribe from events
            PlayerDisguise.onTresspassing -= Show;
            PlayerDisguise.onStoppedTresspassing -= Hide;
        }
    }
}