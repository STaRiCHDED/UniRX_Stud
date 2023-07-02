using UnityEngine;

namespace Scenes._1._Introduction.Pull_Push_Models
{
    // Pull модель: объект позволяет получить текущее значение здоровья персонажа по запросу
    public class HealthControllerPullExample : MonoBehaviour
    {
        private int _currentHealth;

        public void TakeDamage(int newHealth)
        {
            _currentHealth -= newHealth;
        }

        public int GetCurrentHealth()
        {
            return _currentHealth;
        }
    }
}