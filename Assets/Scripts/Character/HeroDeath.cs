using System;
using UnityEngine;

namespace Scrips.Character
{
    [RequireComponent( typeof(HeroHealth) )]
    public class HeroDeath : MonoBehaviour
    {
        public HeroHealth _health;
        public HeroMove _move;
        public HeroAttack _attack;
        public HeroAnimator _animator;

        public GameObject _deathFx;
        private bool _idDead;

        private void Start() => 
            _health.HealthChanged += HealthChanged;

        private void OnDestroy() =>
            _health.HealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if (!_idDead && _health.Current <= 0)
                Debug();
        }

        private void Debug()
        {
            _idDead = true;

            _move.enabled = false;
            _attack.enabled = false;
            _animator.PlayDeath();

            Instantiate(_deathFx, transform.position, Quaternion.identity);
        }
    }
}