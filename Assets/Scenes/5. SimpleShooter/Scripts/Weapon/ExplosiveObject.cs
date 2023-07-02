using Scenes._5._SimpleShooter.Scripts.Player;
using UnityEngine;

namespace Scenes._5._SimpleShooter.Scripts.Weapon
{
    public class ExplosiveObject : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _explosionParticle;
        [SerializeField] private int _explosionRangeRadius = 10;
        [SerializeField] private int _explosionForce = 20;

        public void CreateExplode(float explosionDamage)
        {
            Instantiate(_explosionParticle, transform.position, Quaternion.identity);

            Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRangeRadius);
            
            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out ExplodingBox explodingBox))
                {
                    explodingBox.Explode(_explosionForce, _explosionRangeRadius);
                }

                if (collider.TryGetComponent(out HealthController healthController))
                {
                    healthController.TakeDamage(explosionDamage);
                }
            }

            Destroy(gameObject);
        }
    }
}