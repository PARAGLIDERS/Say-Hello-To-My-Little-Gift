using Pooling;
using Root;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace Enemies {
	public class EnemyRabbit : EnemyRabbitBase {
		[SerializeField] private float _strafeCooldown;		
		[SerializeField] private ParticleSystem _cloneEffect;
		[SerializeField] private EnemyStage _stage;
		[SerializeField] private Transform[] _clonePoints;

		private List<PoolObject> _clones = new List<PoolObject>();

		public override void Activate(Vector3 position, Quaternion rotation) {
			base.Activate(position, rotation);
			_damageable.OnDamage += HandleDamage;
			_damageable.OnDie += HandleDie;
			_stage.Reset();
		}

		public override void Deactivate() {
			base.Deactivate();
			_damageable.OnDamage -= HandleDamage;
			_damageable.OnDie -= HandleDie;
		}

		private void HandleDie() {
			//foreach (PoolObject clone in _clones) {
				//clone.Deactivate();
			//}
			StopAllCoroutines();
		}

		private void HandleDamage() {
			float percentage = (float)_damageable.CurrentHealth / _damageable.MaxHealth;
			if (_stage.IsChanged(percentage)) {
				StartCoroutine(WaitClone());
			}
		}

		private IEnumerator WaitClone() {
			yield return new WaitForSeconds(0.1f);
			Clone();
		}

		private void Clone() {
			_cloneEffect.Play();
			foreach (Transform point in _clonePoints) {
				PoolObject clone = Core.PoolController.Spawn(ObjectType.Enemy_Rabbit_Clone, point.position, transform.rotation);
				Core.PoolController.Spawn(ObjectType.VFX_EnemySpawnEffect, point.position, transform.rotation);
				//_clones.Add(clone);
			}
		}
	}
}
