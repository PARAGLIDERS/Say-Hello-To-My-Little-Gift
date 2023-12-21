using Root;
using UnityEngine;
using System;
using System.Collections;

namespace DamageSystem {
	public class Damageable : MonoBehaviour {
		[SerializeField] private DamageableConfig _config;
		[SerializeField] private GameObject _hitModel;

		public event Action OnDamage;
		public event Action OnHeal;
		public event Action OnDie;
		
		public int CurrentHealth { get; private set; }
		public int MaxHealth => _config.MaxHealth;

		private IEnumerator _death;

		public virtual void ApplyDamage(int amount, Quaternion damagerRotation) {
			if (amount < 0) {
				Debug.LogError("damage is below zero!");
				return;
			}

			if(CurrentHealth <= 0) return;
			
			CurrentHealth -= amount;
			OnDamage?.Invoke();
			if(_hitModel != null) _hitModel.SetActive(true);
			StartCoroutine(HideHitModel());

			if (CurrentHealth > 0) return;
			CurrentHealth = 0;

			_death = Die(damagerRotation);
			StartCoroutine(_death);
		}

		private IEnumerator HideHitModel() {
			yield return new WaitForSeconds(0.1f);
			if (_hitModel != null) _hitModel.SetActive(false);
		}

		private IEnumerator Die(Quaternion damagerRotation) {
			yield return new WaitForSeconds(_config.DeathDelay);

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

			if(_death != null) {
				StopCoroutine(_death);
				_death = null;
			}

			if (_hitModel != null) _hitModel.SetActive(false);
		}
	}
}