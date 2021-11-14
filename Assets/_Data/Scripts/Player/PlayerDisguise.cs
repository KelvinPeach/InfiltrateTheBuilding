using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiltrate
{
    public class PlayerDisguise : MonoBehaviour
    {
        public Disguise CurrentDisguise { get { return currentDisguise; } }

        [SerializeField] Disguise startingDisguise;

        [SerializeField] Renderer rend;

        Disguise currentDisguise;
        // List rather than bool because moving between two triggers wouldn't work. Sometimes the OnExit of the first trigger would go second and set tresspassing (incorrectly) to false.
        List<TresspassingTrigger> tresspassings = new List<TresspassingTrigger>();

        // Events
        public delegate void OnDisguiseChanged(Disguise newDisguise);
        public static event OnDisguiseChanged onDisguiseChanged;
        public delegate void OnTresspassing(TresspassingTrigger location);
        public static event OnTresspassing onTresspassing;
        public delegate void OnStoppedTresspassing();
        public static event OnStoppedTresspassing onStoppedTresspassing;

        void Start()
        {
            SetDisguise(startingDisguise);
        }

        void CheckTresspassingStatus()
        {
            if (IsTresspassing())
            {
                if (onTresspassing != null)
                    onTresspassing(tresspassings[0]);
            }
            else
            {
                if (onStoppedTresspassing != null)
                    onStoppedTresspassing();
            }
        }

        public void SetDisguise(Disguise newDisguise)
        {
            if (newDisguise)
            {
                // Drop old disguise
                if (currentDisguise)
                    Instantiate(currentDisguise.Pickup, transform.position, transform.rotation);

                // Equip new disguise
                currentDisguise = newDisguise;
                rend.material = currentDisguise.Material;

                if (onDisguiseChanged != null)
                    onDisguiseChanged(currentDisguise);

                // Check if the player's new disguise will make or stop them tresspassing
                CheckTresspassingStatus();
            }
        }

        public void AddTresspassing(TresspassingTrigger newTresspassing)
        {
            if (!tresspassings.Contains(newTresspassing))
            {
                tresspassings.Add(newTresspassing);

                CheckTresspassingStatus();
            }
        }

        public void RemoveTresspassing(TresspassingTrigger oldTresspassing)
        {
            if (tresspassings.Contains(oldTresspassing))
            {
                tresspassings.Remove(oldTresspassing);

                CheckTresspassingStatus();
            }
        }

        public bool IsTresspassing()
        {
            bool isTresspassing;

            // Is the player in any tresspassing zones (a restricted area)?
            if (tresspassings.Count > 0)
            {
                // Assume the player is tresspassing
                isTresspassing = true;

                // Is the player correctly disguised for each tresspass
                foreach (var tresspass in tresspassings)
                {
                    // Areas may have more than one valid disguise
                    foreach (var disguise in tresspass.AllowedDisguises)
                    {
                        if (currentDisguise == disguise)
                        {
                            isTresspassing = false;
                        }
                    }
                }
            }
            // Or are they in a public place
            else
            {
                isTresspassing = false;
            }

            return isTresspassing;
        }
    }
}