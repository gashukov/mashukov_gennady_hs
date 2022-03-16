using System.Threading.Tasks;
using Infrastructure.Services;
using UI;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private  IGameStateMachine _stateMachine;
        private  ISceneLoader _sceneLoader;
        private  LoadingCurtain _loadingCurtain;
        private  IGameFactory _gameFactory;
    
        private readonly IStaticDataService _staticData;
    
        public LoadLevelState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader, IGameFactory gameFactory, LoadingCurtain loadingCurtain)
        {
            _stateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _gameFactory.Cleanup();
            _gameFactory.WarmUp();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit()
        {
            _loadingCurtain.Hide();
        }
      

        private async void OnLoaded()
        {
            await InitGameWorld();

            _stateMachine.Enter<GameLoopState>();
        }

        private async Task InitGameWorld()
        {
            await _gameFactory.CreateClickRaycaster();
            await _gameFactory.CreateWaypoints();
            await _gameFactory.CreateHero();
            await _gameFactory.CreateHud(_stateMachine);
        }

    }
}