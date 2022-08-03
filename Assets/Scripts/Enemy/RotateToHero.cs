using Scripts.Infrastructure.Services.Factory;
using Scripts.Infrastructure.Services;
using System;
using UnityEngine;

namespace Scrips.Enemy
{
    public class RotateToHero : Follow
    {
        public float _speed = 5;

        private Transform _heroTransform;

        internal void Construct(Transform heroTransform) =>
            _heroTransform = heroTransform;

        private void Update()
        {
            if (_heroTransform != null)
                RotateTowardsHero();
        }

        private void RotateTowardsHero()
        {
            Vector3 positionDiff = _heroTransform.position - transform.position;
            Vector3 positionToLook = new Vector3(positionDiff.x, transform.position.y, positionDiff.z);

            transform.rotation = Quaternion.Lerp
                (
                transform.rotation, 
                Quaternion.LookRotation(positionToLook), 
                _speed * Time.deltaTime
                );
        }
    }
}