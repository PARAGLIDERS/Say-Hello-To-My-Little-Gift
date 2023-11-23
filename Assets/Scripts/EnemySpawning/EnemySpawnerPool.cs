using System.Collections.Generic;
using Enemies;
using Misc.Root;
using UnityEngine;

namespace EnemySpawning {
	public class EnemySpawnerPool {
		private readonly EnemiesConfig _config;
		private readonly Dictionary<EnemyType, Stack<Enemy>> _pools;
		private readonly Transform _container;
		
		public EnemySpawnerPool(EnemiesConfig config) {
			_config = config;
			_container = new GameObject("enemies").transform;
			_container.SetParent(Core.Container);
			
			_pools = new Dictionary<EnemyType, Stack<Enemy>>();

			foreach (EnemyConfig c in config.EnemyConfigs) {
				if(_pools.ContainsKey(c.Type)) continue;
				
				Stack<Enemy> pool = new ();
				for (int i = 0; i < 100; i++) {
					pool.Push(Create(c.Type));
				}
				
				_pools.Add(c.Type, pool);
			}
		}
		
		public void Dispose() {
			foreach (KeyValuePair<EnemyType,Stack<Enemy>> keyValuePair in _pools) {
				foreach (Enemy enemy in keyValuePair.Value) {
					Object.Destroy(enemy);
				}
				
				keyValuePair.Value.Clear();
			}
			
			_pools.Clear();
		}

		public Enemy Get(EnemyType type) {
			if (!_pools.TryGetValue(type, out Stack<Enemy> pool)) {
				Debug.LogError($"no enemy pool of type: {type}");
				return null;
			}

			return pool.Count > 0 ? pool.Pop() : Create(type);
		}

		private Enemy Create(EnemyType type) {
			Enemy prefab = _config.Get(type);

			if (prefab == null) {
				Debug.LogError($"prefab of type {type} is null in config");
				return null;
			}
			
			Enemy instance = Object.Instantiate(prefab, _container);
			instance.OnKill += () => { Return(type, instance); };
			return instance;
		}

		private void Return(EnemyType type, Enemy enemy) {
			if (!_pools.TryGetValue(type, out Stack<Enemy> pool)) {
				Debug.LogError($"no enemy pool of type: {type}");
				return;
			}

			pool.Push(enemy);
		}
	}
}