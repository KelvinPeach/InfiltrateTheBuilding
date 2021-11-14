using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiltrate
{
	// Return to initial position
    public class EnemyReturnState : EnemyBaseState
    {
        public override void EnterState(Enemy enemy)
        {
            enemy.Agent.speed = enemy.normalSpeed;
            enemy.FovRen.material = enemy.fovNormalMaterial;

            enemy.Agent.destination = enemy.StartingPosition;
        }

        public override void Update(Enemy enemy)
        {
            // Return to where we were before chasing the player or being distracted
            if (enemy.Agent.remainingDistance <= enemy.Agent.stoppingDistance)
            {
                enemy.transform.rotation = enemy.StartingRotation;
                enemy.TransitionToState(enemy.StartingEnemyState);
            }
        }
    }
}