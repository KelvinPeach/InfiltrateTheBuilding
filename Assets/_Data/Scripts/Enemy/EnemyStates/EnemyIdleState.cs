using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiltrate
{
    public class EnemyIdleState : EnemyBaseState
    {
        public override void EnterState(Enemy enemy)
        {
            enemy.Agent.speed = enemy.normalSpeed;
            enemy.FovRen.material = enemy.fovNormalMaterial;
        }

        public override void Update(Enemy enemy)
        {
            // Stand still and do nothing like a good NPC
        }
    }
}