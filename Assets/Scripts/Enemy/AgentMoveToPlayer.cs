using UnityEngine;
using UnityEngine.AI;

namespace Scrips.Enemy
{
    public class AgentMoveToPlayer : Follow
    {
        private const float _minimalDistance = 1;

        public NavMeshAgent _agent;

        private Transform _heroTransform;

        internal void Construct(Transform heroTransform) =>
            _heroTransform = heroTransform;

        private void Update()
        {
            if (_heroTransform != null && HeroNotReached())
                _agent.destination = _heroTransform.position;
        }

        private bool HeroNotReached() =>
            Vector3.Distance(_agent.transform.position, _heroTransform.position) >= _minimalDistance;
    }
}