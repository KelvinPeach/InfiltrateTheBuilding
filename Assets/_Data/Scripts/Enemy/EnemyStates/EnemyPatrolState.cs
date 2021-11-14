using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiltrate
{
    public class EnemyPatrolState : EnemyBaseState
    {
        float stopDuration;

        public override void EnterState(Enemy enemy)
        {
            enemy.Agent.speed = enemy.normalSpeed;
            enemy.FovRen.material = enemy.fovNormalMaterial;

            EnemyPatrol enemyPatrol = (EnemyPatrol)enemy;
            stopDuration = enemyPatrol.stopDuration;
            enemy.Agent.destination = enemyPatrol.GetPatrolPoint().position;
        }

        public override void Update(Enemy enemy)
        {
            // Have we reached the position the player was last seen?
            if (enemy.Agent.remainingDistance <= enemy.Agent.stoppingDistance)
            {
                // Stop at patrol point for the specified amount of time
                if (stopDuration <= 0)
                {
                    EnemyPatrol enemyPatrol = (EnemyPatrol)enemy;

                    // Reset
                    stopDuration = enemyPatrol.stopDuration;

                    // Next patrol point
                    enemyPatrol.NextPatrolPoint();
                    enemy.Agent.destination = enemyPatrol.GetPatrolPoint().position;
                }
                else
                {
                    stopDuration -= Time.deltaTime;
                }
            }
        }
    }
}