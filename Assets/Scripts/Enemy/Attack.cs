using Scripts.Infrastructure.Factory;
using Scripts.Infrastructure.Services;
using System;
using System.Linq;
using UnityEngine;

namespace Scrips.Enemy
{
    [RequireComponent( typeof(EnemyAnimator) )]
    public class Attack : MonoBehaviour
    {
        public EnemyAnimator _animator;
        public float _attackCooldown = 3f;
        public float _cleaveg = 0.5f;
        public float _effectiveDistance = 0.5f;

        private IGameFactory _factory;
        private Transform _heroTransform;
        private float _attackCooldownCurrent;

        private bool _isAttacking;
        private Collider[] _hits = new Collider[1];
        private int _layerMask;
        private bool _attackIsActive;

        private void Awake()
        {
            _factory = AllServices.Container.Single<IGameFactory>();
            _factory.HeroCreated += OnHeroCreated;
            _layerMask = 1 << LayerMask.NameToLayer("Player");
        }

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

        private void OnHeroCreated() =>
            _heroTransform = _factory.HeroGameObject.transform;

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