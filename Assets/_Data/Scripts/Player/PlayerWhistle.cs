using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiltrate
{
    public class PlayerWhistle : MonoBehaviour
    {
        [SerializeField] GameObject whistleGo;
        [SerializeField] float whistleDuration = 0.1f;
        [SerializeField] float whistleCooldown = 0.5f;

        float currentCooldown; // Prevent player spamming whistle

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                // Can we whistle again?
                if (currentCooldown <= 0)
                {
                    // Prevent the player constantly whistling
                    currentCooldown = whistleCooldown;

                    // Enable the noise sphere momentarily
                    if (whistleGo)
                        whistleGo.SetActive(true);

                    // Disable the noise sphere after a short duration
                    StartCoroutine(DisableWhistleAfterTime(whistleDuration));
                }
            }

            // Decrease the time until the player can whistle again (if necessary)
            if (currentCooldown > 0)
                currentCooldown -= Time.deltaTime;
        }

        IEnumerator DisableWhistleAfterTime(float delay)
        {
            yield return new WaitForSeconds(delay);

            if (whistleGo)
                whistleGo.SetActive(false);
        }
    }
}