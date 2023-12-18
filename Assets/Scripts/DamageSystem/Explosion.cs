using Root;
using UnityEngine;
using Utils;

namespace DamageSystem {
	public class Explosion : MonoBehaviour {
		[SerializeField] private ExplosionConfig _config;
		[SerializeField] private Damageable _self;

		public void Activate() {
			Collider[] targets = Physics.OverlapSphere(transform.position, _config.Radius, _config.LayerMask);

			foreach (Collider target in targets) {
				if(target.TryGetComponent(out Damageable damageable)) {
					Vector3 targetPosition = damageable.transform.position;
					Vector3 selfPosition = transform.position.With(y: targetPosition.y);
					Vector3 direction = targetPosition - selfPosition;
					Quaternion rotation = Quaternion.LookRotation(direction);

					if (damageable == _self) continue;
					damageable.ApplyDamage(_config.Damage, rotation);
				}
			}

			if (_config.CameraShake == 0) return;
			Core.LevelController.Camera.Shake(transform.position, _config.CameraShake);
		}
	}
}
