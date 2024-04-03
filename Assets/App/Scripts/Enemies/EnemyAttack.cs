using System;
using UnityEngine;

namespace Enemies {
	public class EnemyAttack {
		public event Action OnAttack;

		private readonly float _cooldown;

		private float _currentCooldown;

		public EnemyAttack(float cooldown) {
			_cooldown = cooldown;
		}

		public void ResetCooldown() {
			_currentCooldown = Time.time + _cooldown;
		}

		public void Update() {
			if (!CanAttack()) return;

			OnAttack?.Invoke();
			ResetCooldown();
		}

		private bool CanAttack() {
			return Time.time > _currentCooldown;
		}
	}
}
