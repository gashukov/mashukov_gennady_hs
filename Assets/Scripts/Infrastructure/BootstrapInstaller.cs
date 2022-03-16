using AssetManagement;
using Helpers;
using Infrastructure.Services;
using UI;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        public Object CurtainPrefab;
    
        public override void InstallBindings()
        {
            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
            Container.Bind<ICoroutineRunner>().To<GameBootstrapper>().FromComponentOnRoot().AsSingle();
            Container.Bind<LoadingCurtain>().FromComponentInNewPrefab(CurtainPrefab).AsSingle();
        }

    }
}
