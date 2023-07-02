using System;
using Scenes._5._SimpleShooter.Scripts.Weapon;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scenes._5._SimpleShooter.Scripts
{
    public class ScoreManager : MonoBehaviour
    {
        public IObservable<int> OnScoreChanged => _scoreSubject;
        private readonly ReplaySubject<int> _scoreSubject = new(100);

        private int _score;

        private void Awake()
        {
            var boxes = FindObjectsOfType<ExplodingBox>(); // поиск всех взрываемых боксов на сцене
            
            foreach (var box in boxes)
            {
                box.OnExploded
                    .Subscribe(_ => AddRandomScore())
                    .AddTo(this);
            }
        }


        private void AddRandomScore()
        {
            var randomScore = Random.Range(1, 100);
            _score += randomScore;
            _scoreSubject.OnNext(_score);
        }
    }
}