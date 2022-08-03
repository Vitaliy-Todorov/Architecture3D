using UnityEngine;
using UnityEngine.AI;

namespace Scrips.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(EnemyAnimator))]

    public class AnimateAlongAgent : MonoBehaviour
    {
        private const float _minimalVelocity = 0.1f;

        public NavMeshAgent _agent;
        public EnemyAnimator _animator;

        private void Update()
        {
            if (ShoulMove())
                _animator.Move(_agent.velocity.magnitude);
            else
                _animator.StopMoving();
        }

        //Проверяем что скорость больше минимальной и что персонаж не упёрся в цель
        private bool ShoulMove() => 
            _agent.velocity.magnitude > _minimalVelocity && _agent.remainingDistance > _agent.radius;
    }
}