using Units;
using UnityEngine;

namespace Player {
	public class PlayerController : MonoBehaviour {
		[SerializeField] private float _speed;
		[SerializeField] private float _maxSpeed;
		[SerializeField] private float _drag;
        [SerializeField] private float _rotationSpeed;
		
		[SerializeField] private Transform _unitTransform;
		[SerializeField] private Rigidbody _rigidbody;
		[SerializeField] private UnitAnimation _animation;
		
		public static Vector3 POSITION;
		private InputHandler _inputHandler;
		
		private void Awake() {
			_inputHandler = new InputHandler(Camera.main);
		}

		private void Update() {
			POSITION = transform.position;
		}

		private void FixedUpdate() {
            Rotate(_inputHandler.GetPointerPosition());
			Vector3 input = _inputHandler.GetInput();
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