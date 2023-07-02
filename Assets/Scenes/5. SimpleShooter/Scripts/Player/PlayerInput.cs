using System;
using UniRx;
using UnityEngine;

namespace Scenes._5._SimpleShooter.Scripts.Player
{
    [RequireComponent(typeof(FirstPersonController))]
    public class PlayerInput : MonoBehaviour
    {
        private const float MAX_LOOK_UP = 50;
        private const float MIN_LOOK_DOWN = -40;
        public IObservable<Unit> Shoot => _shootSubject;
        public IObservable<Unit> Jump => _jumpSubject;
        public IObservable<Vector3> Move => _moveSubject;
        public IObservable<Vector3> Look => _lookSubject;

        [SerializeField] private float _scrollSensitivity = 2f;
        
        private readonly Subject<Unit> _shootSubject = new();
        private readonly Subject<Vector3> _moveSubject = new();
        private readonly Subject<Vector3> _lookSubject = new();
        private readonly Subject<Unit> _jumpSubject = new();
        private float _vertical;
        private float _horizontal;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _shootSubject.OnNext(default);
            }

            if (Input.GetButtonDown("Jump"))
            {
                _jumpSubject.OnNext(default);
            }

            var moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _moveSubject.OnNext(moveVector);

            _vertical -= Mathf.Clamp(Input.GetAxis("Mouse Y") * _scrollSensitivity, MIN_LOOK_DOWN, MAX_LOOK_UP);
            _horizontal += Input.GetAxis("Mouse X") * _scrollSensitivity;
            var lookVector = new Vector3(_vertical, _horizontal, 0);
            _lookSubject.OnNext(lookVector);
        }
        
    }
}