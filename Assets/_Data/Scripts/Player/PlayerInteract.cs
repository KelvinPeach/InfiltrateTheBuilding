using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiltrate
{
    public class PlayerInteract : MonoBehaviour
    {
        static float maxInteractCooldown = 0.1f;

        public Interactable NearestInteractable { get { return nearestInteractable; } }

        float currentInteractCooldown;
        Interactable nearestInteractable;

        // Cache
        PlayerDisguise playerDisguise;

        // Events
        public delegate void OnInteractableInRange(Interactable interactable);
        public static event OnInteractableInRange onInteractableInRange;
        //public delegate void OnStatusChanged(Interactable interactable);
        //public static event OnStatusChanged onStatusChanged;
        public delegate void OnInteractableOutOfRange(Interactable interactable);
        public static event OnInteractableOutOfRange onInteractableOutOfRange;

        void Awake()
        {
            // Cache
            playerDisguise = GetComponent<PlayerDisguise>();
        }

        void Update()
        {
            // Prevent rapidly pressing the interact button
            if (currentInteractCooldown > 0)
            {
                currentInteractCooldown -= Time.deltaTime;
            }
            else
            {
                // Is the player pressing the action button
                if (Input.GetKeyDown(KeyCode.F))
                {
                    // Is there a nearby interactable?
                    if (nearestInteractable != null)
                    {
                        currentInteractCooldown = maxInteractCooldown;

                        nearestInteractable.Interact(gameObject);
                    }
                }
            }
        }

        void OnTriggerEnter(Collider other)
        {
            // Is it an interactable?
            Interactable interactable = other.transform.GetComponent<Interactable>();

            if (interactable != null)
            {
                nearestInteractable = interactable;

                if (onInteractableInRange != null)
                    onInteractableInRange(interactable);
            }
        }

        void OnTriggerExit(Collider other)
        {
            // Have we moved away from our current interactable?
            if (nearestInteractable)
            {
                if (other.name == nearestInteractable.name)
                {
                    nearestInteractable = null;

                    if (onInteractableOutOfRange != null)
                        onInteractableOutOfRange(nearestInteractable);
                }
            }
        }

        public bool IsPerformingIllegalAction()
        {
            // currentInteractCooldown = has the player just performed an action?
            if (nearestInteractable && currentInteractCooldown > 0)
            {
                // Is it illegal (in at least some disguises)?
                if (nearestInteractable.IsIllegal)
                {
                    // Is it illegal in the player's current disguise?
                    foreach (Disguise allowedDisguise in nearestInteractable.AllowedDisguises)
                    {
                        if (allowedDisguise == playerDisguise)
                        {
                            // The player can do this action in their current disguise
                            return false;
                        }
                    }

                    // The player can't do this action in their current disguise
                    return true;
                }
                else
                {
                    // Not illegal under any circumstance
                    return false;
                }
            }

            // No nearest interactable
            return false;
        }

        public static bool IsInteractableIllegal(Interactable interactable, Disguise playerDisguise)
        {
            if (playerDisguise && interactable)
            {
                // Is it illegal (in at least some disguises)?
                if (interactable.IsIllegal)
                {
                    // Is it illegal in the player's current disguise?
                    foreach (Disguise allowedDisguise in interactable.AllowedDisguises)
                    {
                        if (allowedDisguise == playerDisguise)
                        {
                            // The player can do this action in their current disguise
                            return false;
                        }
                    }

                    // The player can't do this action in their current disguise
                    return true;
                }
                else
                {
                    // Not illegal under any circumstance
                    return false;
                }
            }

            // No nearest interactable
            return false;
        }
    }
}