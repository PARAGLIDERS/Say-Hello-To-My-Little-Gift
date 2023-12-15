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
		[SerializeField] protected float _attackDistance;
		[SerializeField] private float _lookThreshold = 0.03f;
		[SerializeField] private float _attackCooldown;

		private UnitRotation _rotation;
		private EnemyAttack _attack;

		private void Awake() {
			_agent.updateRotation = false;
			_rotation = new UnitRotation(transform, _rotationSpeed);
			_attack = new EnemyAttack(_attackCooldown);
		}

		public override void Activate(Vector3 position, Quaternion rotation) {			
			base.Activate(position, rotation);

			_damageable.ResetHealth();
			_damageable.OnDie += Deactivate;

			_attack.ResetCooldown(); 
			_attack.OnAttack += Attack;
		}

		public override void Deactivate() {
			base.Deactivate();
			_damageable.OnDie -= Deactivate;
			_attack.OnAttack -= Attack;
		}

		protected virtual void Update() {
			Move();
			UpdateAnimation();
			Rotate();

			if (!IsNearPlayer()) return;
			if (!IsLookingAtPlayer()) return;

			_attack.Update();
		}

		protected bool IsLookingAtPlayer() {
			Vector3 playerPosition = GetPlayerPosition();
			return _rotation.IsLookingAt(playerPosition, _lookThreshold);
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

		protected abstract void Attack();
	}
}