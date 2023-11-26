using System;
using System.Collections.Generic;
using UnityEngine;

namespace PoolSystem {
    public class PoolController {
        private readonly Dictionary<PoolType, Pool> _pools = new();
        private readonly Transform _container;

        public PoolController(Transform parent, PoolConfig config) {
            _container = new GameObject("Pools").transform; 
            _container.SetParent(parent);

            foreach (PoolConfigItem item in config.Items) {
                if (_pools.ContainsKey(item.Type)) {
                    Debug.LogError($"{item.Type} already exists in pools dictionary");
                    continue;
                }

                if(item.Prefab == null) {
                    Debug.LogError($"pool object prefab is null: {item.Type}");
                    continue;
                }

                Pool pool = new Pool(_container, item.Type, item.Prefab, item.Size);
                _pools.Add(item.Type, pool);
            }
        }

        public void Spawn(PoolType type, Vector3 position, Quaternion rotation, Action onActivate = null, Action onDeactivate = null) {
            if(!_pools.TryGetValue(type, out Pool pool)) {
                Debug.LogError($"no such pool in dictionary: {type}");
                return;
            }

            PoolObject poolObject = pool.Get();
            if(poolObject == null ) {
                Debug.LogError($"pool object is null: {type}");
                return;
            }

            poolObject.OnActivate += onActivate;
            poolObject.OnDeactivate += onDeactivate;

            poolObject.Activate(position, rotation);
        }

        public void DeactivateAll() {
            foreach (KeyValuePair<PoolType, Pool> pool in _pools) {
                pool.Value.DeactivateAll();
            }
        }

        public void Dispose() {
            if (_container) GameObject.Destroy(_container.gameObject);

            foreach (var pool in _pools) {
                pool.Value.Dispose();
            }
        }
    }
}