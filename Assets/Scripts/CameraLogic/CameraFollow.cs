using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        // public float RotationAngleX;
        // Высота расположения камер над персонажем
        // public float OffsetZ;
        public float _distance;
        public float _rotationAngleX;
        public float _offsetY;

        [SerializeField]
        private Transform _following;

        private void LateUpdate()
        {
            if (_following == null)
                return;

            // Угол камеры над персонажем
            Quaternion rotation = Quaternion.Euler(_rotationAngleX, 0, 0);
            Vector3 position = rotation * new Vector3(0, 0, -_distance) + FollowingPointPosition();

            transform.rotation = rotation;
            transform.position = position;
        }

        public void Follow(GameObject followinf) => _following = followinf.transform;

        private Vector3 FollowingPointPosition()
        {
            Vector3 followingPosition = _following.position;
            followingPosition.y += _offsetY;

            return followingPosition;
        }
    }
}