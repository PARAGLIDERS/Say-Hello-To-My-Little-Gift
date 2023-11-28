using Player;
using Root;
using UnityEngine;

namespace CameraControl {
	public class CameraController : MonoBehaviour {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _head;
        [SerializeField] private float _speed;
        [SerializeField] private float _motionSmooth = 1f;

        private Vector3 _velocity;

        public Vector3 Forward => Vector3.ProjectOnPlane(_head.forward, Vector3.up).normalized;
        public Vector3 Right => Vector3.ProjectOnPlane(_head.right, Vector3.up).normalized;

        public Ray ScreenPointToRay(Vector3 point) => _camera.ScreenPointToRay(point);

        public void Activate() {
            transform.position = Core.LevelController.PlayerController.Position;
            gameObject.SetActive(true);
        }

        public void Deactivate() {
            gameObject.SetActive(false);
        }

        private void LateUpdate() {
            Vector3 playerPos = Core.LevelController.PlayerController.Position;
            transform.position = Vector3.SmoothDamp(transform.position, playerPos, ref _velocity, _motionSmooth, _speed);
        }
    }
}