using DamageSystem;
using UnityEngine;
using Utils;

namespace Enemies {
    public class EnemyDeer : Enemy {
		[SerializeField] private int _damage;
		[SerializeField] private LayerMask _attackableLayer;
		[SerializeField] private ParticleSystem _effect;
		
		protected override void Attack() {
			_effect.Play();
			Collider[] targets = Physics.OverlapSphere(transform.position, _attackDistance, _attackableLayer);

			foreach (Collider target in targets) {
				if (target.TryGetComponent(out Damageable damageable)) {
					Vector3 targetPosition = damageable.transform.position;
					Vector3 selfPosition = transform.position.With(y: targetPosition.y);
					Vector3 direction = targetPosition - selfPosition;
					Quaternion rotation = Quaternion.LookRotation(direction);

					damageable.ApplyDamage(_damage, rotation);
				}
			}
		}
	}
}
