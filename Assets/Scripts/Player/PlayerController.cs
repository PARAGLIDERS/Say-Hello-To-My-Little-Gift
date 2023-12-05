using Root;
using Units;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

namespace Player {
	public class PlayerController : MonoBehaviour {
        [SerializeField] private Vector3 _defaultPosition; 

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

        private UnitRotation _rotation;
        private UnitRotation _gunRotation;

        private void Awake() {
            _rotation = new UnitRotation(_unitTransform, _rotationSpeed);
            _gunRotation = new UnitRotation(_gunsHolder, _rotationSpeed);
        }

        private void FixedUpdate() {
            UpdateRotation();
            UpdatePosition();

            if(TryGetInput(out Vector3 input)) {
                Move(input);
                _animation.Trigger();
            }
        }

        public void Activate() {
            ResetPosition();
            gameObject.SetActive(true);
        }
        
        public void Deactivate() {
            gameObject.SetActive(false);
        }
		
        private bool TryGetInput(out Vector3 input) {
            input = Core.InputController.GetPlayerInput();
            return input != Vector3.zero;
        }

        private void Move(Vector3 direction) {
			_rigidbody.velocity += direction * (_speed);

			if (_rigidbody.velocity.magnitude > _maxSpeed) {
				_rigidbody.velocity = _rigidbody.velocity.normalized * _maxSpeed;
			}

			_rigidbody.velocity -= _rigidbody.velocity.normalized * _drag;
        }

        private void UpdateRotation() {
            Vector3 target = Core.InputController.GetPointerPosition();
            _rotation.Update(target);
            _gunRotation.Update(target.With(y: _gunsHolder.position.y));
        }

        private void UpdatePosition() {
            Position = transform.position;
        }

        private void ResetPosition() {
            transform.position = _defaultPosition;
            Position = transform.position;
        }
    }
}