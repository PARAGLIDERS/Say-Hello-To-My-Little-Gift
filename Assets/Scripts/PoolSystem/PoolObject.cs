using System;
using UnityEngine;

namespace PoolSystem {
	public class PoolObject : MonoBehaviour {
		public void Init() {
			gameObject.SetActive(false);
		}

		public virtual void Activate(Vector3 position, Quaternion rotation) {
			transform.position = position;
			transform.rotation = rotation;
			gameObject.SetActive(true);
		}

		public virtual void Deactivate() {
			gameObject.SetActive(false);
		}
	}
}