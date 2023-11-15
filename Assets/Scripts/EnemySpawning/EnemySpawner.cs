using System.Collections;
using Enemies;
using Misc.Root;
using PoolSystem;
using UnityEngine;

namespace EnemySpawning {
	public class EnemySpawner : MonoBehaviour {
		[SerializeField] private EnemySpawnerGrid _grid;
		[SerializeField] private EnemySpawnerConfig _config;
		[SerializeField] private EnemyConfig[] _enemyModelConfigs;

		public void Init() {
			_grid.Init();
			//StartCoroutine(Spawning());
		}

		//private IEnumerator Spawning() {
			//while (true) {
				//for (int i = 0; i < _count; i++) {
				//	Core.PoolController.Spawn<Enemy>(PoolType.Enemy, _grid.GetPosition(), Quaternion.identity);
				//}
				//yield return new WaitForSeconds(period);
			//}
		//}	
	}
}