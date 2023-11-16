using System.Collections.Generic;
using Misc.Root;
using UnityEngine;

namespace PoolSystem {
	public class PoolController {
		private readonly Dictionary<PoolType, Pool> _pools = new ();
		private readonly Transform _container;
		
		public PoolController() {
			GameObject containerGo = new ("Pools");
			Object.DontDestroyOnLoad(containerGo);
			_container = containerGo.transform;
			
			foreach (Pool pool in Core.Resources.Pools) {
				_pools.Add(pool.Type, pool);
				GameObject poolRoot = new (pool.Type.ToString());
				poolRoot.transform.SetParent(_container);
				pool.Init(poolRoot.transform);
			}
		}
			
		public void Spawn(PoolType type, Vector3 position, Quaternion rotation) {
			if (!_pools.TryGetValue(type, out Pool pool)) {
				Debug.LogError($"Pool of type {type} does not exist!");
				return;
			}

			if (!pool.TryGetObject(out PoolObject poolObject)) {
				Debug.LogError($"Pool of type {type} returned null");
				return;
			}
			
			poolObject.Activate(position, rotation);
		}

		public T Spawn<T>(PoolType type) where T : PoolObject {
			if (!_pools.TryGetValue(type, out Pool pool)) {
				Debug.LogError($"Pool of type {type} does not exist!");
				return null;
			}
			
			if (!pool.TryGetObject(out PoolObject poolObject)) {
				Debug.LogError($"Pool of type {type} returned null");
				return null;
			}

			return (T)poolObject;
		}
		
		public void DeactivateAll() {
			foreach (KeyValuePair<PoolType,Pool> pool in _pools) {
				pool.Value.DeactivateAll();
			}
		}
		
		public void Dispose() {
			if(_container) Object.Destroy(_container.gameObject);

			foreach (var pool in _pools) {
				pool.Value.Dispose();
			}
		}
	}
}