using UnityEngine;

namespace Animations {
	public class RotationAnimation : MonoBehaviour{
		private void Update() {
			transform.Rotate(Vector3.forward, Time.deltaTime * 30f);
		}
	}
}