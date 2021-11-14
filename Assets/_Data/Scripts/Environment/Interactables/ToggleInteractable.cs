using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiltrate
{
    public class ToggleInteractable : Interactable
    {
        [SerializeField] GameObject objectToToggle;

        public override void Interact(GameObject user)
        {
            base.Interact(user);

            if (objectToToggle)
                objectToToggle.SetActive(Status);
        }
    }
}