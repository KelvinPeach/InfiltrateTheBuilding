using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiltrate
{
    public class EnemyStationary : Enemy
    {
        protected override void Start()
        {
            base.Start();

            StartingEnemyState = idleState;
            TransitionToState(StartingEnemyState);
        }
    }
}