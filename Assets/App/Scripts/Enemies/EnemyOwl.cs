using DamageSystem;
using DG.Tweening;
using Root;
using System.Collections;
using UnityEngine;

namespace Enemies {
	public class EnemyOwl : Enemy {
		[SerializeField] private float _initScale;
		[SerializeField] private ParticleSystem _attackEffect;
		[SerializeField] private Explosion _explosion;
		[SerializeField] private Transform _bodyContainer;
		[SerializeField] private EnemyStage _stage;
		[SerializeField] private Rigidbody _rigidbody;
		[SerializeField] private float _dashCooldown;
		[SerializeField] private float _dashForce;
		[SerializeField] private ParticleSystem _dashParticles;

		private float _initParticlesScale;

		public override void Activate(Vector3 position, Quaternion rotation) {
			base.Activate(position, rotation);
			_initParticlesScale = _dashParticles.main.startSizeYMultiplier;
			_damageable.OnDamage += HandleDamage;
			SetScale(1f);
			_stage.Reset();
			_rigidbody.isKinematic = true;
			_rigidbody.velocity *= 0;

		}

		public override void Deactivate() {
			base.Deactivate();
			_damageable.OnDamage -= HandleDamage;
		}

		protected override void Attack() {
			_agent.enabled = false;
			_rigidbody.isKinematic = false;
			_rigidbody.AddForce(transform.forward * _dashForce, ForceMode.Impulse);
			_dashParticles.Play();
			Core.SfxController.Play(SfxSystem.SfxType.Dash);
			StartCoroutine(DashCooldown());
		}

		private IEnumerator DashCooldown() {
			yield return new WaitForSeconds(_dashCooldown);
			_agent.enabled = true;
			_rigidbody.isKinematic = true;
			_rigidbody.velocity *= 0;
			Explode();
		}

		private void Explode() {
			_attackEffect.Play();
			_explosion.Activate();
		}

		private void HandleDamage() {
			float percentage = (float) _damageable.CurrentHealth / _damageable.MaxHealth;
			if (_stage.IsChanged(percentage)) {
				SetScale(percentage);
				Explode();
			}
		}

		private void SetScale(float percentage) {
			Vector3 targetScale = new Vector3(_initScale, percentage * _initScale, _initScale);
			_bodyContainer.DOComplete();
			_bodyContainer.DOScale(targetScale, 0.25f).SetEase(Ease.OutBack);

			var main = _dashParticles.main;
			main.startSizeYMultiplier = percentage * _initParticlesScale;			
		}
	}
}
