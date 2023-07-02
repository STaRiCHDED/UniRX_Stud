using UnityEngine;
using UniRx;

namespace Scenes._1._Introduction.Pull_Push_Models
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private HealthControllerPushExample _controllerPushExample;
        [SerializeField] private HealthControllerPullExample _controllerPullExample;

        private void Start()
        {
            _controllerPushExample.HealthSubject
                .Subscribe(OnHealthChanged) // Подписываемся на события изменения здоровья и вызываем метод OnHealthChanged
                .AddTo(this); // Добавляем подписку в список, чтобы автоматически отписаться при уничтожении объекта
        }

        private void Update()
        {
            // Получаем текущее здоровье из pull модели и проверяем его
            var currentHealth = _controllerPullExample.GetCurrentHealth();
            CheckHealth(currentHealth);
        }

        private void OnHealthChanged(int newHealth)
        {
            // Проверяем здоровье при получении обновлений в push модели
            CheckHealth(newHealth);
        }

        private void CheckHealth(int health)
        {
            if (health <= 0)
            {
                Debug.Log("Персонаж погиб");
            }
            else
            {
                Debug.Log("Персонаж жив");
            }
        }
    }
}