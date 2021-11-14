using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Infiltrate
{
    public class MessageText : MonoBehaviour
    {
        [SerializeField] Text messageText;

        PlayerDisguise playerDisguise;
        PlayerInteract playerInteract;

        void Awake()
        {
            // Subscribe to events
            DisguisePickup.onPlayerInRange += OnDisguiseShow;
            DisguisePickup.onPlayerOutOfRange += OnDisguiseHide;
            PlayerInteract.onInteractableInRange += OnInteractableShow;
            Interactable.onStatusChanged += OnInteractableShow;
            PlayerInteract.onInteractableOutOfRange += OnInteractableHide;

            // Cache
            playerDisguise = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDisguise>(); // Assumes only one player
            playerInteract = playerDisguise.GetComponent<PlayerInteract>();
        }

        void OnDisguiseShow(Disguise disguiseInRange)
        {
            messageText.text = "Press 'E' to equip " + disguiseInRange.DisplayName;
        }

        void OnDisguiseHide(Disguise disguiseOutOfRange)
        {
            messageText.text = "";
        }

        void OnInteractableShow(Interactable interactable)
        {
            // Make sure the player is in range (e.g. enemy's turning off a TV shouldn't show the message text)
            if (!playerInteract.NearestInteractable) return;

            // E.g. Press F to turn on TV
            messageText.text = string.Format("Press 'F' to {0} {1}", interactable.GetCurrentStatusVerb(), interactable.DisplayName); // string.format for localization

            // Is this action illegal?
            if (interactable.IsIllegal)
            {
                // Is the player wearing a disguise that mitigates this?
                if (PlayerInteract.IsInteractableIllegal(interactable, playerDisguise.CurrentDisguise))
                {
                    messageText.text += " <b><color=red>(ILLEGAL ACTION)</color></b>";
                }
            }
        }

        void OnInteractableHide(Interactable interactable)
        {
            messageText.text = "";
        }

        void OnDestroy()
        {
            // Unsubscribe from events
            DisguisePickup.onPlayerInRange -= OnDisguiseShow;
            DisguisePickup.onPlayerOutOfRange -= OnDisguiseHide;
            PlayerInteract.onInteractableInRange -= OnInteractableShow;
            Interactable.onStatusChanged -= OnInteractableShow;
            PlayerInteract.onInteractableOutOfRange -= OnInteractableHide;
        }
    }
}