using System;
using UnityEngine;

namespace Scrips.Enemy
{
    [RequireComponent(typeof(EnemyAttack))]
    public class CheckAttackRange : MonoBehaviour
    {
        public EnemyAttack _attack;
        public TriggerObserver _triggerObserver;

        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;

            _attack.DisableAttack();
        }

        private void TriggerExit(Collider obj)
        {
            _attack.DisableAttack();
        }

        private void TriggerEnter(Collider obj)
        {
            _attack.EnableAttack();
        }
    }
}