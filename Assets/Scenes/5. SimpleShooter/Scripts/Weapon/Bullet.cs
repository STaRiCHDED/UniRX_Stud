/*
    Written By Olusola Olaoye

    To only be used by those who purchased from the Unity asset store

*/

using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Scenes._5._SimpleShooter.Scripts.Weapon
{
    [RequireComponent(typeof(ExplosiveObject), typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private ExplosiveObject _explosiveObject;
        [SerializeField] private Rigidbody _rigidbody;


        public void Initialize(float explosionDamage)
        {
            gameObject
                .OnCollisionEnterAsObservable()
                .Subscribe(_ => _explosiveObject.CreateExplode(explosionDamage))
                .AddTo(this);
        }

        public void AddForce(float force)
        {
            _rigidbody.AddForce(transform.forward * force, ForceMode.Acceleration);
        }
    }
}