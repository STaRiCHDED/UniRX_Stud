using System;
using UniRx;
using UnityEngine;

namespace Scenes._5._SimpleShooter.Scripts.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class FirstPersonController : MonoBehaviour, IDisposable
    {
        [SerializeField] [Range(0, 30)] private float _movementSpeed = 10;
        [SerializeField] private float _jumpSpeed = 5;
        [SerializeField] private float _gravity = 9.81f;

        private CharacterController _characterController;
        private float _verticalVelocity;
        private PlayerInput _playerInput;
        private readonly CompositeDisposable _subscriptions = new();

        private void Awake()
        {
            _characterController = gameObject.GetComponent<CharacterController>();
            _playerInput = GetComponent<PlayerInput>();
        }

        private void Start()
        {
            _playerInput.Move
                .Subscribe(MovePlayer)
                .AddTo(_subscriptions);

            _playerInput.Jump
                .Subscribe(_ => JumpPlayer())
                .AddTo(_subscriptions);

            _playerInput.Look
                .Subscribe(RotatePlayer)
                .AddTo(_subscriptions);
        }

        private void MovePlayer(Vector3 direction)
        {
            var moveVector = new Vector3(direction.x, 0, direction.z) * _movementSpeed * Time.deltaTime;

            moveVector = transform.rotation * moveVector;
            if (!_characterController.isGrounded)
            {
                _verticalVelocity -= _gravity * Time.deltaTime;
            }
            else
            {
                _verticalVelocity = 0;
            }

            _characterController.Move(moveVector + new Vector3(0, _verticalVelocity, 0) * Time.deltaTime);
        }

        private void RotatePlayer(Vector3 direction)
        {
            var lookAngle = Quaternion.Euler(direction);
            transform.rotation = Quaternion.Euler(lookAngle.eulerAngles.x, lookAngle.eulerAngles.y, 0);
        }

        private void JumpPlayer()
        {
            _verticalVelocity = _jumpSpeed;
        }

        public void Dispose()
        {
            _subscriptions.Dispose();
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}