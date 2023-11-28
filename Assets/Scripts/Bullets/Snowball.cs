using PoolSystem;
using UnityEngine;

namespace Bullets {
    public class Snowball : BulletBase {
        [SerializeField] private float _force;
        [SerializeField] private float _gravity;

        protected override PoolType _explosionType => PoolType.SnowballExplosion;

        public override void Activate(Vector3 position, Quaternion rotation) {
            base.Activate(position, rotation);
            _rigidbody.AddForce(transform.forward * _force, ForceMode.Impulse);
        }

        private void FixedUpdate() {
            _rigidbody.velocity -= Vector3.up * _gravity;
        }
    }
}