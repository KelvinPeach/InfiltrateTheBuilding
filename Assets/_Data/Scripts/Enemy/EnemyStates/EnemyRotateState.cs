using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiltrate
{
    public class EnemyRotateState : EnemyBaseState
    {
        float rotateTimer;

        public override void EnterState(Enemy enemy)
        {
            enemy.Agent.speed = enemy.normalSpeed;
            enemy.FovRen.material = enemy.fovNormalMaterial;

            EnemyRotate enemyRotate = (EnemyRotate)enemy;
            rotateTimer = enemyRotate.rotationDuration;
        }

        public override void Update(Enemy enemy)
        {
            EnemyRotate enemyRotate = (EnemyRotate)enemy;

            if (rotateTimer <= 0)
            {
                rotateTimer = enemyRotate.rotationDuration;
                enemyRotate.NextRotation();
            }
            else
            {
                rotateTimer -= Time.deltaTime;
            }
        }
    }
}