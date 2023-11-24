using Misc.Root;
using PoolSystem;
using UnityEngine;

namespace DamageSystem {
	public class Damageable : MonoBehaviour {
		[SerializeField] private int _maxHealth = 100;
		
		private int _currentHealth;
		private PoolObject _poolObject;

		private void Awake() {
			_poolObject = GetComponent<PoolObject>();
		}

		private void OnEnable() {
			_currentHealth = _maxHealth;
		}

		public void ApplyDamage(int amount, Quaternion damagerRotation) {
			if(_currentHealth <= 0) return;
			
			_currentHealth -= amount;

			if (_currentHealth <= 0) {
				_currentHealth = 0;
				_poolObject.Deactivate();
				Core.PoolController.Spawn(PoolType.FloorBlood, transform.position, Quaternion.identity);
				Core.PoolController.Spawn(PoolType.BloodParticles, transform.position, damagerRotation);
			}
		}
	}
}