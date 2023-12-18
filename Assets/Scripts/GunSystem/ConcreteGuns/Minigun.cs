using UnityEngine;

namespace GunSystem.ConcreteGuns {
	public class Minigun : Gun {
		[SerializeField] private Transform _barrel;
		[SerializeField] private Vector3 _orientation;
		[SerializeField] private float _speed;
		[SerializeField] private float _deceleration;

		private float _currentSpeed;

		protected override void OnShoot() {
			base.OnShoot();
			_currentSpeed = _speed;
		}

		private void Update() {
			if(_currentSpeed > 0) { 
				_currentSpeed -= Time.deltaTime * _deceleration;
			}

			if(_currentSpeed < 0) _currentSpeed = 0;
			_barrel.Rotate(_orientation, Time.deltaTime * _currentSpeed);
		}
	}
}
