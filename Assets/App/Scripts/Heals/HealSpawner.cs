using Pooling;
using Root;
using Spawner;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace Heals {
	public class HealSpawner : SpawnerBase{
		private readonly HealSpawnerConfig _config;

		public HealSpawner(HealSpawnerConfig config) {
			_config = config;
		}

		protected override IEnumerator Execute() {
			while (true) {
				yield return new WaitForSeconds(_config.Cooldown);
				Vector3 position = _grid.GetPosition();
				Core.PoolController.Spawn(PoolType.HealVfx, position, Quaternion.identity);
				Core.PoolController.Spawn(PoolType.Heal, position, Quaternion.identity);
			}
		}
	}
}
