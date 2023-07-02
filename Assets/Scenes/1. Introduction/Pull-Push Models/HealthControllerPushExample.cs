using UniRx;
using UnityEngine;

namespace Scenes._1._Introduction.Pull_Push_Models
{
    // Push модель: объект активно отправляет событие при изменении значения здоровья персонажа
    public class HealthControllerPushExample : MonoBehaviour
    {
        public readonly Subject<int> HealthSubject = new Subject<int>();
        private int _currentHealth;

        public void TakeDamage(int newHealth)
        {
            _currentHealth -= newHealth;
            HealthSubject.OnNext(_currentHealth);
        }
    }
}