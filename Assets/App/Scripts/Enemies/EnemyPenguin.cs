using DamageSystem;
using System.Collections;
using UnityEngine;

namespace Enemies {
	public class EnemyPenguin : Enemy {
		[SerializeField] private DamageTrigger trigger;

		private const float _attackDelay = 2f;
		private float _currentAttackDelay;

		public override void Activate(Vector3 position, Quaternion rotation) {
			base.Activate(position, rotation);
			trigger.enabled = false;
			_currentAttackDelay = Time.time + _attackDelay;
		}

		protected override void Attack() {
			if(Time.time < _currentAttackDelay) return;
			trigger.enabled = true;
			StartCoroutine(WaitForTriggerAttack());
		}

		private IEnumerator WaitForTriggerAttack() {
			yield return new WaitForSeconds(_attackCooldown / 10);
			trigger.enabled = false;
		}
	}
}
