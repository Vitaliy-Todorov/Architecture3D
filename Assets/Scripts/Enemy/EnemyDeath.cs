using System;
using System.Collections;
using UnityEngine;

namespace Scrips.Enemy
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyDeath : MonoBehaviour
    {
        public Follow _follow;
        public EnemyHealth _health;
        public EnemyAnimator _animator;

        public GameObject _deathFx;

        public event Action Happened;

        private void Start() => 
            _health.HealthChanged += HealthChanged;

        private void OnDestroy() =>
            _health.HealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if (_health.Current <= 0)
                Die();
        }

        private void Die()
        {
            _follow.enabled = false;
            _health.HealthChanged -= HealthChanged;

            _animator.PlayDeath();
            SpawnDeathFx();
            StartCoroutine(DestroyTimer());

            Happened?.Invoke();
        }

        private void SpawnDeathFx()
        {
            Instantiate(_deathFx, transform.position, Quaternion.identity);
        }

        private IEnumerator DestroyTimer()
        {
            yield return new WaitForSeconds(3);
            Destroy(gameObject);
        }
    }
}