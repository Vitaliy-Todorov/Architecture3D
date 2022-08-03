using Scripts.Infrastructure.Services.Factory;
using Scripts.Infrastructure.Services;
using System;
using System.Linq;
using UnityEngine;
using Scripts.Logic;

namespace Scrips.Enemy
{
    [RequireComponent( typeof(EnemyAnimator) )]
    public class EnemyAttack : MonoBehaviour
    {
        public EnemyAnimator _animator;
        public float _attackCooldown = 3f;
        public float _cleaveg = 0.5f;
        public float _effectiveDistance = 0.5f;
        public float _damage = 10f;

        private Transform _heroTransform;
        private float _attackCooldownCurrent;

        private bool _isAttacking;
        private Collider[] _hits = new Collider[1];
        private int _layerMask;
        private bool _attackIsActive;

        internal void Construct(Transform heroTransform) =>
            _heroTransform = heroTransform;

        private void Awake() => 
            _layerMask = 1 << LayerMask.NameToLayer("Player");

        private void Update()
        {
            if (_attackCooldownCurrent > 0)
                _attackCooldownCurrent -= Time.deltaTime;
            else if (!_isAttacking && _attackIsActive)
                StartAttack();
        }

        private void OnAttack()
        {
            if( Hit(out Collider hit) )
            {
                PhysicsDebug.DrawDebug(StartPoin(), _cleaveg, 1);
                hit.transform.GetComponent<IHealth>().TakeDamage(_damage);
            }
        }

        private void OnAttackEnded()
        {
            _attackCooldownCurrent = _attackCooldown;
            _isAttacking = false;
        }

        internal void EnableAttack() => 
            _attackIsActive = true;

        internal void DisableAttack() => 
            _attackIsActive = false;

        private void StartAttack()
        {
            transform.LookAt(_heroTransform);
            _animator.PlayAttack();

            _isAttacking = true;
        }

        private bool Hit(out Collider hit)
        {
            int hitsCount = Physics.OverlapSphereNonAlloc(StartPoin(), _cleaveg, _hits, _layerMask);

            hit = _hits.FirstOrDefault();

            return hitsCount > 0;
        }

        private Vector3 StartPoin()
        {
            return transform.position + new Vector3(0, 0.5f, 0) + transform.forward * _effectiveDistance;
        }
    }
}