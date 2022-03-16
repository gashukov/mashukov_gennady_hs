using System;
using UnityEngine;

namespace Creatures
{
    public class HeroHealth : MonoBehaviour, IDamageReceiver
    {
        public float Health;
        public float MaxHealth;
        public Action OnDie;
        public Action<float, float> OnDamage;

        public void DealDamage(float damage = 1f)
        {
            Health = Mathf.Clamp(Health - damage, 0f, MaxHealth);
            OnDamage?.Invoke(Health, MaxHealth);
            if (Health == 0)
            {
                OnDie?.Invoke();
            }
        }
    
    
    }
}