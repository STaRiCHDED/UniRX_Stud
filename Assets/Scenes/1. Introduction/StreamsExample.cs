using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Scenes._1._Introduction
{
    public class StreamsExample : MonoBehaviour
    {
        [SerializeField] private Collider _collider;
        
        private void Start()
        {
            _collider.OnCollisionEnterAsObservable() // Получаем поток данных столкновения с коллайдером
                .Where(_ => CompareTag("Player")) // Фильтруем только столкновения с объектами, у которых тег "Player"
                .Subscribe(OnPlayerCollision) // Подписываемся на поток данных и вызываем метод OnPlayerCollider для каждого события столкновения
                .AddTo(this); // Добавляем подписку в список, связанный с объектом, чтобы автоматически отписаться при уничтожении объекта
        }
        
        private void OnPlayerCollision(Collision collision)
        {
            // Обработка столкновения с игроком
            Debug.Log(collision.gameObject.name);
        }
    }
}