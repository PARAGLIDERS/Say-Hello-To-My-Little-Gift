using System;
using System.Collections.Generic;
using UnityEngine;

namespace PoolSystem {
	public class PoolController : MonoBehaviour {
		[SerializeField] private List<Pool> _pools = new ();
		private readonly Dictionary<PoolType, Pool> _poolDictionary = new ();

		public static void Spawn(PoolType type, Vector3 position, Quaternion rotation) => SPAWN?.Invoke(type, position, rotation);	

		private static event Action<PoolType, Vector3, Quaternion> SPAWN; 

		private void OnEnable() => SPAWN += Spawn_Internal;
		private void OnDisable() => SPAWN -= Spawn_Internal;

		private void Awake() {
			foreach (Pool pool in _pools) {
				_poolDictionary.Add(pool.Type, pool);
				GameObject poolRoot = new (pool.name);
				poolRoot.transform.SetParent(transform);
				pool.Init(poolRoot.transform);
			}
		}

		private void OnDestroy() {
			foreach (Pool pool in _pools) {
				pool.Dispose();
			}
		}

		private void Spawn_Internal(PoolType type, Vector3 position, Quaternion rotation) {
			if (!_poolDictionary.TryGetValue(type, out Pool pool)) {
				Debug.LogError($"Pool of type {type} does not exist!");
				return;
			}

			if (!pool.TryGetObject(out PoolObject poolObject)) {
				Debug.LogError($"Pool {pool.name} of type {type} returned null");
				return;
			}
			
			poolObject.Activate(position, rotation);
		}
	}
}