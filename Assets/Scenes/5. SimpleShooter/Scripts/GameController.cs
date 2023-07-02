using Scenes._5._SimpleShooter.Scripts.Player;
using Scenes._5._SimpleShooter.Scripts.UI_Scripts;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scenes._5._SimpleShooter.Scripts
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private HealthController _playerHealthController;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Image _deathScreen;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private ScoreDisplay _scoreDisplay;

        private void Start()
        {
            _restartButton.interactable = false;
            _deathScreen.gameObject.SetActive(false);
            
            _playerHealthController.Health
                .Where(health => health <= 0)
                .Subscribe(_ => OnPlayerDied() )
                .AddTo(this);
            
            
            _restartButton
                .OnClickAsObservable()
                .Subscribe(_ => RestartGame())
                .AddTo(this);
        }

        private void OnPlayerDied()
        {
            Cursor.lockState = CursorLockMode.Confined;
            _deathScreen.gameObject.SetActive(true);
            _restartButton.interactable = true;
            _playerInput.enabled = false; 
            _scoreDisplay.OnFinishGame();
        }


        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}