using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiltrate
{
    public class EnemyCombatState : EnemyBaseState
    {
        public override void EnterState(Enemy enemy)
        {
            enemy.SetSpeed(enemy.chaseSpeed);
            enemy.FovRen.material = enemy.fovAlertMaterial;
        }

        public override void Update(Enemy enemy)
        {
            // Can our field of vision cone still see the player?
            if (enemy.Fov.visibleTargets.Count > 0)
            {
                // Chase the player
                enemy.Agent.destination = enemy.Fov.visibleTargets[0].position;
            }
            else
            {
                // Have we reached the position the player was last seen?
                if (enemy.Agent.remainingDistance <= enemy.Agent.stoppingDistance)
                {
                    // Look around the area for where they may be hiding
                    enemy.TransitionToState(enemy.searchState);
                }
            }
        }
    }
}