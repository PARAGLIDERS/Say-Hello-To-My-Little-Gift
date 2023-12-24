using System;
using UnityEngine;

namespace PoolSystem {
	public abstract class PoolObject : MonoBehaviour {
        public bool IsActive {get; private set;}

        public event Action OnActivate;
		public event Action OnDeactivate;

		public virtual void Hide() {
			IsActive = false;
			gameObject.SetActive(false);
            OnActivate = null;
			OnDeactivate = null;
		}

		public virtual void Activate(Vector3 position, Quaternion rotation) {
            IsActive = true;
			transform.SetPositionAndRotation(position, rotation);
            OnActivate?.Invoke();
            OnActivate = null;
            gameObject.SetActive(true);
		}

		public virtual void Deactivate() {
            IsActive = false;
			OnDeactivate?.Invoke();
            OnDeactivate = null;
			gameObject.SetActive(false);
		}
	}
}