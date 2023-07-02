using System;
using UniRx;
using UnityEngine;

namespace Scenes._5._SimpleShooter.Scripts.Player
{
    public class HealthController : MonoBehaviour
    {
        public IObservable<float> Health => _healthSubject;

        [field: SerializeField] public int MaxHealth { get; private set; } = 100;

        private BehaviorSubject<float> _healthSubject;


        private void Start()
        {
            _healthSubject = new BehaviorSubject<float>(MaxHealth);
        }

        public void TakeDamage(float damage)
        {
            _healthSubject.OnNext(_healthSubject.Value - damage);
        }

        public void AddHealth(int health)
        {
            var newHealth = Mathf.Clamp(_healthSubject.Value + health, 0, MaxHealth);
            _healthSubject.OnNext(newHealth);
        }
    }
}