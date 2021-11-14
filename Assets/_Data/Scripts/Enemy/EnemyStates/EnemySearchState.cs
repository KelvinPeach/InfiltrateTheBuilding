using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiltrate
{
	// Search nearby waypoints for the player
    public class EnemySearchState : EnemyBaseState
    {
        static float maxStopDuration = 0.5f;
        static float waypointSearchDistance = 10f;

        Queue<Transform> searchWaypoints = new Queue<Transform>();

        float stopDuration;

        public override void EnterState(Enemy enemy)
        {
            enemy.Agent.speed = enemy.normalSpeed;
            enemy.FovRen.material = enemy.fovEvasionMaterial;

            enemy.SetSpeed(enemy.chaseSpeed);

            searchWaypoints.Clear();

            // Find nearby evasion search nodes
            Collider[] hitColliders = Physics.OverlapSphere(enemy.transform.position, waypointSearchDistance);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Waypoint"))
                {
                    searchWaypoints.Enqueue(hitCollider.transform);
                }
            }

            // Assign first search location position
            if (searchWaypoints.Count > 0)
            {
                enemy.Agent.destination = searchWaypoints.Dequeue().position;
                stopDuration = maxStopDuration;
            }
            // If there aren't any, go straight to the return state
            else
            {
                enemy.TransitionToState(enemy.returnState);
            }
        }

        public override void Update(Enemy enemy)
        {
            // Have we reached the position the player was last seen?
            if (enemy.Agent.remainingDistance <= enemy.Agent.stoppingDistance)
            {
                // Stop at patrol point for the specified amount of time
                if (stopDuration <= 0)
                {
                    // Next patrol point
                    if (searchWaypoints.Count > 0)
                    {
                        stopDuration = maxStopDuration;
                        enemy.Agent.destination = searchWaypoints.Dequeue().position;
                    }
                    // If there are no more places to search for the player, go back to normal
                    else
                    {
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