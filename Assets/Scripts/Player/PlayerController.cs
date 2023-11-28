using Root;
using Units;
using UnityEngine;

namespace Player {
	public class PlayerController : MonoBehaviour {
        [SerializeField] private Vector3 _defaultPosition; //ducktape

		[SerializeField] private float _speed;
		[SerializeField] private float _maxSpeed;
		[SerializeField] private float _drag;
        [SerializeField] private float _rotationSpeed;
		
		[SerializeField] private Transform _unitTransform;
		[SerializeField] private Rigidbody _rigidbody;
		[SerializeField] private UnitAnimation _animation;
        [SerializeField] private Transform _gunsHolder;

        public Transform GunsHolder => _gunsHolder;
        public Vector3 Position { get; private set; }
		
        public void Activate() {
            transform.position = _defaultPosition;
			Position = transform.position;
            gameObject.SetActive(true);
        }

        public void Deactivate() {
            gameObject.SetActive(false);
        }

		private void Update() {
			Position = transform.position;
		}

		private void FixedUpdate() {
            Rotate(Core.InputController.GetPointerPosition());
			Vector3 input = Core.InputController.GetPlayerInput();
			if(input == Vector3.zero) return;
			
			Move(input);
			_animation.Trigger();
		}
		
		private void Move(Vector3 direction) {
			_rigidbody.velocity += direction * (_speed);

			if (_rigidbody.velocity.magnitude > _maxSpeed) {
				_rigidbody.velocity = _rigidbody.velocity.normalized * _maxSpeed;
			}

			_rigidbody.velocity -= _rigidbody.velocity.normalized * _drag;
        }

        private void Rotate(Vector3 point) {
            Quaternion rotation = Quaternion.LookRotation(point - _unitTransform.position);
            _unitTransform.rotation = Quaternion.Lerp(_unitTransform.rotation, rotation, Time.deltaTime * _rotationSpeed);
        }
    }
}