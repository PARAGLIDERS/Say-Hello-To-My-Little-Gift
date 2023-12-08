using Root;
using PoolSystem;
using UnityEngine;
using System;

namespace DamageSystem {
	public class Damageable : MonoBehaviour {
		[SerializeField] private int _maxHealth = 100;
		[SerializeField] private PoolType _dieParticles = PoolType.BloodParticles;
		[SerializeField] private PoolType _dieFloorDecals = PoolType.FloorBlood;

		public event Action OnDamage;
		public event Action OnHeal;
		public event Action OnDie;
		
		public int CurrentHealth { get; private set; }
		public int MaxHealth => _maxHealth;

		public void ApplyDamage(int amount, Quaternion damagerRotation) {
			if (amount < 0) {
				Debug.LogError("damage is below zero!");
				return;
			}

			if(CurrentHealth <= 0) return;
			
			CurrentHealth -= amount;
			OnDamage?.Invoke();

			if (CurrentHealth > 0) return;
			CurrentHealth = 0;
			
			Core.PoolController.Spawn(_dieFloorDecals, transform.position, Quaternion.identity);
			Core.PoolController.Spawn(_dieParticles, transform.position, damagerRotation);

			OnDie?.Invoke();
		}

		public void Heal(int amount) {
			if (amount <= 0) { 
				Debug.LogError("heal is below zero!");
				return; 
			}

			if(CurrentHealth <= 0) return;

			CurrentHealth += amount;

			if(CurrentHealth > _maxHealth) {
				CurrentHealth = _maxHealth;
			}

			OnHeal?.Invoke();
		}

		public void ResetHealth() {
			CurrentHealth = _maxHealth;
		}
	}
}