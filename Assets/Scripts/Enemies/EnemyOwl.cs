using DamageSystem;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies {
	public class EnemyOwl : Enemy {
		[SerializeField] private float _initScale;
		[SerializeField] private List<int> _stages;
		[SerializeField] private ParticleSystem _attackEffect;
		[SerializeField] private Explosion _explosion;

		private int _currentParamIndex;

		public override void Activate(Vector3 position, Quaternion rotation) {
			base.Activate(position, rotation);
			_damageable.OnDamage += HandleDamage;
			SetScale(_initScale);
			_currentParamIndex = 0;
		}

		public override void Deactivate() {
			base.Deactivate();
			_damageable.OnDamage -= HandleDamage;
		}

		protected override void Attack() {
			_attackEffect.Play();
			_explosion.Activate();
		}

		private void HandleDamage() {
			int index = _currentParamIndex;
			float percentage = (float) _damageable.CurrentHealth / _damageable.MaxHealth;
			
			for (int i = _currentParamIndex; i < _stages.Count; i++) {
				index = i;

				if (_stages[i] < percentage * 100) {
					break;
				}
			}

			if (index == _currentParamIndex) {
				return;
			}

			_currentParamIndex = index;
			SetScale(percentage * _initScale);
			Attack();
		}

		private void SetScale(float scale) {
			transform.DOComplete();
			transform.DOScale(scale, 0.1f).SetEase(Ease.OutBack);
		}
	}
}
