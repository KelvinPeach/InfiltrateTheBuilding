using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiltrate
{
    public abstract class Interactable : MonoBehaviour
    {
        public bool Status { get { return status; } set { status = value; } }
        public string DisplayName { get { return displayName; } }
        public bool ShouldGuardsDisable { get { return shouldGuardsDisable; } }
        public bool IsIllegal { get { return isIllegal; } }
        public Disguise[] AllowedDisguises { get { return allowedDisguises; } }

        [SerializeField] bool status; // Whether to start active or inactive
        [SerializeField] string displayName;
        [SerializeField] string enableString;
        [SerializeField] string disableString;
        [SerializeField] AudioClip interactSound;
        [SerializeField] bool shouldGuardsDisable;
        [SerializeField] bool isIllegal; // If this interactable is used within a guard's field of view, will it cause them to attack? (e.g. pressing a fire alarm)
        [SerializeField] Disguise[] allowedDisguises; // Are there disguises that make it okay (e.g. a security guard uniform)

        // Events
        public delegate void OnStatusChanged(Interactable interactable);
        public static event OnStatusChanged onStatusChanged;

        // Cache
        AudioSource audioSrc;

        void Awake()
        {
            // Cache
            audioSrc = GetComponent<AudioSource>();
        }

        public virtual void Interact(GameObject user)
        {
            // Toggle status
            status = !status;

            if (audioSrc && interactSound)
                audioSrc.PlayOneShot(interactSound);

            if (onStatusChanged != null)
                onStatusChanged(this);
        }

        public string GetEnableVerb()
        {
            return enableString;
        }

        public string GetDisableVerb()
        {
            return disableString;
        }

        public string GetCurrentStatusVerb()
        {
            if (status)
            {
                return disableString;
            }
            else
            {
                return enableString;
            }
        }
    }
}