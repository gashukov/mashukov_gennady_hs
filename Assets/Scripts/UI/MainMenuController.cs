using Infrastructure.Services;
using Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class MainMenuController : MonoBehaviour
    {
        [Header("Buttons")]
        public Button ButtonNewGame;

        private IGameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            InitButtons();
        }

        private void InitButtons()
        {
            ButtonNewGame.onClick.AddListener(() => _gameStateMachine.Enter<LoadLevelState, string>("Game"));
        }
    
    }
}
