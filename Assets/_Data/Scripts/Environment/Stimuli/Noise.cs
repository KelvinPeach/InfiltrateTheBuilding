using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiltrate
{
    public class Noise : MonoBehaviour
    {
        public AlertStatus Danger { get; private set; } // Enemy investigates noise response: 0-normal, 1-caution, 2-evasion, 3-alert

        public void Init(AlertStatus danger, float size)
        {
            Danger = danger;

            transform.localScale = new Vector3(size, transform.localScale.y, size);
        }
    }
}