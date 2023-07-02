using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes._4._TextureLoadExample
{
    public class LoadTextureExamples : MonoBehaviour
    {
        [SerializeField] private Image _onlineImage;
        [SerializeField] private Image _resourcesImage;


        private readonly string _url =
            "https://www.tubefilter.com/wp-content/uploads/2021/02/google-stadia-studio-close-1920x1131.jpg";

        private readonly string _texturePath = "bg";

        private void Start()
        {
            var resourceLoader = new ResourceLoader();

            resourceLoader.LoadTextureByUrl(_url)
                .Subscribe(texture => _onlineImage.sprite = GetSprite(texture), OnError)
                .AddTo(this);

            resourceLoader.LoadTextureFromResources(_texturePath)
                .Subscribe(texture => _resourcesImage.sprite = GetSprite(texture), OnError)
                .AddTo(this);
        }

        private Sprite GetSprite(Texture2D texture)
        {
            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }

        private void OnError(Exception error)
        {
            Debug.LogError($"Error loading texture: {error.Message}");
        }
    }
}