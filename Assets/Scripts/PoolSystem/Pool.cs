using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PoolSystem {
	[Serializable]
	public class Pool {
		public PoolType Type => _type;
		[HideInInspector] public string Name;
		
		[SerializeField] private PoolType _type;
		[SerializeField] private PoolObject _poolObject;
		[SerializeField] private int _initSize = 1000;

		private Queue<PoolObject> _objects = new ();

		private Transform _root;
		
		public void Init(Transform root) {
			_root = root;
			_objects.Clear();

			for (int i = 0; i < _initSize; i++) {
				CreateObject();
			}
		}

		public bool TryGetObject(out PoolObject poolObject) {
			poolObject = _objects.Dequeue();
			_objects.Enqueue(poolObject);
			
			return poolObject != null;
		}
		
		private PoolObject CreateObject() {
			if (_poolObject == null) {
				Debug.LogError($"pool object from pool of type {_type} is null");
				return null;
			}
			
			PoolObject poolObject = Object.Instantiate(_poolObject, _root);
			poolObject.Init();
			_objects.Enqueue(poolObject);
			return poolObject;
		}

		public void DeactivateAll() {
			foreach (PoolObject poolObject in _objects) {
				poolObject.Deactivate();
			}
		}
		
		public void Dispose() {
			_objects.Clear();
			_root = null;
		}
	}
}