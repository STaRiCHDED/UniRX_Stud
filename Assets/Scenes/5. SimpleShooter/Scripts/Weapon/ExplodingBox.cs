using System;
using UniRx;
using UnityEngine;

namespace Scenes._5._SimpleShooter.Scripts.Weapon
{
    [RequireComponent(typeof(Rigidbody))]
    public class ExplodingBox : MonoBehaviour
    {
        public IObservable<Unit> OnExploded => _explodedSubject;

        private readonly Subject<Unit> _explodedSubject = new Subject<Unit>();
        private Rigidbody _rigidbody;


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Explode(int explosionForce, int explosionRangeRadius)
        {
            _rigidbody.AddExplosionForce(explosionForce, transform.position, explosionRangeRadius);
            _explodedSubject.OnNext(Unit.Default);
        }
    }
}