using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiltrate
{
    public class ScorePickup : Pickup
    {
        [SerializeField] int value = 1;

        protected override void Use(GameObject other)
        {
            PlayerScore playerScore = other.GetComponent<PlayerScore>();

            if (playerScore)
            {
                playerScore.AddScore(value);
            }
        }
    }
}