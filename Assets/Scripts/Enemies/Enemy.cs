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
		[SerializeField] protected Damageable _damageable;
		[SerializeField] private UnitAnimation _animation;
		[SerializeField] protected NavMeshAgent _agent;
		[SerializeField] private float _rotationSpeed;
		[SerializeField] private float _attackDistance;

		private UnitRotation _rotation;

		private void Awake() {
			_agent.updateRotation = false;
			_rotation = new UnitRotation(transform, _rotationSpeed);
		}

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
			Move();
			Rotate();
			UpdateAnimation();
		}

		protected bool IsLookingAtPlayer() {
			Vector3 playerPosition = GetPlayerPosition();
			return _rotation.IsLookingAt(playerPosition);
		}

		protected bool IsNearPlayer() {
			Vector3 playerPosition = GetPlayerPosition();
			return Vector3.Distance(playerPosition, transform.position) <= _attackDistance;
		}

		private void Move() {
			Vector3 playerPosition = Core.LevelController.Player.Position;
			_agent?.SetDestination(playerPosition);
		}

		private void Rotate() {
			Vector3 target = GetPlayerPosition();
			_rotation.Update(target);
		}

		private void UpdateAnimation() {
			_animation?.Trigger();
		}

		private Vector3 GetPlayerPosition() {
			return Core.LevelController.Player.Position;
		}
	}
}