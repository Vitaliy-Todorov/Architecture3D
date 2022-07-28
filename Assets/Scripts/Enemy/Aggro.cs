using System;
using System.Collections;
using UnityEngine;

namespace Scrips.Enemy
{
    public class Aggro : MonoBehaviour
    {
        public TriggerObserver _triggerObserver;
        public Follow _follow;

        public float _cooldown;

        Coroutine _aggroCoroutine;
        private bool _hesAggroTarget;

        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;

            _follow.enabled = false;
        }
        private void TriggerEnter(Collider obj)
        {
            if (!_hesAggroTarget)
            {
                _hesAggroTarget = true;

                StopAggroCoroutine();
                SwitchFollow(true);
            }
        }

        private void TriggerExit(Collider obj)
        {
            if (_hesAggroTarget)
            {
                _hesAggroTarget = false;
                _aggroCoroutine = StartCoroutine(SwitchFollowOffAfterCooldawn());
            }
        }

        private void StopAggroCoroutine()
        {
            if(_aggroCoroutine != null)
            {
                StopCoroutine(_aggroCoroutine);
                _aggroCoroutine = null;
            }
        }

        private IEnumerator SwitchFollowOffAfterCooldawn()
        {
            yield return new WaitForSeconds(_cooldown);

            SwitchFollow(false);
        }

        private void SwitchFollow(bool OffOrOn) =>
            _follow.enabled = OffOrOn;
    }
}