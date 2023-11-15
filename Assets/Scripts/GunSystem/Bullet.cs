using DamageSystem;
using Misc.Root;
using PoolSystem;
using UnityEngine;

namespace GunSystem {
	[RequireComponent(typeof(Rigidbody))]
	public class Bullet : PoolObject {
		[SerializeField] private int _damage = 100;
		[SerializeField] private float _speedMin = 80f;
		[SerializeField] private float _speedMax = 120f;
		
		private const float _maxFlyTime = .3f;
		private float _timer;

		private Rigidbody _rigidbody;
		private TrailRenderer _trail;
		
		private void Awake() {
			_rigidbody = GetComponent<Rigidbody>();
			_trail = GetComponent<TrailRenderer>();
		}
		
		private void OnEnable() {
			_timer = _maxFlyTime;
			_rigidbody.velocity *= 0f;
			_rigidbody.angularVelocity *= 0f;
			_rigidbody.AddForce(Random.Range(_speedMin, _speedMax) * transform.forward, ForceMode.Impulse);
			_trail.Clear();
		}

		private void OnTriggerEnter(Collider other) {
			if (other.TryGetComponent(out Damageable damageable)) {
				damageable.ApplyDamage(_damage, transform.rotation);
				return;
			}

			if (other.TryGetComponent(out Rigidbody rb)) {
				rb.AddForce(transform.forward * _damage, ForceMode.Impulse);
			}
				
			Core.PoolController.Spawn(PoolType.BulletExplosion, transform.position, transform.rotation);
			Deactivate();
		}

		
		private void Update() {
			if (_timer > 0) {
				_timer -= Time.deltaTime;
				return;
			}
			
			Core.PoolController.Spawn(PoolType.BulletExplosion, transform.position, transform.rotation);
			Deactivate();
		}
	}
}