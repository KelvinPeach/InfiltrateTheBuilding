using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiltrate
{
    public class GameOverUI : Panel
    {
        void Awake()
        {
            // Subscribe to events
            Enemy.onPlayerCaught += Show;
        }

        void OnDestroy()
        {
            // Unsubscribe from events
            Enemy.onPlayerCaught -= Show;
        }
    }
}