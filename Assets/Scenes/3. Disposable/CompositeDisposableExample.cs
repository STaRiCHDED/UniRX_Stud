using System;
using UniRx;
using UnityEngine;

namespace Scenes._3._Disposable
{
    public class CompositeDisposableExample : MonoBehaviour , IDisposable
    {
        private readonly CompositeDisposable _disposables = new();
        private void Start()
        {
            // Подписка на событие обновления каждого кадра
            Observable.EveryUpdate()
                .Subscribe(_ => Debug.Log("Update called"))
                .AddTo(_disposables);

            // Подписка на событие, когда кнопка мыши нажата
            Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButtonDown(0))
                .Subscribe(_ => Debug.Log("Mouse button pressed"))
                .AddTo(_disposables);

            // Подписка на событие, когда кнопка мыши отпущена
            Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButtonUp(0))
                .Subscribe(_ => Debug.Log("Mouse button released"))
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            // Отписываемся от всех подписок одновременно при уничтожении объекта
            _disposables?.Dispose();
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}