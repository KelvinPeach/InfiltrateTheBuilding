using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiltrate
{
	// Moves to the player's location regardless of being able to see them
	// This is used by the guard dog that can sniff the player from anywhere on the level
    public class EnemyHomingState : EnemyBaseState
    {
        Transform target;

        public override void EnterState(Enemy enemy)
        {
            enemy.Agent.speed = enemy.normalSpeed;
            enemy.FovRen.material = enemy.fovNormalMaterial;

            // Find the player
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        public override void Update(Enemy enemy)
        {
            // Move towards the player
            if (target)
                enemy.Agent.destination = target.position;
        }
    }
}