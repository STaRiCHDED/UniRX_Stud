using UniRx;
using UnityEngine;

namespace Scenes._3._Disposable
{
    public class AddToExample : MonoBehaviour
    {
        private void Start()
        {
            var subject = new Subject<int>();

            subject
                .Subscribe(value => Debug.Log(value)) // Подписка на поток данных
                .AddTo(this); // Привязка подписки к объекту "this"
            // При уничтожении объекта "this",
            // подписка будет автоматически отписана
            // и ресурсы будут освобождены
        }
    }
}