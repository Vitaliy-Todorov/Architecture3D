using Scripts.Infrastructure.Factory;
using Scripts.Infrastructure.Services;
using System;
using UnityEngine;

namespace Scrips.Enemy
{
    public class RotateToHero : Follow
    {
        public float _speed;

        private Transform _heroTransform;
        private IGameFactory _gameFactory;
        private Vector3 _positionToLook;

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
            if (_heroTransform != null)
                RotateTowardsHero();
        }

        private void IntializeHeroTransform() =>
            _heroTransform = _gameFactory.HeroGameObject.transform;

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