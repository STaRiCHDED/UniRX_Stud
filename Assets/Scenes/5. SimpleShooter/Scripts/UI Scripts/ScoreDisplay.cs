using TMPro;
using UniRx;
using UnityEngine;

namespace Scenes._5._SimpleShooter.Scripts.UI_Scripts
{
    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField] private ScoreManager _scoreManager;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _finishScoresText;

        
        private void Start()
        {
            _scoreManager.OnScoreChanged
                .Subscribe(score => _scoreText.text = "Score: " + score)
                .AddTo(this);
        }

        public void OnFinishGame()
        {
            _finishScoresText.gameObject.SetActive(true);
            _scoreManager.OnScoreChanged
                .Subscribe(score => _finishScoresText.text += ", " + score);
        }
    }
}