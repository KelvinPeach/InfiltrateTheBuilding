using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiltrate
{
    public class EnemyRotate : Enemy
    {
        public readonly EnemyRotateState rotateState = new EnemyRotateState();

        public float rotationDuration = 2f; // How long before rotating
        [SerializeField] protected float[] rotations;

        int currentRotation;
        float rotationSmoothing = 5f;

        protected override void Start()
        {
            base.Start();

            StartingEnemyState = rotateState;
            TransitionToState(StartingEnemyState);
        }

        protected override void Update()
        {
            base.Update();

            // Rotate to the rotation
            if (CurrentState == rotateState)
            {
                Quaternion rotationTarget = Quaternion.Euler(0, rotations[currentRotation], 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, Time.deltaTime * rotationSmoothing);
            }
        }

        public void NextRotation()
        {
            // We are at the last rotation and must go back to the beginning
            if (currentRotation == rotations.Length - 1)
            {
                currentRotation = 0;
            }
            else
            {
                currentRotation++;
            }
        }
    }
}