using Root;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies {
	public abstract class EnemyRabbitBase : Enemy {
		[SerializeField] private Transform _muzzle;
		[SerializeField] private List<int> _stages;

		protected override void Attack() {
			Core.PoolController.Spawn(PoolSystem.PoolType.EnemyProjectile_Snowball, _muzzle.position, _muzzle.rotation);
			Core.SfxController.Play(SfxSystem.SfxType.EnemySnowmanThrow, _muzzle.position);
		}
	}
}
