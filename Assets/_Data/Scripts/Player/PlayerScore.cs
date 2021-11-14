using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiltrate
{
    public class PlayerScore : MonoBehaviour
    {
        public int CurrentScore { get { return currentScore; } }

        int currentScore;

        // Events
        public delegate void OnScoreChanged(int newScore);
        public static event OnScoreChanged onScoreChanged;

        public void AddScore(int amount)
        {
            currentScore += amount;

            if (onScoreChanged != null)
                onScoreChanged(currentScore);
        }

        public void RemoveScore(int amount)
        {
            currentScore -= amount;

            if (onScoreChanged != null)
                onScoreChanged(currentScore);
        }
    }
}