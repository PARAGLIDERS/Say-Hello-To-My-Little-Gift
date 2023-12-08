using DamageSystem;
using Root;
using PoolSystem;
using UnityEngine;

namespace Bullets {
    public abstract class BulletBase : PoolObject {
        [SerializeField] protected int _damage;
        [SerializeField] protected float _lifeTime;
        [SerializeField] protected Rigidbody _rigidbody;
        
        protected abstract PoolType _explosionType { get; }
        private float _timer;

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
            _timer = Time.time + _lifeTime;
        }

        public override void Deactivate() {
            base.Deactivate();
            Core.PoolController.Spawn(_explosionType, transform.position, transform.rotation);
        }

        private void OnTriggerEnter(Collider other) {
            if (other.TryGetComponent(out Damageable damageable)) {
                damageable.ApplyDamage(_damage, transform.rotation);
            }

            Deactivate();
        }

        private void Update() {
            if (Time.time < _timer) return;
            Deactivate();
        }
    }
}
