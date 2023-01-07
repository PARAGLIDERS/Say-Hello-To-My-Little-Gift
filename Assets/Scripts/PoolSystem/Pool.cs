using System.Collections.Generic;
using UnityEngine;

namespace PoolSystem {
	[CreateAssetMenu(menuName = "Pool")]
	public class Pool : ScriptableObject {
		public PoolType Type => _type;
		
		[SerializeField] private PoolType _type;
		[SerializeField] private PoolObject _poolObject;
		[SerializeField] private int _initSize = 25;

		private List<PoolObject> _objects = new ();
		private Transform _root;

		public void Init(Transform root) {
			_root = root;
			_objects.Clear();

			for (int i = 0; i < _initSize; i++) {
				CreateObject();
			}
		}

		public bool TryGetObject(out PoolObject poolObject) {
			poolObject = _objects.Count == 0 ? CreateObject() : _objects[^1];
			if (poolObject == null) return false;
			_objects.Remove(poolObject);
			return true;
		}
		
		private PoolObject CreateObject() {
			if (_poolObject == null) {
				Debug.LogError($"Pool {name} has nothing to spawn!");
				return null;
			}
			
			PoolObject poolObject = Instantiate(_poolObject, _root);
			poolObject.Init(OnObjectDeactivate);
			_objects.Add(poolObject);
			return poolObject;
		}

		private void OnObjectDeactivate(PoolObject poolObject) {
			_objects.Add(poolObject);
		}

		public void Dispose() {
			_objects.Clear();
			_root = null;
		}
	}
}