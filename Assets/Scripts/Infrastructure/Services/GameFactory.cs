using System.Collections.Generic;
using System.Threading.Tasks;
using AssetManagement;
using Creatures;
using Creatures.Ai;
using Helpers;
using StaticData;
using UI;
using UnityEngine;

namespace Infrastructure.Services
{
    public class GameFactory : IGameFactory
    {
        private readonly IStaticDataService _staticData;
        private readonly IAssetProvider _assetProvider;

        private List<Transform> _waypoints = new List<Transform>();
        private Transform _base;
        private GameObject _hero;

        public GameFactory(IStaticDataService staticDataService, IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            _staticData = staticDataService;
        }
    
        public async Task WarmUp()
        {
            await _assetProvider.Load<GameObject>(AssetAddress.WaypointPath);
            await _assetProvider.Load<GameObject>(AssetAddress.BasePath);
        }

        public async Task CreateWaypoints()
        {
            LevelStaticData levelStaticData = _staticData.ForLevel();
            List<Vector3> waypointPositions = PoissonDiscSampling.GeneratePoints(Vector3.zero,
                levelStaticData.MinWaypointDistance, levelStaticData.SpawnAreaSideSize, levelStaticData.SpawnAreaSideSize,
                levelStaticData.WaypointCount + 1);
        
            GameObject basePrefab = await _assetProvider.Load<GameObject>(AssetAddress.BasePath);
            GameObject waypointPrefab = await _assetProvider.Load<GameObject>(AssetAddress.WaypointPath);
        
            _base = Object.Instantiate(basePrefab, waypointPositions[0], Quaternion.identity).transform;
        
            for (int i = 1; i < waypointPositions.Count; i++)
            {
                Transform waypoint = Object.Instantiate(waypointPrefab, waypointPositions[i], Quaternion.identity).transform;
                _waypoints.Add(waypoint);
            }

        }
    
        public async Task CreateHero()
        {
            HeroStaticData heroStaticData = _staticData.ForHero();
            heroStaticData.Waypoints = _waypoints;
            heroStaticData.BaseWaypoint = _base;
            _hero = await _assetProvider.Instantiate(AssetAddress.HeroPath, _base.position);
            _hero.transform.Translate(Vector3.up * _hero.GetComponent<CapsuleCollider>().height * 1.01f);
            _hero.GetComponent<AiAgent>().Construct(heroStaticData);
        }

        public async Task CreateClickRaycaster()
        {
            LevelStaticData levelStaticData = _staticData.ForLevel();
            GameObject pointer = await _assetProvider.Instantiate(AssetAddress.ClickRaycaster);
            pointer.GetComponent<IDamageDealer>().Damage = levelStaticData.ClickDamage;
        }


        public async Task CreateHud(IGameStateMachine gameStateMachine)
        {
            GameObject hud = await _assetProvider.Instantiate(AssetAddress.HudPath);
            HudController hudController = hud.GetComponent<HudController>();
            hudController.Init(gameStateMachine, _hero.GetComponent<AiAgent>());
        }

        public void Cleanup()
        {
            _assetProvider.Cleanup();
            _base = null;
            _hero = null;
            _waypoints.Clear();
        }
    
    
    
    
    }
}