using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiltrate
{
    public class GameManager : MonoBehaviour
    {
        public static GameState GameState { get { return gameState; } }

        static GameState gameState;

        // Events
        public delegate void OnVictory();
        public static event OnVictory onVictory;
        public delegate void OnGameOver();
        public static event OnGameOver onGameOver;

        void Awake()
        {
            // Subscribe to events
            GoalTrigger.onPlayerReached += OnLevelComplete;
            Enemy.onPlayerCaught += OnLevelFailed;
        }

        void SetState(GameState newState)
        {
            gameState = newState;

            switch (newState)
            {
                case GameState.INTRO:

                    break;
                case GameState.PLAYING:

                    break;
                case GameState.VICTORY:

                    Time.timeScale = 0;

                    if (onVictory != null)
                        onVictory();

                    break;
                case GameState.GAME_OVER:

                    Time.timeScale = 0;

                    if (onGameOver != null)
                        onGameOver();

                    break;
                default:
                    break;
            }
        }

        void OnLevelComplete()
        {
            SetState(GameState.VICTORY);
        }

        void OnLevelFailed()
        {
            SetState(GameState.GAME_OVER);
        }

        void OnDestroy()
        {
            // Unsubscribe from events
            GoalTrigger.onPlayerReached -= OnLevelComplete;
            Enemy.onPlayerCaught -= OnLevelFailed;
        }
    }

    public enum GameState { INTRO, PLAYING, VICTORY, GAME_OVER }
}