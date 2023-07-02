using System.Collections;
using Scenes._5._SimpleShooter.Scripts.Player;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes._5._SimpleShooter.Scripts.UI_Scripts
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;
        [SerializeField] private TextMeshProUGUI _healthPercentText;
        [SerializeField] private HealthController _playerHealthController;
        [SerializeField] private float _fillAmountSpeed = 2f;
        private Coroutine _fillAmountCoroutine;
        

        private void Start()
        {
            _playerHealthController.Health
                .Subscribe(UpdateHealthUI)
                .AddTo(this);
        }
        
        private void UpdateHealthUI(float health)
        {
            var clampedHealth = Mathf.Clamp(health, 0f, _playerHealthController.MaxHealth);
            var targetFillAmount = clampedHealth / _playerHealthController.MaxHealth;
            
            if (_fillAmountCoroutine != null)
            {
                StopCoroutine(_fillAmountCoroutine);
            }
            
            _fillAmountCoroutine = StartCoroutine(ChangeFillAmount(_healthBar.fillAmount, targetFillAmount));
            _healthPercentText.text = Mathf.RoundToInt(clampedHealth).ToString(); 
        }
        
        private IEnumerator ChangeFillAmount(float startFillAmount, float targetFillAmount)
        {
            var elapsedTime = 0f;

            while (elapsedTime < _fillAmountSpeed)
            {
                var fillAmount = Mathf.Lerp(startFillAmount, targetFillAmount, elapsedTime * _fillAmountSpeed);
                _healthBar.fillAmount = fillAmount;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _healthBar.fillAmount = targetFillAmount;
        }
    }
}