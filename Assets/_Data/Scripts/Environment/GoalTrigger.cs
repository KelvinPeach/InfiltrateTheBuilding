using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiltrate
{
    public class GoalTrigger : MonoBehaviour
    {
        // Events
        public delegate void OnPlayerReached();
        public static event OnPlayerReached onPlayerReached;

        bool hasBeenReached;

        void OnTriggerEnter(Collider other)
        {
            // Only trigger once
            if (hasBeenReached) return;

            if (other.CompareTag("Player"))
            {
                hasBeenReached = true;

                if (onPlayerReached != null)
                    onPlayerReached();
            }
        }
    }
}