using System.Collections.Generic;
using Creatures.Ai;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "HeroData", menuName = "Static Data/Hero")]
    public class HeroStaticData : ScriptableObject
    {
        [Range(1, 1000)]
        public float Health = 100f;
    
        [Range(0.05f, 50f)]
        public float Speed = 0.5f;

        public AiStateId InitialState = AiStateId.Idle;

        [HideInInspector] public List<Transform> Waypoints;
        [HideInInspector] public Transform BaseWaypoint;
    }
}