using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiltrate
{
	// Go to a noise distractions location and check it out
    public class EnemyInvestigateState : EnemyBaseState
    {
        static float maxStopDuration = 2f;

        float stopDuration;

        public override void EnterState(Enemy enemy)
        {
            // How severe did this noise sound (e.g. gun shot or just someone whistling)?
            if (enemy.CurrentInvestigation.danger == AlertStatus.Evasion)
            {
                enemy.FovRen.material = enemy.fovEvasionMaterial;
                enemy.Agent.speed = enemy.chaseSpeed;
            }
            else
            {
                enemy.FovRen.material = enemy.fovCautionMaterial;
                enemy.Agent.speed = enemy.normalSpeed;
            }

            enemy.Agent.destination = enemy.CurrentInvestigation.location;
            stopDuration = maxStopDuration;
        }

        public override void Update(Enemy enemy)
        {
            // Have we reached the location of noise we are investigating?
            if (enemy.Agent.remainingDistance <= enemy.Agent.stoppingDistance)
            {
                // Stop at patrol point for the specified amount of time
                if (stopDuration <= 0)
                {
                    // Was the thing that attracted us here an interactable that should be turned off?
                    Collider[] hitColliders = Physics.OverlapSphere(enemy.transform.position, 0.1f);
                    foreach (var hitCollider in hitColliders)
                    {
                        Interactable interactable = hitCollider.GetComponent<Interactable>();

                        // 1. are we stood next to an interactable
                        // 2. should a guard disable it
                        // 3. is it enabled
                        if (interactable && interactable.ShouldGuardsDisable && interactable.Status)
                        {
                            interactable.Interact(enemy.gameObject);
                        }
                    }

                    // Depending on the severity of the noise
                    if (enemy.CurrentInvestigation.danger == AlertStatus.Evasion)
                    {
                        // This is serious! Sweep the area (find nearby waypoints and go to them one by one)
                        enemy.TransitionToState(enemy.searchState);
                    }
                    else
                    {
                        // Meh! Ignore it and go back to normal
                        enemy.TransitionToState(enemy.returnState);
                    }
                }
                else
                {
                    stopDuration -= Time.deltaTime;
                }
            }
        }
    }
}