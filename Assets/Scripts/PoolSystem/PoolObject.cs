using System;
using UnityEngine;

namespace PoolSystem {
	public class PoolObject : MonoBehaviour {
		private Action<PoolObject> _onDispose;

		public void Init(Action<PoolObject> onDispose) {
			gameObject.SetActive(false);
			_onDispose = onDispose;
		}

		public void Activate(Vector3 position, Quaternion rotation) {
			transform.position = position;
			transform.rotation = rotation;
			gameObject.SetActive(true);
		}

		public void Deactivate() {
			gameObject.SetActive(false);
			_onDispose?.Invoke(this);
		}
	}
}