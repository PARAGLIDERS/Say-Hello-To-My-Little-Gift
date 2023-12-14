using UnityEngine;

namespace Bullets {
    public class Snowball : BulletBase {
        [SerializeField] private ProjectileConfig _config; 

		private void Awake() {
			Init(_config);
		}

		public override void Activate(Vector3 position, Quaternion rotation) {
            base.Activate(position, rotation);
            _rigidbody.AddForce(transform.forward * (_config as ProjectileConfig).Force, ForceMode.Impulse);
        }

        private void FixedUpdate() {
            _rigidbody.velocity -= Vector3.up * (_config as ProjectileConfig).Gravity;
        }
    }
}