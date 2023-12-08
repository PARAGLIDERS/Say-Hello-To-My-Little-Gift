using UnityEngine;

namespace Animations {
	public class RotationAnimation : MonoBehaviour {
		[SerializeField] private float _speed = 30f;
		[SerializeField] private Vector3 _orientation = Vector3.up;

		private void Update() {
			transform.Rotate(_orientation, Time.deltaTime * _speed);
		}
	}
}