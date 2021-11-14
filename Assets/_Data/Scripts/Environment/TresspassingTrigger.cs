using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiltrate
{
    public class TresspassingTrigger : MonoBehaviour
    {
        public Disguise[] AllowedDisguises { get { return allowedDisguises; } }
        public string DisplayName { get { return displayName; } }

        [SerializeField] string displayName;
        [SerializeField] Disguise[] allowedDisguises;

        void OnTriggerEnter(Collider other)
        {
            // Is it the player?
            PlayerDisguise player = other.GetComponent<PlayerDisguise>();

            if (player)
            {
                // Does their current disguise allow them to be in this area?
                bool hasValidDisguise = false;

                // Check if the player's disguise is permitted in this area
                foreach (var allowedDisguise in allowedDisguises)
                {
                    if (player.CurrentDisguise == allowedDisguise)
                    {
                        hasValidDisguise = true;
                        break;
                    }
                }

                if (!hasValidDisguise)
                    player.AddTresspassing(this);
            }
        }

        void OnTriggerExit(Collider other)
        {
            // Is it the player?
            PlayerDisguise player = other.GetComponent<PlayerDisguise>();

            if (player)
            {
                player.RemoveTresspassing(this);
            }
        }
    }
}