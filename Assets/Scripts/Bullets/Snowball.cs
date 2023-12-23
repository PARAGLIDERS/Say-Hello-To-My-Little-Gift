using Root;
using UnityEngine;

namespace Bullets {
    public class Snowball : BulletBase {
        [SerializeField] private ProjectileConfig _config; 

		private void Awake() {
			Init(_config);
		}

		public override void Activate(Vector3 position, Quaternion rotation) {
            base.Activate(position, rotation);
            _rigidbody.AddForce(transform.forward * _config.Force, ForceMode.Impulse);
        }

		protected override void Despawn() {
			base.Despawn();
			Core.SfxController.Play(SfxSystem.SfxType.EnemySnowballHit);
		}


		private void FixedUpdate() {
            _rigidbody.velocity -= Vector3.up * _config.Gravity;
        }
	}
}