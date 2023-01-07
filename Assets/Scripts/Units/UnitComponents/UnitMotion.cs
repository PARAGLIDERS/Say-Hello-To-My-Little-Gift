using Units.UnitConfigs;
using UnityEngine;
using UnityEngine.AI;

namespace Units {
	public abstract class UnitMotion {
		protected readonly UnitMotionConfig _config;
		
		protected UnitMotion(UnitMotionConfig config) {
			_config = config;
		}

		public abstract void Move(Vector3 direction);
	}

	public class UnitMotionRigidbody : UnitMotion {
		private readonly Rigidbody _rigidbody;

		public UnitMotionRigidbody(UnitMotionConfig config, Rigidbody rigidbody) : base(config) {
			_rigidbody = rigidbody;
		}

		public override void Move(Vector3 direction) {
			_rigidbody.velocity += direction * (_config.Speed);

			if (_rigidbody.velocity.magnitude > _config.MaxSpeed) {
				_rigidbody.velocity = _rigidbody.velocity.normalized * _config.MaxSpeed;
			}

			_rigidbody.velocity -= _rigidbody.velocity.normalized * _config.Drag;
		}
	}

	public class UnitMotionNavmesh : UnitMotion {
		private readonly NavMeshAgent _agent;
		
		public UnitMotionNavmesh(UnitMotionConfig config, NavMeshAgent agent) : base(config) {
			_agent = agent;
			_agent.speed = config.Speed;
			_agent.acceleration = 1f / (config.Drag > 0f ? config.Drag : 0.001f);
		}

		public override void Move(Vector3 targetPosition) {
			_agent.SetDestination(targetPosition);
		}
	}
}