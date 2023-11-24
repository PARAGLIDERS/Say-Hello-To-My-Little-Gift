using System;
using UnityEngine;

namespace PoolSystem {
	public class PoolObject : MonoBehaviour {
        public bool IsActive {get; private set;}

        public event Action OnActivate;
		public event Action OnDeactivate;

		public void Init() {
			gameObject.SetActive(false);
		}

		public virtual void Activate(Vector3 position, Quaternion rotation) {
            IsActive = true;
			transform.SetPositionAndRotation(position, rotation);
            OnActivate?.Invoke();
            gameObject.SetActive(true);
		}

		public virtual void Deactivate() {
            IsActive = false;
			OnDeactivate?.Invoke();
			gameObject.SetActive(false);
		}
	}
}