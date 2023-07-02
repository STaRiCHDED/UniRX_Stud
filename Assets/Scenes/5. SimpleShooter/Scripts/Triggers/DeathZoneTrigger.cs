using Scenes._5._SimpleShooter.Scripts.Player;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Scenes._5._SimpleShooter.Scripts.Triggers
{
    public class DeathZoneTrigger : MonoBehaviour
    {
        
        private void Start()
        {
            gameObject.OnTriggerEnterAsObservable()
                .Subscribe(collider =>
            {
                if (collider.TryGetComponent(out HealthController healthController))
                {
                    healthController.TakeDamage(healthController.MaxHealth);
                }
            }).AddTo(this);
        }
    }
}
