using DamageSystem;
using Dash;
using Root;
using SfxSystem;
using System;
using Units;
using UnityEngine;
using Utils;

namespace Player {
	public class PlayerController : MonoBehaviour {
        [SerializeField] private Damageable _damageable;
        [SerializeField] private PlayerDashConfig _dashConfig;
        [SerializeField] private ParticleSystem _dashParticles;
        [SerializeField] private SfxType[] _damageSounds;

        [SerializeField] private Vector3 _defaultPosition; 

		[SerializeField] private float _speed;
		[SerializeField] private float _maxSpeed;
		[SerializeField] private float _drag;
        [SerializeField] private float _rotationSpeed;
		
		[SerializeField] private Transform _unitTransform;
		[SerializeField] private Rigidbody _rigidbody;
		[SerializeField] private UnitAnimation _animation;
        [SerializeField] private Transform _gunsHolder;

        public event Action DashChanged {
			add => _dash.Changed += value;
			remove => _dash.Changed -= value;            
		}

        public int DashCount => _dash.Count;

        public event Action OnDamage {
			add => _damageable.OnDamage += value;
			remove => _damageable.OnDamage -= value;            
		}

        public event Action OnHeal {
			add => _damageable.OnHeal += value;
			remove => _damageable.OnHeal -= value;
		}

		public event Action OnDie {
			add => _damageable.OnDie += value;
			remove => _damageable.OnDie -= value;
		}

        public int MaxHealth => _damageable.MaxHealth;
        public int CurrentHealth => _damageable.CurrentHealth;

        public Transform GunsHolder => _gunsHolder;
        public Vector3 Position { get; private set; }

        private UnitRotation _rotation;
        private UnitRotation _gunRotation;
        private PlayerDash _dash;

        private void Awake() {
            _rotation = new UnitRotation(_unitTransform, _rotationSpeed);
            _gunRotation = new UnitRotation(_gunsHolder, _rotationSpeed);
            _dash = new PlayerDash(_dashConfig, _rigidbody, _dashParticles);
        }

		private void Update() {
			_dash.Update();
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
            _damageable.ResetHealth();
            _damageable.OnDie += HandleDie;
            _damageable.OnDamage += HandleDamage;
            _dash.Reset();
            gameObject.SetActive(true);
		}

		public void Deactivate() {
            _damageable.OnDie -= HandleDie;
			_damageable.OnDamage -= HandleDamage;
			gameObject.SetActive(false);
        }

        private void HandleDie() {
            Deactivate();
        }
		
        private void HandleDamage() {
            Core.LevelController.Camera.Shake(transform.position, 0.7f);
            Core.SfxController.Play(_damageSounds[UnityEngine.Random.Range(0, _damageSounds.Length)]);
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
            _rotation.Update(target.With(y: _unitTransform.position.y));
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