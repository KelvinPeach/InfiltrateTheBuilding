using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infiltrate
{
    [CreateAssetMenu(fileName = "Disguise", menuName = "ScriptableObjects/Disguise", order = 1)]
    public class Disguise : ScriptableObject
    {
        public string DisplayName { get { return displayName; } }
        public string Description { get { return description; } }
        public Material Material { get { return material; } }
        public DisguisePickup Pickup { get { return pickup; } }

        [SerializeField] string displayName;
        [SerializeField] string description;
        [SerializeField] Material material;
        [SerializeField] DisguisePickup pickup;
    }
}