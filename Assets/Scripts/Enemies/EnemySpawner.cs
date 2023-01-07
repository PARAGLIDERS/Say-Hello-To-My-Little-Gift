using System.Collections;
using PoolSystem;
using UnityEngine;

namespace Enemies {
	public class EnemySpawner : MonoBehaviour {
		[SerializeField] private SpawnerGrid _grid;
		[SerializeField] private int _count = 3;
		[SerializeField] private float period = 1f;
		
		private void Awake() {
			_grid.Init();
			StartCoroutine(Spawning());
		}

		private IEnumerator Spawning() {
			while (true) {
				for (int i = 0; i < _count; i++) {
					PoolController.Spawn(PoolType.Enemy, _grid.GetPosition(), Quaternion.identity);
				}
				yield return new WaitForSeconds(period);
			}
		}	
	}
}