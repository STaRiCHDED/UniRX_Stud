using System;
using UniRx;
using UnityEngine;

namespace Scenes._3._Disposable
{
    public class DisposeExample : MonoBehaviour
    {
        private IDisposable _subscription;

        private void Start()
        {
            var subject = new Subject<int>();

            _subscription = subject
                .Subscribe(value => Debug.Log(value)); // Подписка на поток данных
        }

        private void OnDestroy()
        {
            _subscription.Dispose(); // Отписка от потока данных
        }
    }
}