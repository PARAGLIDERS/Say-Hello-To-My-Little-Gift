using Root;
using UnityEngine;

namespace Enemies {
	public class EnemyRabbitClone : EnemyRabbitBase {
		public override void Activate(Vector3 position, Quaternion rotation) {
			base.Activate(position, rotation);
			_damageable.OnDie += Despawn;
		}

		public override void Deactivate() {
			base.Deactivate();
			_damageable.OnDie -= Despawn;
		}

		public void Despawn() {
			Core.PoolController.Spawn(PoolSystem.PoolType.VFX_EnemySpawnEffect, transform.position, transform.rotation);
		}
	}
}
