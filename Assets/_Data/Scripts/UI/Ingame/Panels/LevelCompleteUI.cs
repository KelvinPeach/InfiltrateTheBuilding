using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiltrate
{
    public class LevelCompleteUI : Panel
    {
        void Awake()
        {
            // Subscribe to events
            GoalTrigger.onPlayerReached += Show;
        }

        void OnDestroy()
        {
            // Unsubscribe from events
            GoalTrigger.onPlayerReached -= Show;
        }
    }
}