using UnityEngine;

namespace Bullets {
	public class Bullet : BulletBase {
        [SerializeField] private BulletConfig _config;
		[SerializeField] private TrailRenderer _trail;

		private void Awake() {
            Init(_config);
		}

		public override void Activate(Vector3 position, Quaternion rotation) {
            if(_trail != null) _trail.Clear();
            base.Activate(position, rotation);            
            _rigidbody.AddForce(Random.Range(_config.SpeedMin, _config.SpeedMax) * transform.forward, ForceMode.Impulse);
        }

		protected override void OnTriggerEnter(Collider other) {
			base.OnTriggerEnter(other);
			Despawn();
		}
	}
}