using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiltrate
{
    public class EnemyPatrol : Enemy
    {
        public readonly EnemyPatrolState patrolState = new EnemyPatrolState();

        public float stopDuration = 2f; // How long to stop once a patrol point is reached

        [SerializeField] protected Transform[] patrolPoints;

        int currentPatrolPoint;

        protected override void Start()
        {
            base.Start();

            StartingEnemyState = patrolState;
            TransitionToState(StartingEnemyState);
        }

        public void NextPatrolPoint()
        {
            // We are at the last rotation and must go back to the beginning
            if (currentPatrolPoint == patrolPoints.Length - 1)
            {
                currentPatrolPoint = 0;
            }
            else
            {
                currentPatrolPoint++;
            }
        }

        public Transform GetPatrolPoint()
        {
            return patrolPoints[currentPatrolPoint];
        }
    }
}