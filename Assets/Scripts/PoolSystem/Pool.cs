using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PoolSystem {
	public class Pool {
		private readonly PoolType _type;
		private readonly PoolObject _prefab;
		private readonly Queue<PoolObject> _objects;
		private Transform _container;
		
        public Pool(Transform parent, PoolType type, PoolObject prefab, int size) {
            _type = type;
            _prefab = prefab;

            _container = new GameObject(type.ToString()).transform;
            _container.SetParent(parent);

            _objects = new Queue<PoolObject>();
            for (int i = 0; i < size; i++) {
                _objects.Enqueue(Create());
            }
        }

		public PoolObject Get() {
			PoolObject poolObject = _objects.Peek().IsActive ? Create() : _objects.Dequeue();
			_objects.Enqueue(poolObject);			
			return poolObject;
		}
		
		private PoolObject Create() {
			if (_prefab == null) {
				Debug.LogError($"pool object from pool of type {_type} is null");
				return null;
			}
			
			PoolObject poolObject = Object.Instantiate(_prefab, _container);
			poolObject.Init();
			return poolObject;
		}

		public void DeactivateAll() {
			foreach (PoolObject poolObject in _objects) {
				poolObject.Deactivate();
			}
		}
		
		public void Dispose() {
            Object.Destroy(_container.gameObject);
            _objects.Clear();
		}
	}
}