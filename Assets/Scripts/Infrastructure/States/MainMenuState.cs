using Infrastructure.Services;
using UI;

namespace Infrastructure.States
{
    public class MainMenuState : IState
    {
        private const string MainMenuSceneName = "MainMenu";
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameStateMachine _gameStateMachine;
        private LoadingCurtain _loadingCurtain;
        
        public MainMenuState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader, LoadingCurtain loadingCurtain)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
        }

        public void Enter()
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(MainMenuSceneName, OnLoaded);
        }

        private void OnLoaded()
        {
            _loadingCurtain.Hide();
        }
        
        public void Exit()
        {
            
        }
    }
}