using System;
using System.Collections;
using Scenes._5._SimpleShooter.Scripts.Player;
using UniRx;
using UnityEngine;

namespace Scenes._5._SimpleShooter.Scripts.Weapon
{
    public class ExplosionWeapon : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private Transform _muzzle;
        [SerializeField] private float _explosionBulletForce;
        [SerializeField] private float _delay = 0.2f;
        [SerializeField] private float _explosionDamage = 10;

        
        private void Start()
        {
            _playerInput.Shoot
                .ThrottleFirst(TimeSpan.FromSeconds(_delay)) 
                // ThrottleFirst используется для игнорирования последующих событий
                // в течение указанного интервала времени _delay.
                // Таким образом, мы реализуем кулдаун между выстрелами.
                .Subscribe(_ => Shoot())
                .AddTo(this);
        }

        private void Shoot()
        {
            var bullet = Instantiate(_bulletPrefab, _muzzle.position, _muzzle.rotation);
            
            bullet.Initialize(_explosionDamage);
            bullet.AddForce(_explosionBulletForce);
        }
    }
}