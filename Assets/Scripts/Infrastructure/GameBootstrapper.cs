using Helpers;
using Infrastructure.Services;
using Infrastructure.States;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        public IGameStateMachine StateMachine;
    
        private void Awake()
        {
            StateMachine.Enter<BootstrapState>();

            DontDestroyOnLoad(this);
        }

        [Inject]
        public void Construct(IGameStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
    }
}