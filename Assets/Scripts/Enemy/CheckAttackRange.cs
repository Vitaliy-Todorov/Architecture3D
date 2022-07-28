using System;
using UnityEngine;

namespace Scrips.Enemy
{
    [RequireComponent(typeof(Attack))]
    public class CheckAttackRange : MonoBehaviour
    {
        public Attack _attack;
        public TriggerObserver _triggerObserver;

        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;

            _attack.DisableAttack();
        }

        private void TriggerExit(Collider obj)
        {
            _attack.EnableAttack();
        }

        private void TriggerEnter(Collider obj)
        {
            _attack.DisableAttack();
        }
    }
}