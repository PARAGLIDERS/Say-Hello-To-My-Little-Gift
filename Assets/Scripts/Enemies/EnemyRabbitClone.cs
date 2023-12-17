using Root;

namespace Enemies {
	public class EnemyRabbitClone : EnemyRabbitBase {
		public override void Deactivate() {
			base.Deactivate();
			Core.PoolController.Spawn(PoolSystem.PoolType.VFX_EnemySpawnEffect, transform.position, transform.rotation);
		}
	}
}
