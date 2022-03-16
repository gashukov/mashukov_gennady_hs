using System;
using StaticData;
using UnityEngine;

namespace Creatures.Ai
{
    public class AiAgent : MonoBehaviour
    {
    
        [HideInInspector] public HeroStaticData HeroData;
        [HideInInspector] public Rigidbody Rigidbody;
        [HideInInspector] public Action<Collider> OnTriggerWaypoint;
        [HideInInspector] public Animator Animator;
        [HideInInspector] public HeroMove HeroMove;
        [HideInInspector] public HeroHealth HeroHealth;
        [HideInInspector] public Transform CurrentWaypoint;
    
        private AiStateMachine _stateMachine;
    

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Animator = GetComponent<Animator>();
            HeroMove = GetComponent<HeroMove>();
            HeroHealth = GetComponent<HeroHealth>();
        }

        public void Construct(HeroStaticData heroStaticData)
        {
            InitStateMachine();
            InitData(heroStaticData);
            InitActions();

            _stateMachine.ChangeState(HeroData.InitialState);
        }
    
        public void Do(AiStateId newState)
        {
            if (_stateMachine.CurrentState != AiStateId.Death)
            {
                _stateMachine.ChangeState(newState);
            }
        }

        public bool IsOnBase()
        {
            return HeroData.BaseWaypoint.Equals(CurrentWaypoint);
        }

        private void InitData(HeroStaticData heroStaticData)
        {
            HeroData = heroStaticData;
            CurrentWaypoint = HeroData.BaseWaypoint;
            HeroHealth.MaxHealth = heroStaticData.Health;
            HeroHealth.Health = HeroHealth.MaxHealth;
        }

        private void InitActions()
        {
            HeroHealth.OnDie += () => Do(AiStateId.Death);
            HeroHealth.OnDamage += (health, maxHealth) => Animator.SetTrigger("Damaged");
        }


        private void InitStateMachine()
        {
            _stateMachine = new AiStateMachine(this);
            _stateMachine.RegisterState(new AiIdleState());
            _stateMachine.RegisterState(new AiPatrolState());
            _stateMachine.RegisterState(new AiGoToBase());
            _stateMachine.RegisterState(new AiDeathState());
        }

        private void FixedUpdate()
        {
            _stateMachine?.Update();
            Animator.SetFloat("Speed", Vector3.Scale(Rigidbody.velocity, new Vector3(1, 0, 1)).magnitude);
            Animator.SetFloat("Health", HeroHealth.Health);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Waypoint"))
            {
                CurrentWaypoint = other.transform;
                OnTriggerWaypoint?.Invoke(other);
            }   
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Waypoint"))
            {
                CurrentWaypoint = null;
            }
        }
    }
}