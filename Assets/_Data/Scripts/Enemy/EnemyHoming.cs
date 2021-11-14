using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiltrate
{
    public class EnemyHoming : Enemy
    {
        public readonly EnemyHomingState homingState = new EnemyHomingState();

        protected override void Start()
        {
            base.Start();

            StartingEnemyState = homingState;
            TransitionToState(StartingEnemyState);
        }
    }
}