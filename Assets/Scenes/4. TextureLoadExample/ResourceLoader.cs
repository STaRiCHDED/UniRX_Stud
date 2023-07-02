using System;
using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;

namespace Scenes._4._TextureLoadExample
{
    public class ResourceLoader
    {
        public IObservable<Texture2D> LoadTextureByUrl(string url)
        {
            return Observable.FromCoroutine<Texture2D>(observer => LoadTextureCoroutine(url, observer));
        }

        public IObservable<Texture2D> LoadTextureFromResources(string filePath)
        {
            return Observable.FromCoroutine<Texture2D>(observer =>
                LoadTextureFromResourcesCoroutine(filePath, observer));
        }

        private IEnumerator LoadTextureCoroutine(string url, IObserver<Texture2D> observer)
        {
            // Загружаем текстуру асинхронно
            using var request = UnityWebRequestTexture.GetTexture(url);
            // Ожидаем завершения загрузки текстуры
            yield return request.SendWebRequest();


            if (request.result == UnityWebRequest.Result.Success)
            {
                // Получаем загруженную текстуру
                var texture = DownloadHandlerTexture.GetContent(request);
                observer.OnNext(texture);
                observer.OnCompleted();
            }
            else
            {
                observer.OnError(new Exception(request.error));
            }
        }

        private IEnumerator LoadTextureFromResourcesCoroutine(string filePath, IObserver<Texture2D> observer)
        {
            // Загружаем текстуру асинхронно
            ResourceRequest request = Resources.LoadAsync<Texture2D>(filePath);

            // Ожидаем завершения загрузки текстуры
            yield return request;

            // Получаем загруженную текстуру
            Texture2D texture = request.asset as Texture2D;

            if (texture != null)
            {
                observer.OnNext(texture);
                observer.OnCompleted();
            }
            else
            {
                observer.OnError(new Exception("Texture is null"));
            }
        }
    }
}