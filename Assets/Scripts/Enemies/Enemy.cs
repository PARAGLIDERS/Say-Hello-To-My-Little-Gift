using DamageSystem;
using Player;
using PoolSystem;
using Root;
using Units;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies {
	[RequireComponent(typeof(NavMeshAgent))]
	public abstract class Enemy : PoolObject {
		[SerializeField] private Damageable _damageable;
		[SerializeField] private UnitAnimation _animation;
		[SerializeField] protected NavMeshAgent _agent;

		public override void Activate(Vector3 position, Quaternion rotation) {
			base.Activate(position, rotation);
			_damageable.ResetHealth();
			_damageable.OnDie += Deactivate;
		}

		public override void Deactivate() {
			base.Deactivate();
			_damageable.OnDie -= Deactivate;
		}

		protected virtual void Update() {
            Vector3 playerPosition = Core.LevelController.PlayerController.Position;
			_agent?.SetDestination(playerPosition);
			_animation?.Trigger();
		}
	}
}