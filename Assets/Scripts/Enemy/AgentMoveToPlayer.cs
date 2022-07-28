using Scripts.Infrastructure.Factory;
using Scripts.Infrastructure.Services;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Scrips.Enemy
{
    public class AgentMoveToPlayer : Follow
    {
        private const float _minimalDistance = 1;

        public NavMeshAgent _agent;

        private Transform _heroTransform;
        private IGameFactory _gameFactory;

        private void Start()
        {
            _gameFactory = AllServices.Container.Single<IGameFactory>();

            if (_gameFactory.HeroGameObject != null)
                IntializeHeroTransform();
            else
                _gameFactory.HeroCreated += IntializeHeroTransform;
        }

        private void Update()
        {
            if (_heroTransform != null && HeroNotReached())
                _agent.destination = _heroTransform.position;
        }

        private void IntializeHeroTransform() =>
            _heroTransform = _gameFactory.HeroGameObject.transform;

        private bool HeroNotReached()
        {
            return Vector3.Distance(_agent.transform.position, _heroTransform.position) >= _minimalDistance;
        }
    }
}