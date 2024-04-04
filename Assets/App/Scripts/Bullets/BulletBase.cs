using DamageSystem;
using Root;
using Pooling;
using UnityEngine;

namespace Bullets {
    public abstract class BulletBase : PoolObject {
        [SerializeField] protected Rigidbody _rigidbody;
        
        private BulletBaseConfig _baseConfig;
        private float _timer;

        protected void Init(BulletBaseConfig config) {
            _baseConfig = config;
        }

        public override void Activate(Vector3 position, Quaternion rotation) {
            base.Activate(position, rotation);

            ResetRigidbody();
            ResetTimer();
        }

        private void ResetRigidbody() {
            _rigidbody.velocity *= 0;
            _rigidbody.angularVelocity *= 0;
        }

        private void ResetTimer() {
            _timer = Time.time + _baseConfig.Lifetime;
        }

        protected virtual void OnTriggerEnter(Collider other) {
            if (other.TryGetComponent(out Damageable damageable)) {
                damageable.ApplyDamage(_baseConfig.Damage, transform.rotation);
            }
        }

        private void Update() {
            if (Time.time < _timer) return;
            Despawn();
        }

        protected virtual void Despawn() {
            Core.PoolController.Spawn((PoolType)_baseConfig.ExplosionType, transform.position, transform.rotation);
            Deactivate();
        }
    }
}
