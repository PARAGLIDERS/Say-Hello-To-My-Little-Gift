using Player;
using UnityEngine;

namespace CameraControl {
	public class CameraController : MonoBehaviour {
		[SerializeField] private float _speed = 1f;
		
		private void LateUpdate() {
			transform.position = Vector3.Lerp(transform.position, PlayerController.POSITION, Time.deltaTime * _speed);
		}
	}
}