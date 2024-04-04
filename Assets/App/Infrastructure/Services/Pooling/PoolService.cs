using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pooling {
    public class PoolService : IPoolService {
        private readonly PoolFactory _factory;
        private Dictionary<PoolType, Queue<PoolObject>> _pools;

        public PoolService(PoolFactory factory) {
            _factory = factory;
		} 

        public void Initialize() {            
            _pools = _factory.CreatePools();
		}

		public PoolObject Spawn(PoolType type, Vector3 position, Quaternion rotation, Action onActivate = null, Action onDeactivate = null) {
            if(!TryGetObject(type, out PoolObject poolObject)) {
                Debug.LogError($"pool object is null: {type}");
                return null;
            }

            poolObject.OnActivate += onActivate;
            poolObject.OnDeactivate += onDeactivate;

            poolObject.Activate(position, rotation);
            return poolObject;
        }

        private bool TryGetObject(PoolType type, out PoolObject poolObject) {
			poolObject = null;

			if (!_pools.TryGetValue(type, out Queue<PoolObject> pool)) {
				Debug.LogError($"no such pool in dictionary: {type}");
				return false;
			}

			poolObject = pool.Peek().IsActive ? _factory.Create(type) : pool.Dequeue();
			pool.Enqueue(poolObject);

            return poolObject == null;
		}

        public void DeactivateAll() {
            foreach (Queue<PoolObject> pool in _pools.Values) {
                foreach (PoolObject poolObject in pool) {
                    poolObject.Deactivate();
                }
            }
        }
    }
}