using UnityEngine;
using UnityEngine.AI;

namespace Units {
	public class UnitNavmesh : Unit {
		[SerializeField] private NavMeshAgent _agent;
		[SerializeField] private Transform _bodyTransform;

		private UnitConfig _config;

		public void Init(UnitConfig config) {
			_config = config;
			InitUnit();
		}
		
		public override UnitAnimation GetAnimation() => new UnitAnimation(_config.AnimationConfig, _bodyTransform);
		public override UnitMotion GetMotion() => new UnitMotionNavmesh(_config.MotionConfig, _agent);
	}
}