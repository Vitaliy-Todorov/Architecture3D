using Scrips.Logic.Animator_;
using System;
using UnityEngine;

namespace Scrips.Enemy
{
    public class EnemyAnimator : MonoBehaviour
    {
        // Название тригеров
        private static readonly int _attackParameters = Animator.StringToHash("Attack_1");
        private static readonly int _speedParameters = Animator.StringToHash("Speed");
        private static readonly int _isMovingParameters = Animator.StringToHash("IsMoving");
        private static readonly int _hitParameters = Animator.StringToHash("Hit");
        private static readonly int _dieParameters = Animator.StringToHash("Die");

        // Название анимаций
        private static readonly int _idleStateHash = Animator.StringToHash("idle");
        private static readonly int _attackStatHash = Animator.StringToHash("attack01");
        private static readonly int _walkingStateHash = Animator.StringToHash("Move");
        private static readonly int _deathStateHash = Animator.StringToHash("die");

        private Animator _animator;

        public event Action<AnimatorState> stateEntered;
        public event Action<AnimatorState> stateExited;

        public AnimatorState State { get; private set; }

        private void Awake() =>
            _animator = GetComponent<Animator>();

        public void PlayHit() => _animator.SetTrigger(_hitParameters);
        public void PlayDeath() => _animator.SetTrigger(_dieParameters);

        public void Move(float speed)
        {
            _animator.SetBool(_isMovingParameters, true);
            _animator.SetFloat(_speedParameters, speed);
        }

        public void StopMoving() => _animator.SetBool(_isMovingParameters, false);
        public void PlayAttack() => _animator.SetTrigger(_attackParameters);

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            stateEntered?.Invoke(State);
        }

        public void ExitedState(int stateHash) =>
            stateExited?.Invoke(StateFor(stateHash));

        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;

            if (stateHash == _idleStateHash)
                state = AnimatorState.Idle;
            else if (stateHash == _attackStatHash)
                state = AnimatorState.Attack;
            else if (stateHash == _walkingStateHash)
                state = AnimatorState.Walking;
            else if (stateHash == _deathStateHash)
                state = AnimatorState.Died;
            else
                state = AnimatorState.Unknown;

            return state;
        }
    }
}