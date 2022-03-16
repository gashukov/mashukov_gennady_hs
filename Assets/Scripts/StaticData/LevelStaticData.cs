using System.Collections.Generic;
using Helpers;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Static Data/Level")]
    public class LevelStaticData : ScriptableObject
    {
        [Range(10, 35)]
        public float SpawnAreaSideSize = 35;
    
        [Range(2, 30)]
        public int WaypointCount = 6;
        
        [Range(1, 20)]
        public int MinWaypointDistance = 5;
    
        [Range(1, 20)]
        public float ClickDamage = 1.111f;

        public AssetReferenceGameObject WaypointPrefabRef;
        
        public AssetReferenceGameObject BasePrefabRef;

        private void OnValidate()
        {
            List<Vector3> waypoints = PoissonDiscSampling.GeneratePoints(Vector3.zero, MinWaypointDistance, SpawnAreaSideSize, SpawnAreaSideSize, WaypointCount + 1);
            if (waypoints.Count < WaypointCount + 1)
            {
                Debug.LogError($"It is not possible to place {WaypointCount} + 1 points on a {SpawnAreaSideSize}x{SpawnAreaSideSize} plane with a minimum distance {MinWaypointDistance} between points.");
            }
        }
    }
}