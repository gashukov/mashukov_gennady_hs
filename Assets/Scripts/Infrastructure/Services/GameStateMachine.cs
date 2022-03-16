using System;
using System.Collections.Generic;
using AssetManagement;
using Infrastructure.States;
using UI;

namespace Infrastructure.Services
{
    public class GameStateMachine : IGameStateMachine
    {
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(IAssetProvider assetProvider, IStaticDataService staticDataService, ISceneLoader sceneLoader, IGameFactory gameFactory, LoadingCurtain loadingCurtain)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, assetProvider, staticDataService),
                [typeof(MainMenuState)] = new MainMenuState(this, sceneLoader, loadingCurtain),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, gameFactory, loadingCurtain),
                [typeof(GameLoopState)] = new GameLoopState(this),
            };
      
        }
    
        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
      
            TState state = GetState<TState>();
            _activeState = state;
      
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;
    }
}