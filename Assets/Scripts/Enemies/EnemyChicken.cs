using DamageSystem;
using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Enemies {
    public class EnemyChicken : Enemy {        
        [SerializeField] private float _boomTime;
        [SerializeField] private AnimationCurve _boomCurve;
        [SerializeField] private float _maxScale = 1.3f;
		[SerializeField] private Explosion _explosion;

        private IEnumerator _finalCountdown;

		public override void Activate(Vector3 position, Quaternion rotation) {
			base.Activate(position, rotation);
			_agent.updatePosition = true;
			_damageable.OnDie += Explode;
		}

		public override void Deactivate() {
			base.Deactivate();
			_damageable.OnDie -= Explode;

			if (_finalCountdown != null) {
				StopCoroutine(_finalCountdown);
				_finalCountdown = null;
			}
		}

		protected override void Attack() {
			if (_finalCountdown != null)
				return;

			_finalCountdown = FinalCountdown();
			StartCoroutine(_finalCountdown);
		}

		private IEnumerator FinalCountdown() {
            //_agent.updatePosition = false;
            transform.DOScale(_maxScale, _boomTime).SetEase(_boomCurve);
            yield return new WaitForSeconds(_boomTime);

            _damageable.ApplyDamage(_damageable.MaxHealth, Quaternion.identity);			
        }

		private void Explode() {
			_explosion.Activate();
		}
	}
}
