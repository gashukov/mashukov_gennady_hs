using Creatures;
using Creatures.Ai;
using Infrastructure.Services;
using Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HudController : MonoBehaviour
    {
    
        [Header("Buttons")]
        public Button ButtonIdle;
        public Button ButtonPatrol;
        public Button ButtonToBase;
        public Button ButtonExit;

        [Space]
        [Header("Bars")]
        public ProgressBar HpProgressBar;

        private AiAgent _agent;

        private IGameStateMachine _gameStateMachine;

    
        public void Init(IGameStateMachine gameStateMachine, AiAgent agent)
        {
            _gameStateMachine = gameStateMachine;
            _agent = agent;
        
            InitButtons();
            InitProgressBars();
        }

        private void InitButtons()
        {
            ButtonIdle.onClick.AddListener(() => _agent.Do(AiStateId.Idle));
            ButtonPatrol.onClick.AddListener(() => _agent.Do(AiStateId.Patrol));
            ButtonToBase.onClick.AddListener(() => _agent.Do(AiStateId.GoToBase));
            ButtonExit.onClick.AddListener(() => _gameStateMachine.Enter<MainMenuState>());
        }

        private void InitProgressBars()
        {
            HpProgressBar.SetProgress(_agent.HeroHealth.Health, _agent.HeroHealth.MaxHealth);
            _agent.HeroHealth.OnDamage += (health, maxHealth) =>
                HpProgressBar.SetProgress(health, maxHealth);
        }

    }
}