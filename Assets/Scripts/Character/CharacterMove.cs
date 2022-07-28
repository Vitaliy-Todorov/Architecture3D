using Scripts.Data;
using Scripts.Infrastructure.Services;
using Scripts.Infrastructure.Services.PersistentProgress;
using Scripts.CameraLogic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scripts.Infrastructure.Services.InputService;

namespace Scrips.Character
{
    public partial class CharacterMove : MonoBehaviour, ISavedProgress
    {
        public Rigidbody2D _rigidbody;
        public float _movementSpeed;

        private IInputService _inputService;
        private Camera _camera;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }

        private void Start()
        {
            CameraFllow(gameObject);
        }

        private static void CameraFllow(GameObject character)
        {
            Camera.main
                .GetComponent<CameraFollow>()
                .Follow(character);
        }

        private void Update()
        {
            Vector2 movmentVector = _inputService.Axis;

            if (movmentVector != Vector2.zero && _rigidbody.velocity.sqrMagnitude < _movementSpeed)
            {
                Rotate(movmentVector);
                Move(movmentVector);
            }
        }

        private void Move(Vector2 movmentVector) => 
            _rigidbody.velocity += _movementSpeed * movmentVector;

        private void Rotate(Vector2 movmentVector)
        {
            float angle = Mathf.Atan2(movmentVector.x, movmentVector.y) * Mathf.Rad2Deg;
            transform.eulerAngles = -new Vector3(0, 0, angle);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            Vector3Data position = transform.position.AsVectorData();
            progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), position);
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if(CurrentLevel() == progress.WorldData.PositionOnLevel.Level)
            {
                Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
                if(savedPosition != null)
                    transform.position = savedPosition.AsUnityVector();
            }
        }

        private static string CurrentLevel() =>
            SceneManager.GetActiveScene().name;
    }
}