using Root;
using UnityEngine;
using System;

namespace DamageSystem {
	public class Damageable : MonoBehaviour {
		[SerializeField] private DamageableConfig _config;

		public event Action OnDamage;
		public event Action OnHeal;
		public event Action OnDie;
		
		public int CurrentHealth { get; private set; }
		public int MaxHealth => _config.MaxHealth;

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

			Core.PoolController.Spawn(_config.DieFloorDecals, transform.position, Quaternion.identity);
			Core.PoolController.Spawn(_config.DieParticles, transform.position, damagerRotation);
			Core.SfxController.Play(_config.DieSfx, transform.position);

			OnDie?.Invoke();
		}

		public void Heal(int amount) {
			if (amount <= 0) { 
				Debug.LogError("heal is below zero!");
				return; 
			}

			if(CurrentHealth <= 0) return;

			CurrentHealth += amount;

			if(CurrentHealth > _config.MaxHealth) {
				CurrentHealth = _config.MaxHealth;
			}

			OnHeal?.Invoke();
		}

		public void ResetHealth() {
			CurrentHealth = _config.MaxHealth;
		}
	}
}