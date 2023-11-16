using Units;
using Units.UnitConfigs;
using UnityEngine;

namespace Player {
	public class PlayerController : MonoBehaviour {
		[SerializeField] private float _speed;
		[SerializeField] private float _maxSpeed;
		[SerializeField] private float _drag;

		[SerializeField] private UnitAnimationConfig _animationConfig;
		[SerializeField] private UnitRotationConfig _rotationConfig;
		
		[SerializeField] private Transform _unitTransform;
		[SerializeField] private Transform _bodyTransform;
		[SerializeField] private Rigidbody _rigidbody;
		
		public static Vector3 POSITION;
		private InputHandler _inputHandler;
		private UnitRotation _rotation;
		private UnitAnimation _animation;
		
		private void Awake() {
			_animation = new UnitAnimation(_animationConfig, _bodyTransform);
			_inputHandler = new InputHandler(Camera.main);
			_rotation = new UnitRotation(_rotationConfig, _unitTransform);
		}

		protected void Update() {
			POSITION = transform.position;
		}

		private void FixedUpdate() {
			_rotation.Rotate(_inputHandler.GetPointerPosition());

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
	}
}