using AssetManagement;
using Infrastructure.Services;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private IGameStateMachine _stateMachine;
        private IAssetProvider _assetProvider;
        private IStaticDataService _staticDataService;

        public BootstrapState(IGameStateMachine gameStateMachine, IAssetProvider assetProvider, IStaticDataService staticDataService)
        {
            _stateMachine = gameStateMachine;
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            Init();
        }

        public void Enter()
        {
            _stateMachine.Enter<MainMenuState>();
        }
      

        public void Exit()
        {
            
        }
        
        
        private void Init()
        {
            _assetProvider.Initialize();
            _staticDataService.Load();
        }



    }
}

