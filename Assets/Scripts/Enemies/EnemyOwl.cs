using DamageSystem;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies {
	public class EnemyOwl : Enemy {
		[SerializeField] private float _initScale;
		[SerializeField] private ParticleSystem _attackEffect;
		[SerializeField] private Explosion _explosion;
		[SerializeField] private Transform _bodyContainer;
		[SerializeField] private EnemyStage _stage;

		public override void Activate(Vector3 position, Quaternion rotation) {
			base.Activate(position, rotation);
			_damageable.OnDamage += HandleDamage;
			SetScale(_initScale);
			_stage.Reset();
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
			float percentage = (float) _damageable.CurrentHealth / _damageable.MaxHealth;
			if (_stage.IsChanged(percentage)) {
				SetScale(percentage * _initScale);
				Attack();
			}
		}

		private void SetScale(float scale) {
			Vector3 targetScale = new Vector3(_initScale, scale, _initScale);
			_bodyContainer.DOComplete();
			_bodyContainer.DOScale(targetScale, 0.25f).SetEase(Ease.OutBack);
		}
	}
}
