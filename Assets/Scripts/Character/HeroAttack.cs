using Scripts.Data;
using Scripts.Infrastructure.Services;
using Scripts.Infrastructure.Services.InputService;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scripts.Logic;
using UnityEngine;

namespace Scrips.Character
{
    [RequireComponent( typeof(HeroAnimator), typeof(CharacterController) )]
    public class HeroAttack : MonoBehaviour, ISavedProgressReader
    {
        public HeroAnimator _animator;
        public CharacterController _characterController;
        public IInputService _input;

        private int _layerMask;
        private Stats _stats;
        private Collider[] _hit = new Collider[3];

        private void Awake()
        {
            _input = AllServices.Container.Single<IInputService>();
            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }

        private void Update()
        {
            if (_input.AttackButtonUp && !_animator.IsAttacking)
                _animator.PlayAttack();
        }  

        public void OnAttack()
        {
            for (int i = 0; i < Hit(); i++)
                _hit[i].transform.parent
                    .GetComponent<IHealth>()
                    .TakeDamage(_stats.Damage);
        }

        public void LoadProgress(PlayerProgress progress) =>
            _stats = progress.HeroStats;

        public int Hit() => 
            Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, _stats.DamageRadius, _hit, _layerMask);

        private Vector3 StartPoint() =>
            new Vector3(transform.position.x, _characterController.center.y / 2, transform.position.z);
    }
}