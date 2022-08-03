using Scripts.Data;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scripts.Logic;
using System;
using UnityEngine;

namespace Scrips.Character
{
    [RequireComponent(typeof(HeroAnimator))]
    public class HeroHealth : MonoBehaviour, ISavedProgress, IHealth
    {
        public HeroAnimator Animator;
        private State _heroState;

        public event Action HealthChanged;

        public float Current
        {
            get => _heroState.CurrentHP;
            set
            {
                if(_heroState.CurrentHP != value)
                {
                    _heroState.CurrentHP = value;
                    HealthChanged?.Invoke();
                }
            }
        }
        public float Max 
        { 
            get => _heroState.MaxHP; 
            set => _heroState.MaxHP = value; 
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _heroState = progress.HeroState;
            HealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HeroState.CurrentHP = Current;
            progress.HeroState.MaxHP = Max;
        }

        public void TakeDamage(float damage)
        {
            if(Current > 0)
            {
                Current -= damage;
                Animator.PlayHit();
            }
        }
    }
}