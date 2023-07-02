using Scenes._5._SimpleShooter.Scripts.Player;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Scenes._5._SimpleShooter.Scripts.Triggers
{
    [RequireComponent(typeof(BoxCollider))]

    public class MedicineTrigger : MonoBehaviour 
    {
        [SerializeField]
        private int _healthValue; 
    
        private void Start()
        {
            gameObject.OnTriggerEnterAsObservable()
                .Subscribe(collider =>
                {
                    if (collider.TryGetComponent(out HealthController healthController))
                    {
                        healthController.AddHealth(_healthValue);
                    }
                }).AddTo(this);
        }

    }
}
