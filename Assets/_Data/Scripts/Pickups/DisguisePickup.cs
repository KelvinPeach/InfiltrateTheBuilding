using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiltrate
{
    public class DisguisePickup : MonoBehaviour
    {
        [SerializeField] Disguise disguise;

        PlayerDisguise currentPlayer;

        // Events
        public delegate void OnPlayerInRange(Disguise disguiseInRange);
        public static event OnPlayerInRange onPlayerInRange;
        public delegate void OnPlayerOutOfRange(Disguise disguiseOutOfRange);
        public static event OnPlayerOutOfRange onPlayerOutOfRange;
        public delegate void OnDisguisePickedUp(Disguise disguisePickedUp);
        public static event OnDisguisePickedUp onDisguisePickedUp;

        void Update()
        {
            // Is the player pressing the action key?
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Is the player near the disguise?
                if (currentPlayer)
                {
                    Use();

                    Destroy(gameObject);
                }
            }
        }

        void OnTriggerEnter(Collider other)
        {
            // Is the player in range of this disguise?
            if (other.CompareTag("Player"))
            {
                currentPlayer = other.GetComponent<PlayerDisguise>();

                if (onPlayerInRange != null)
                    onPlayerInRange(disguise);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            // Is the player out of range of this disguise?
            if (other.CompareTag("Player"))
            {
                currentPlayer = null;

                if (onPlayerOutOfRange != null)
                    onPlayerOutOfRange(disguise);
            }
        }

        void Use()
        {
            // If the player is in range equip this disguise
            if (currentPlayer)
            {
                if (onDisguisePickedUp != null)
                    onDisguisePickedUp(disguise);

                currentPlayer.SetDisguise(disguise);
            }
        }
    }
}